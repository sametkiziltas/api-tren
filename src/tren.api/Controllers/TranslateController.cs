using Microsoft.AspNetCore.Mvc;
using tren.api.Api.Models.Responses;
using tren.api.Services;

namespace tren.api.Controllers;

[Route("translates")]
public class TranslateController : ControllerBase 
{
    private readonly ITranslateService _translateService;

    public TranslateController(ITranslateService translateService)
    {
        _translateService = translateService;
    }
    
    [HttpGet("{text}/{language}")]
    [ProducesResponseType(typeof(List<ResponseTranslate>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search(string text, string language)
    {
        List<ResponseTranslate> response = await _translateService.SearchAsync(text, language);
        return Ok(response);
    }
}