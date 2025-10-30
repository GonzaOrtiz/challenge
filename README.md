# Challenge

Este repositorio contiene una solución en .NET  para el ejercicio solicitado: modelar ventas de 4 modelos de vehículos en 4 centros, calcular volúmenes y porcentajes, y exponer estos comportamientos mediante una API REST.

## Resumen rápido
Requisitos implementados :
- Exponer endpoints REST para insertar una venta y obtener métricas (volumen total, volumen por centro, % por modelo/centro).
- Usar datos mock (InMemory) para las operaciones.
- El modelo "sport" aplica un impuesto extra del 7%.
- Imprimir/registrar el tiempo de ejecución de los endpoints (se recomienda middleware o logger).
- Pruebas unitarias con xUnit.


## Requisitos
- .NET SDK 8.0 (el proyecto se encuentra en `net8.0` según la carpeta `bin`).
- Docker (opcional).

## Ejecutar localmente
Desde PowerShell (Windows):

```powershell
cd .\challenge
dotnet restore
dotnet build --configuration Debug
dotnet run --project .\challenge.csproj
```

Por defecto la aplicación se ejecutará en el puerto configurado en `Properties/launchSettings.json` (o en `http://localhost:5000` / `https://localhost:5001` si no se modifica). Si el proyecto expone Swagger, se podrá acceder en `http://localhost:{PORT}/swagger`.

## Endpoints (documentación básica)
Nota: las rutas pueden variar según la implementación; ajustar según el `Route` usado en los controladores.

### POST /api/ventas
	- Descripción: Inserta una venta.
	- Response: 201 Created con la venta creada (id, modelo, precio calculado, centroId, fecha).
    - Ejemplo de body JSON para POST /api/ventas (forma esperada por el DTO `VentaRequestDto`):

```json
{
	"CentroDistribucionId": "11111111-1111-1111-1111-111111111111",
	"Fecha": "2025-10-30T12:34:56Z",
	"Detalles": [
		{
			"ModeloId": "00000000-0000-0000-0000-000000000004",
			"Cantidad": 1
		}
	]
}
```

### GET /api/ventas
	- Descripción: Obtiene todas las ventas registradas.
	- Response: 200 OK
		```json
		[
			{
				"id": "55555555-5555-5555-5555-555555555555",
				"centroDistribucionId": "11111111-1111-1111-1111-111111111111",
				"fecha": "2025-10-30T12:34:56Z",
				"detalles": [
					{
						"modeloId": "00000000-0000-0000-0000-000000000004",
						"cantidad": 1,
						"precioUnitario": 50000.00
					}
				]
			}
		]
		```

### GET /api/ventas/{id}
	- Descripción: Obtiene una venta específica por su ID.
	- Response: 200 OK
		```json
		{
			"id": "55555555-5555-5555-5555-555555555555",
			"centroDistribucionId": "11111111-1111-1111-1111-111111111111",
			"fecha": "2025-10-30T12:34:56Z",
			"detalles": [
				{
					"modeloId": "00000000-0000-0000-0000-000000000004",
					"cantidad": 1,
					"precioUnitario": 50000.00
				}
			]
		}
		```
	- Response: 404 Not Found si no existe la venta

###  GET /api/ventas/volumen
	- Descripción: Devuelve el volumen de ventas total (suma de precios).
	- Response: 200 OK
		```json
		{
			"volumenTotal": 123456.78
		}
		```

###  GET /api/ventas/volumen/centro/{centroId}
	- Descripción: Volumen de ventas para un centro concreto.
	- Response: 200 OK
		```json
		{
			"centroId": "11111111-1111-1111-1111-111111111111",
			"volumen": 34567.89
		}
		```
	- Response: 404 Not Found si no existe el centro

###  GET /api/ventas/porcentajes
	- Descripción: Devuelve el porcentaje de unidades de cada modelo vendido en cada centro sobre el total de unidades vendidas.
	- Response: 200 OK
		```json
		[
			{
				"centroId": "11111111-1111-1111-1111-111111111111",
				"modeloId": "00000000-0000-0000-0000-000000000001",
				"modelo": "sedan",
				"porcentajeSobreTotal": 12.5
			},
			{
				"centroId": "11111111-1111-1111-1111-111111111111",
				"modeloId": "00000000-0000-0000-0000-000000000004",
				"modelo": "sport",
				"porcentajeSobreTotal": 3.2
			}
		]
		```

### GET /api/centros
	- Descripción: Obtiene todos los centros de distribución.
	- Response: 200 OK
		```json
		[
			{
				"id": "11111111-1111-1111-1111-111111111111",
				"nombre": "Centro Norte"
			},
			{
				"id": "22222222-2222-2222-2222-222222222222",
				"nombre": "Centro Sur"
			}
		]
		```

### GET /api/modelos
	- Descripción: Obtiene todos los modelos de vehículos.
	- Response: 200 OK
		```json
		[
			{
				"id": "00000000-0000-0000-0000-000000000001",
				"nombre": "Sedan",
				"precioBase": 40000.00
			},
			{
				"id": "00000000-0000-0000-0000-000000000004",
				"nombre": "Sport",
				"precioBase": 50000.00
			}
		]
		```


### Mocks Disponibles
IDs mock disponibles (ver `Infrastructure/InMemoryData.cs`):

- Centros:
	- Centro Norte: "11111111-1111-1111-1111-111111111111"
	- Centro Sur:  "22222222-2222-2222-2222-222222222222"
	- Centro Este: "33333333-3333-3333-3333-333333333333"
	- Centro Oeste: "44444444-4444-4444-4444-444444444444"

- Modelos:
	- Sedan:    "00000000-0000-0000-0000-000000000001"
	- Suv:      "00000000-0000-0000-0000-000000000002"
	- Offroad:  "00000000-0000-0000-0000-000000000003"
	- Sport:    "00000000-0000-0000-0000-000000000004"

Ejemplo de body (JSON):





Ajusta los puertos según la configuración del `Dockerfile` y del `Kestrel` en `appsettings` si aplica.

## Tests
Si el proyecto incluye tests con xUnit, puedes ejecutarlos con:

```powershell
dotnet test
```

Se valoran especialmente pruebas que cubran:
- Cálculo de precio por modelo (incluido el +7% para "sport").
- Inserción de ventas y respuesta esperada.
- Cálculo de volúmenes y porcentajes.

## Swagger / OpenAPI
Si el proyecto expone Swagger (OpenAPI), se podrá acceder normalmente en `/swagger` cuando la app está en modo Development. Si no está habilitado, se recomienda añadir el paquete `Swashbuckle.AspNetCore` y configurar Swagger en `Program.cs` para facilitar pruebas y documentación interactiva.

## Mock data
En este proyecto hay un componente de datos en memoria — revisa `Infrastructure/InMemoryData.cs` para ver los datos iniciales y cómo modificarlos. 

## Medición de tiempos
El enunciado pide "imprimir el tiempo de ejecución de cada método". Recomendaciones:
- Implementar un middleware que mida el tiempo por request y lo registre con `ILogger`.
- Alternativa: usar filtros de acción (ActionFilter) para medir tiempo por controlador/método.

Ejemplo de salida esperada en logs:

```
INFO: Request POST /api/ventas completed in 12 ms
```

