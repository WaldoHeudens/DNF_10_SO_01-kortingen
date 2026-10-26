namespace Kortingen;

/// <summary>
/// Rekent het te betalen bedrag uit. De kassa weet niet welke korting geldt:
/// ze hangt af van de abstractie IKortingStrategie, die via de constructor
/// binnenkomt (constructor-injectie, Dependency Inversion).
/// </summary>
public sealed class Kassa
{
    private readonly IKortingStrategie _korting;

    public Kassa(IKortingStrategie korting)
    {
        _korting = korting;
    }

    /// <summary>
    /// Geeft het te betalen bedrag na korting. Een negatief bedrag is niet toegelaten.
    /// </summary>
    public decimal Reken(decimal bedrag)
    {
        if (bedrag < 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(bedrag), "Bedrag mag niet negatief zijn.");
        }

        return bedrag - _korting.BerekenKorting(bedrag);
    }
}
