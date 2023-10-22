using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Users.UnitTests.Helpers;

/// <summary>
/// Used in conjunction with <see cref="TheoryAttribute"/> to allow passing in Moq mocks for classes that use DI.
/// Don't forget you can use <see cref="FrozenAttribute"/> to also receive raw Moq mocks along side your instance under test
/// </summary>
public class AutoMoqDataAttribute : AutoDataAttribute
{
    private readonly IFixture _fixture;

    public AutoMoqDataAttribute()
        : base(() => new Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true }))
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true });
    }

    private AutoMoqDataAttribute(IFixture fixture) : base(fixture)
    {

    }
}
