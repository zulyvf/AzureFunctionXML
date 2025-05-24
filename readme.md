# Azure Function - Reading XML file

Este proyecto ejecuta un Azure Function con Time Trigger cada 30 minutos, procesando un archivo XML.

## Funcionalidad

La funci�n realiza las siguientes tareas:

1. Lee el archivo XML (`file-area.xml`).
2. Imprime cuantos nodos `<area>` existen.
3. Imprime cuantos nodos `<area>` tienen m�s de 2 empleados.
4. Por �ltimo, por cada `<area>` imprime su nombre y a suma total de los salarios, separados por un pipe (`|`).