using Kortingen;
using Microsoft.Extensions.DependencyInjection;

// We stellen de container samen: welke kortingstrategie geldt, en de kassa die
// ervan afhangt. Wil je een andere korting? Wijzig enkel de registratie hieronder.
ServiceCollection services = new();
services.AddSingleton<IKortingStrategie, StaffelKorting>();
services.AddTransient<Kassa>();

using ServiceProvider provider = services.BuildServiceProvider();

// De kassa krijgt haar strategie automatisch mee van de container.
Kassa kassa = provider.GetRequiredService<Kassa>();

foreach (decimal bedrag in new[] { 40m, 120m })
{
    decimal teBetalen = kassa.Reken(bedrag);
    Console.WriteLine($"Bedrag {bedrag:0.00} euro -> te betalen {teBetalen:0.00} euro");
}
