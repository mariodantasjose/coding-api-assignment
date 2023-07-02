using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireAPI.Application.Interfaces;
using QuestionnaireAPI.Application;
using QuestionnaireAPI.Domain.DTO;

namespace QuestionnaireAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQuestionnaireFormService _loaderService;
        private readonly IAnswerService _answerService;
        private readonly ILogger<QuestionnaireController> _logger;

        public QuestionnaireController(IMapper mapper, IQuestionnaireFormService loaderService, IAnswerService answerService, ILogger<QuestionnaireController> logger)
        {
            _mapper = mapper;
            _loaderService = loaderService;
            _answerService = answerService;
            _logger = logger;
        }

        [HttpGet("GetQuestionnairePages")]
        public async Task<IActionResult> GetPaginatedQuestionnaire([FromQuery] PagingParameters pagingParameters)
        {
            try
            {
                return EvaluateAndReturn(await _loaderService.LoadPaginatedQuestionnaire(pagingParameters));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting the questionnaire form: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("PostAnswers")]
        public async Task<IActionResult> PostAnswers(List<PostAnswer> answers)
        {
            try
            {
                return EvaluateAndReturn(await _answerService.SubmitAnswers(_mapper, answers));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while submitting the answers: {ex}");
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
