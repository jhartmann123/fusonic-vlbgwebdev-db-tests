using FancyLib.Domain;

namespace FancyLib.Services;

public class TextService
{
    private readonly AppDbContext dbContext;

    public TextService(AppDbContext dbContext) => this.dbContext = dbContext;

    public int CreateText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Text is empty", nameof(text));

        var entity = new Text { Value = text };
        dbContext.Add(entity);
        dbContext.SaveChanges();

        return entity.Id;
    }
}