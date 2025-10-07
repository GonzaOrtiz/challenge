# challenge
## Realizar los siguientes puntos usando como lenguaje .NET o NET CORE, no tiene límite de tiempo ni limitaciones de uso de frameworks.
1- Enviar la solución en un repo git (github o gitlab) con los commits con el detalle de lo que se está subiendo.
2- Crear uno o varios servicios rest que expongan la solución al problema planteado.
3- Comentar en código lo que se considere necesario para explicar cómo se hizo la solución.
4- Imprimir el tiempo de ejecución de cada método.
5- Mockear los datos
6- No es necesario realizar cruds de las entidades en la api
7- Se tiene en cuenta la profundidad de la solución brindada y profesionalidad de código.
8- NO REQUIERE FRONTEND
9- Agregar explicación de cómo deben usarse los servicios.
10- Es valorable la entrega de pruebas unitarias implementadas con xUnit.
 
## Problema:
Una fábrica de automóviles produce 4 modelos de coches (sedan, suv, offroad, sport) cuyos precios de venta son: 8.000 u$s, 9.500 u$s, 12.500 u$s y 18.200 u$s. 
La empresa tiene 4 centros de distribución y venta. Se tiene una relación de datos correspondientes al tipo de vehículo vendido y punto de distribución en el que se produjo la venta del mismo.
El tipo “sport” incluye un impuesto extra del 7% que se debe adicionar al precio en la venta.
Realizar una api rest que contemple:
•            Insertar una venta
•            Obtener el volumen de ventas total.
•            Obtener el volumen de ventas por centro.
•            Obtener el porcentaje de unidades de cada modelo vendido en cada centro sobre el total de ventas de la empresa. 