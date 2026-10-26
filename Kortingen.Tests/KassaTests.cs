using Kortingen;

namespace Kortingen.Tests;

public class KassaTests
{
    [Fact]
    public void Reken_ZonderKorting_GeeftHetVolledigeBedrag()
    {
        Kassa kassa = new(new GeenKorting());
        Assert.Equal(50m, kassa.Reken(50m));
    }

    [Fact]
    public void Reken_MetVasteKorting_TrektKortingAf()
    {
        Kassa kassa = new(new VasteKorting(10m));
        Assert.Equal(90m, kassa.Reken(100m));
    }

    [Fact]
    public void Reken_NegatiefBedrag_Werpt()
    {
        Kassa kassa = new(new GeenKorting());
        Assert.Throws<ArgumentOutOfRangeException>(() => kassa.Reken(-1m));
    }
}
