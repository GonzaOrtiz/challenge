using Microsoft.AspNetCore.Mvc;
using challenge.src.Infrastructure.Modelo;
using System.Diagnostics;

namespace challenge.src.Api.Controllers.v1
{
    [ApiController]
    [Route("v1/modelos")]
    public class ModelosController : ControllerBase
    {
        private readonly IModeloRepository _modeloRepository;
        private readonly ILogger<ModelosController> _logger;

        public ModelosController(ILogger<ModelosController> logger, IModeloRepository modeloRepository)
        {
            _logger = logger;
            _modeloRepository = modeloRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            _logger.LogInformation("Obteniendo lista de modelos");
            var modelos = _modeloRepository.GetAll();
            return Ok(modelos);
        }
    }
}