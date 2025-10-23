using challenge.src.Api.Dtos;
using challenge.src.Application.Venta;
using Microsoft.AspNetCore.Mvc;

namespace challenge.src.Api.Controllers.v1
{
    [ApiController]
    [Route("v1/ventas")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaServices _ventaServices;
        private readonly ILogger<VentasController> _logger;
        public VentasController(ILogger<VentasController> logger, IVentaServices ventaServices)
        {
            _logger = logger;
            _ventaServices = ventaServices;
        }

        [HttpGet]
        public ActionResult Get()
        {
            _logger.LogInformation("test log");
            var res = _ventaServices.test();
            return Ok(res);
        }

        [HttpPost]
        public ActionResult Post([FromBody]VentaRequestDto req)
        {
            _logger.LogInformation("InsertarVenta log");
            var res = _ventaServices.InsertarVenta(req);
            return Ok(res);
        }
    }
}
