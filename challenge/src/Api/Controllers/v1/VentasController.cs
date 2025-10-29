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
            try
            {
            _logger.LogInformation("Obteniendo porcentaje de modelos por centro");
            var res = _ventaBusiness.ObtenerPorcentajeModelosPorCentro(centroId);
            return Ok(res);
            }catch( Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener porcentaje de modelos por centro");
                return StatusCode(500, new { Error = "Error interno del servidor" });
            }
        }


        [HttpGet("volumen-total")]
        public ActionResult GetVolumenTotal()
        {
            try
            {
                _logger.LogInformation("Obteniendo volumen total de ventas");
                var volumenTotal = _ventaBusiness.ObtenerVolumenTotal();
                return Ok(new { VolumenTotal = volumenTotal });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener volumen total de ventas");
                return StatusCode(500, new { Error = "Error interno del servidor" });
            }
        }

        [HttpGet("volumen-total/centro")]
        public ActionResult GetVolumenTotalPorCentro([FromQuery] Guid centroId)
        {
            try
            {
                _logger.LogInformation($"Obteniendo volumen de ventas por centro: {centroId}");
                var volumenTotal = _ventaBusiness.ObtenerVolumenPorCentro(centroId);
                return Ok(new { CentroId = centroId, VolumenTotal = volumenTotal });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error inesperado al obtener volumen por centro: {centroId}");
                return StatusCode(500, new { Error = "Error interno del servidor" });
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]VentaRequestDto req)
        {
            try
            {
                _logger.LogInformation("InsertarVenta log");
                var res = _ventaBusiness.InsertarVenta(req);
                return Ok(new {  res });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al insertar venta");
                return StatusCode(500, new { Error = "Error interno del servidor" });
            }
        }
    }
}
