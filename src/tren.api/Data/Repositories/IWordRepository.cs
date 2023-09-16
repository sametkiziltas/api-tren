using tren.api.Data.Entities;

namespace tren.api.Api.Data.Repositories;

public interface IWordRepository
{
    // IQueryable<Models.Entities.Post> GetQueryable();
    Task<Word> GetAsync(int id);
    // Task<Models.Entities.Post> InsertAsync(Models.Entities.Post entity);
    // Task UpdateAsync(Models.Entities.Post entity);
    Task<List<Word>> SearchAsync(string text, string language);
    Task<List<Word>> SuggestAsync(string text, string language);
}