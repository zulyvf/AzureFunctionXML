using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Xml.Linq;

namespace FunctionApplicationXML.Functions;

public class XmlFunction
{
    private readonly ILogger _logger;

    public XmlFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<XmlFunction>();
    }

    [Function("Function")]
    public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
    {
        try {
            string xmlPath = Path.Combine(Environment.CurrentDirectory, "file-area.xml");
            XDocument xml = XDocument.Load(xmlPath);


            var areas = xml.Descendants("area").ToList();
            _logger.LogInformation($"Total de nodos de tipo <area>: {areas.Count}");

            int areas2 = areas.Count(a => a.Descendants("employee").Count() > 2);
            _logger.LogInformation($"Total de nodos de tipo <area> que tienen más de 2 empleados: {areas2}");

            foreach (var area in areas)
            {
                string areaName = area.Element("name")?.Value ?? "Sin nombre";
                var salary = area.Descendants("employee").Select(e => decimal.Parse(e.Attribute("salary")?.Value ?? "0"));
                decimal totalSalary = salary.Sum();
                _logger.LogInformation($"{areaName}|{totalSalary:F2}");
            }
        } catch (Exception ex)  {
            _logger.LogInformation($"Tenemos el siguiente error: {ex.Message}");
        }        
    }
}