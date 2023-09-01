using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace tren.api.Api.Data.Repositories;

using tren.api.Data.Entities;

public class WordRepository : IWordRepository
{
    private readonly AppDbContext _context;
    private static string categories = @"general,computer,common usage
formal
slang
colloquial
technical
category
history
literature
poetry
philosophy
art
medical
psychology
geography
chemistry
physics
biology
mathematics
architecture
engineering
education
social sciences
advertising
cinema
music
religion
anthropology
astronomy
automative
telecommunications
television
tourism
agriculture
environment
marine
military
ornithology
transportation
zoology
basketball
football
weight lifting
baseball
veterinary
food engineering
forestry
mining
theatre";
    public WordRepository(AppDbContext context)
    {
        _context = context;
    }

    // public IQueryable<Models.Entities.Word> GetQueryable()
    // {
    //     return new EnumerableQuery<Models.Entities.Word>();
    //     // return _context.Words.Where(c => !c.IsDeleted);
    // }

    public async Task<Word> GetAsync(int id)
    {
        return new Word();
        // return await _context.Pps.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Word>> SearchAsync(string text, string language)
    {
        var result = new List<Word>();

        switch (language)
        {
            case "tr":
                result = await _context.Words.Where(x => x.Turkish.Equals(text)).OrderBy(y=> y.Category).ThenBy(z=> z.Turkish).ThenBy(w=> w.English).ToListAsync();
                break;
            case "en":
                result = await _context.Words.Where(x => x.English.Equals(text)).OrderBy(y=> y.Category).ThenBy(z=> z.English).ThenBy(w=> w.Turkish).ToListAsync();
                break;
        }
        
        
        return result;
    }

    public async Task<Word> InsertAsync(Word entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Word entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
}