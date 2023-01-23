using FancyLib;
using FancyLib.Services;
using FluentAssertions;

namespace FancyLibTests.Services;

public class NumberServiceTests : TestBase
{
    public static IEnumerable<object[]> PositiveNumbers => Enumerable.Range(1, 100).Select(n => new object[] { n });
    public static IEnumerable<object[]> NegativeNumbers => Enumerable.Range(1, 100).Select(n => new object[] { -n });

    private readonly NumberService numberService;
    private readonly AppDbContext dbContext;

    public NumberServiceTests(TestFixture fixture) : base(fixture)
    {
        numberService = GetInstance<NumberService>();
        dbContext = GetInstance<AppDbContext>();
    }

    [Theory]
    [MemberData(nameof(PositiveNumbers))]
    public void CreateNumber_AddsNumberToDatabase(int number)
    {
        var id = numberService.CreateNumber(number);

        dbContext.ChangeTracker.Clear();

        var entity = dbContext.Numbers.Find(id);
        entity.Should().NotBeNull();
        entity!.Value.Should().Be(number);
    }

    [Theory]
    [MemberData(nameof(NegativeNumbers))]
    public void CreateNumber_NegativeNumber_ThrowsException(int number)
    {
        var act = () => numberService.CreateNumber(number);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}

public class NumberServiceTexts1 : NumberServiceTests { public NumberServiceTexts1(TestFixture fixture) : base(fixture) { } }
public class NumberServiceTexts2 : NumberServiceTests { public NumberServiceTexts2(TestFixture fixture) : base(fixture) { } }
public class NumberServiceTexts3 : NumberServiceTests { public NumberServiceTexts3(TestFixture fixture) : base(fixture) { } }
public class NumberServiceTexts4 : NumberServiceTests { public NumberServiceTexts4(TestFixture fixture) : base(fixture) { } }
public class NumberServiceTexts5 : NumberServiceTests { public NumberServiceTexts5(TestFixture fixture) : base(fixture) { } }
public class NumberServiceTexts6 : NumberServiceTests { public NumberServiceTexts6(TestFixture fixture) : base(fixture) { } }
public class NumberServiceTexts7 : NumberServiceTests { public NumberServiceTexts7(TestFixture fixture) : base(fixture) { } }
public class NumberServiceTexts8 : NumberServiceTests { public NumberServiceTexts8(TestFixture fixture) : base(fixture) { } }
public class NumberServiceTexts9 : NumberServiceTests { public NumberServiceTexts9(TestFixture fixture) : base(fixture) { } }