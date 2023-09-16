using tren.api.Api.Data.Repositories;
using tren.api.Models.Responses;
using tren.api.Services;

namespace tren.api.Api.Services;

using api.Data.Entities;

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
        .ThenByDescending(y => y.Turkish == "general")
        .ToList();

        return result;
    }
    public async Task<List<ResponseSuggest>> SuggestAsync(string text, string language)
    {
        if (text.Length < 2)
        {
            return new List<ResponseSuggest>();
        }

        List<Word> words = await _wordRepository.SuggestAsync(text.ToLower(), language);

        List<ResponseSuggest> result = words.Select(x => new ResponseSuggest()
        {
            Suggestion = LanguageDecision(language, x)
        })
        .ToList();

        return result;
    }

    private string LanguageDecision(string language, Word word)
    {
        if (language == "tr")
        {
            return word.Turkish;
        }
        else if (language == "en")
        {
            return word.English;
        }

        return string.Empty;
    }



}