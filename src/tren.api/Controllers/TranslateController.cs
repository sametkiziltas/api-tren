using Microsoft.AspNetCore.Mvc;
using tren.api.Models.Responses;
using tren.api.Services;

namespace tren.api.Controllers;

[Route("")]
public class TranslateController : ControllerBase 
{
    private readonly ITranslateService _translateService;

    public TranslateController(ITranslateService translateService)
    {
        _translateService = translateService;
    }
    
    [HttpGet("translates/{text}/{language}")]
    [ProducesResponseType(typeof(List<ResponseTranslate>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search(string text, string language)
    {
        List<ResponseTranslate> response = await _translateService.SearchAsync(text, language);
        return Ok(response);
    }

    [HttpGet("suggests/{text}/{language}")]
    [ProducesResponseType(typeof(List<ResponseSuggest>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Suggest(string text, string language) 
    {
        List<ResponseSuggest> response = await _translateService.SuggestAsync(text, language);
        return Ok(response);
    }
}

