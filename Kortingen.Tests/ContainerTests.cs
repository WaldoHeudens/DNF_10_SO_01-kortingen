using Kortingen;
using Microsoft.Extensions.DependencyInjection;

namespace Kortingen.Tests;

/// <summary>
/// Test dat de container de kassa correct samenstelt: de geregistreerde
/// strategie moet automatisch in de kassa terechtkomen.
/// </summary>
public class ContainerTests
{
    [Fact]
    public void Container_InjecteertGeregistreerdeStrategie()
    {
        ServiceCollection services = new();
        services.AddSingleton<IKortingStrategie, StaffelKorting>();
        services.AddTransient<Kassa>();

        using ServiceProvider provider = services.BuildServiceProvider();
        Kassa kassa = provider.GetRequiredService<Kassa>();

        // StaffelKorting geeft 15% vanaf 100 euro: 120 - 18 = 102.
        Assert.Equal(102m, kassa.Reken(120m));
    }

    [Fact]
    public void Singleton_GeeftTelkensDezelfdeInstantie()
    {
        ServiceCollection services = new();
        services.AddSingleton<IKortingStrategie, StaffelKorting>();

        using ServiceProvider provider = services.BuildServiceProvider();

        IKortingStrategie eerste = provider.GetRequiredService<IKortingStrategie>();
        IKortingStrategie tweede = provider.GetRequiredService<IKortingStrategie>();

        Assert.Same(eerste, tweede);
    }

    [Fact]
    public void Transient_GeeftTelkensEenNieuweInstantie()
    {
        ServiceCollection services = new();
        services.AddTransient<IKortingStrategie, StaffelKorting>();

        using ServiceProvider provider = services.BuildServiceProvider();

        IKortingStrategie eerste = provider.GetRequiredService<IKortingStrategie>();
        IKortingStrategie tweede = provider.GetRequiredService<IKortingStrategie>();

        Assert.NotSame(eerste, tweede);
    }
}
