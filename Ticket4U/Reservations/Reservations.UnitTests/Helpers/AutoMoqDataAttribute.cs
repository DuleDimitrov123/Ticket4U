using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Reservations.UnitTests.Helpers;

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
