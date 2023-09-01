using tren.api.Api.Data.Repositories;
using tren.api.Api.Models.Responses;
using tren.api.Services;

namespace tren.api.Api.Services;

public class TranslateService : ITranslateService
{
    private readonly IWordRepository _wordRepository;

    public TranslateService(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }
    
    public async Task<List<ResponseTranslate>> SearchAsync(string text, string language)
    {
        var words = await _wordRepository.SearchAsync(text.ToLower(), language);
        
        var result = words.Select(x => new ResponseTranslate()
        {
            Category = x.Category,
            English = x.English,
            Turkish = x.Turkish
        })
        .OrderByDescending(y => y.Category == "common usage")
        .ThenByDescending(y => y.Category == "general")
        .ToList();
        
        return result;;
    }
}