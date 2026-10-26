using Kortingen;

namespace Kortingen.Tests;

public class KortingstrategieTests
{
    [Fact]
    public void GeenKorting_GeeftAltijdNul()
    {
        IKortingStrategie strategie = new GeenKorting();
        Assert.Equal(0m, strategie.BerekenKorting(200m));
    }

    [Fact]
    public void VasteKorting_GeeftHetPercentage()
    {
        IKortingStrategie strategie = new VasteKorting(10m);
        Assert.Equal(20m, strategie.BerekenKorting(200m));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void VasteKorting_WeigertOngeldigPercentage(decimal percentage)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new VasteKorting(percentage));
    }

    [Fact]
    public void StaffelKorting_OnderDrempel_Geeft5Procent()
    {
        IKortingStrategie strategie = new StaffelKorting();
        Assert.Equal(2m, strategie.BerekenKorting(40m));
    }

    [Fact]
    public void StaffelKorting_VanafDrempel_Geeft15Procent()
    {
        IKortingStrategie strategie = new StaffelKorting();
        Assert.Equal(18m, strategie.BerekenKorting(120m));
    }
}
