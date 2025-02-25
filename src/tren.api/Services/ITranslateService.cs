using tren.api.Api.Models.Responses;
using tren.api.Models.Base;

namespace tren.api.Services;

public interface ITranslateService
{
    Task<List<ResponseTranslate>> SearchAsync(string text, string language);
}