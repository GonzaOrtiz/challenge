using challenge.src.Api.Dtos;
using challenge.src.Application.Venta;
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


        [HttpGet("volumen-total")]
        public ActionResult GetVolumenTotal([FromQuery] Guid? centroId = null)
        {
            _logger.LogInformation("Obteniendo volumen total de ventas");
            var volumenTotal = _ventaBusiness.ObtenerVolumenTotal(centroId);
            return Ok(volumenTotal);
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
