using AutoFixture.Xunit2;
using Xunit.Sdk;

namespace Users.UnitTests.Helpers;

/// <summary>
/// Used instead of <see cref="InlineDataAttribute"/> when you have a combination of inline and Moq mock test method parameters
/// Make sure you are declaring your inline data receiver arguments in test methods first,
/// before all other arguments that will be filled in by mocks
/// </summary>
public class InlineAutoMoqDataAttribute : CompositeDataAttribute
{
    public InlineAutoMoqDataAttribute(params object[] values)
        : base(new DataAttribute[]
        {
            new InlineDataAttribute(values),
            new AutoMoqDataAttribute()
        })
    { }
}
