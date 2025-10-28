using System;
using challenge.src.Api.Dtos;
using challenge.src.Application.Ventas;
using Microsoft.AspNetCore.Mvc;

namespace challenge.src.Api.Controllers.v1
{
    [ApiController]
    [Route("v1/ventas")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaBusiness _ventaBusiness;
        private readonly ILogger<VentasController> _logger;

        public VentasController(ILogger<VentasController> logger, IVentaBusiness ventaBusiness)
        {
            _logger = logger;
            _ventaBusiness = ventaBusiness;
        }


        [HttpGet("porcentaje-modelos")]
        public ActionResult GetPorcentajeModelosPorCentro([FromQuery] Guid? centroId = null)
        {
            _logger.LogInformation("Obteniendo porcentaje de modelos por centro");
            var res = _ventaBusiness.ObtenerPorcentajeModelosPorCentro(centroId);
            return Ok(res);
        }


        [HttpGet("volumen-total")]
        public ActionResult GetVolumenTotal()
        {
            _logger.LogInformation("Obteniendo volumen total de ventas");

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var volumenTotal = _ventaBusiness.ObtenerVolumenTotal();
            stopwatch.Stop();

            _logger.LogInformation($"Tiempo de ejecución GetVolumenTotal: {stopwatch.ElapsedMilliseconds}ms");
            return Ok(new { VolumenTotal = volumenTotal, TiempoMs = stopwatch.ElapsedMilliseconds });
        }

        [HttpGet("volumen-total/centro")]
        public ActionResult GetVolumenTotalPorCentro([FromQuery] Guid centroId)
        {
            _logger.LogInformation($"Obteniendo volumen de ventas por centro: {centroId}");

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var volumenTotal = _ventaBusiness.ObtenerVolumenPorCentro(centroId);
            stopwatch.Stop();

            _logger.LogInformation($"Tiempo de ejecución GetVolumenTotalPorCentro: {stopwatch.ElapsedMilliseconds}ms");
            return Ok(new { CentroId = centroId, VolumenTotal = volumenTotal, TiempoMs = stopwatch.ElapsedMilliseconds });
        }

        [HttpPost]
        public ActionResult Post([FromBody]VentaRequestDto req)
        {
            _logger.LogInformation("InsertarVenta log");
            var res = _ventaBusiness.InsertarVenta(req);
            return Ok(res);
        }
    }
}
