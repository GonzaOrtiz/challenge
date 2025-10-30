using Microsoft.AspNetCore.Mvc;
using challenge.src.Application.Modelos;

namespace challenge.src.Api.Controllers.v1
{
    [ApiController]
    [Route("v1/modelos")]
    public class ModelosController : ControllerBase
    {
    private readonly IModeloBusiness _modeloBusiness;
        private readonly ILogger<ModelosController> _logger;

        public ModelosController(ILogger<ModelosController> logger, IModeloBusiness modeloBusiness)
        {
            _logger = logger;
            _modeloBusiness = modeloBusiness;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            _logger.LogInformation("Obteniendo lista de modelos");
            var modelos = _modeloBusiness.GetAll();
            return Ok(modelos);
        }
    }
}