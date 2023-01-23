using FancyLib;
using FancyLib.Services;
using FluentAssertions;

namespace FancyLibTests.Services;

public class TextServiceTests : TestBase
{
    public static IEnumerable<object[]> Texts => Enumerable.Range(1, 100).Select(n => new object[] { "Text " + n });

    private readonly TextService textService;
    private readonly AppDbContext dbContext;

    public TextServiceTests(TestFixture fixture) : base(fixture)
    {
        textService = GetInstance<TextService>();
        dbContext = GetInstance<AppDbContext>();
    }

    [Theory]
    [MemberData(nameof(Texts))]
    public void CreateText_AddsTextToDatabase(string text)
    {
        var id = textService.CreateText(text);

        dbContext.ChangeTracker.Clear();

        var count = dbContext.Texts.Count();
        count.Should().Be(2);

        var entity = dbContext.Texts.Find(id);
        entity.Should().NotBeNull();
        entity!.Value.Should().Be(text);
    }
}

public class TextServiceTexts1 : TextServiceTests { public TextServiceTexts1(TestFixture fixture) : base(fixture) { } }
public class TextServiceTexts2 : TextServiceTests { public TextServiceTexts2(TestFixture fixture) : base(fixture) { } }
public class TextServiceTexts3 : TextServiceTests { public TextServiceTexts3(TestFixture fixture) : base(fixture) { } }
public class TextServiceTexts4 : TextServiceTests { public TextServiceTexts4(TestFixture fixture) : base(fixture) { } }
public class TextServiceTexts5 : TextServiceTests { public TextServiceTexts5(TestFixture fixture) : base(fixture) { } }
public class TextServiceTexts6 : TextServiceTests { public TextServiceTexts6(TestFixture fixture) : base(fixture) { } }
public class TextServiceTexts7 : TextServiceTests { public TextServiceTexts7(TestFixture fixture) : base(fixture) { } }
public class TextServiceTexts8 : TextServiceTests { public TextServiceTexts8(TestFixture fixture) : base(fixture) { } }
public class TextServiceTexts9 : TextServiceTests { public TextServiceTexts9(TestFixture fixture) : base(fixture) { } }