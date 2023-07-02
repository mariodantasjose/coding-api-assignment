using Microsoft.AspNetCore.Mvc;
using QuestionnaireAPI.Application.Interfaces;
using QuestionnaireAPI.Application;

namespace QuestionnaireAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricsService _metricsService;
        private readonly ILogger<MetricsController> _logger;

        public MetricsController(IMetricsService metricsService, ILogger<MetricsController> logger)
        {
            _metricsService = metricsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMetrics()
        {
            try
            {
                return EvaluateAndReturn(await _metricsService.CalculateMetrics(ModelState));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting the question metrics: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        private IActionResult EvaluateAndReturn<T>(ServiceResponse<T> result)
        {
            if (result.NotFound) return NotFound();
            if (result.IsError) return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }
    }
}
