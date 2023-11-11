using Shows.Domain.Performers;

namespace Shows.UnitTests.Dummies;

public static class Performers
{
    public static Performer Performer1 = Performer.Create("Performer1",
        new List<PerformerInfo>()
        {
            PerformerInfo.Create("PerformerInfoName1", "PerformerInfoValue1")
        });

    public static Performer Performer2 = Performer.Create("Performer2",
        new List<PerformerInfo>()
        {
                PerformerInfo.Create("PerformerInfoName2", "PerformerInfoValue2")
        });

    public static Performer Performer3 = Performer.Create("Performer3",
        new List<PerformerInfo>()
        {
                PerformerInfo.Create("PerformerInfoName3", "PerformerInfoValue3")
        });
}
