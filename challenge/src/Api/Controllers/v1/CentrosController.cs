using Microsoft.AspNetCore.Mvc;
using challenge.src.Application.Centros;

namespace challenge.src.Api.Controllers.v1
{
    [ApiController]
    [Route("v1/centros")]
    public class CentrosController : ControllerBase
    {
        private readonly ICentroBusiness _centroBusiness;
        private readonly ILogger<CentrosController> _logger;

        public CentrosController(ILogger<CentrosController> logger, ICentroBusiness centroBusiness)
        {
            _logger = logger;
            _centroBusiness = centroBusiness;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            _logger.LogInformation("Obteniendo lista de centros");
            var centros = _centroBusiness.GetAll();
            return Ok(centros);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            _logger.LogInformation($"Obteniendo centro por id: {id}");
            var centro = _centroBusiness.GetById(id);
            if (centro == null) return NotFound();
            return Ok(centro);
        }
    }
}
