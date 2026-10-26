namespace Kortingen;

/// <summary>
/// Een kortingstrategie: bereken hoeveel korting je op een bedrag geeft.
/// Dit is het strategy-pattern: verschillende implementaties zijn onderling
/// uitwisselbaar omdat ze dezelfde interface delen.
/// </summary>
public interface IKortingStrategie
{
    decimal BerekenKorting(decimal bedrag);
}

/// <summary>Geeft nooit korting.</summary>
public sealed class GeenKorting : IKortingStrategie
{
    public decimal BerekenKorting(decimal bedrag) => 0m;
}

/// <summary>Geeft altijd een vast percentage korting.</summary>
public sealed class VasteKorting : IKortingStrategie
{
    private readonly decimal _percentage;

    /// <param name="percentage">Percentage tussen 0 en 100, bijvoorbeeld 10 voor 10%.</param>
    public VasteKorting(decimal percentage)
    {
        if (percentage is < 0 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(percentage), "Percentage moet tussen 0 en 100 liggen.");
        }

        _percentage = percentage;
    }

    public decimal BerekenKorting(decimal bedrag) => bedrag * _percentage / 100m;
}

/// <summary>
/// Staffelkorting: vanaf 100 euro krijg je 15%, daaronder 5%.
/// </summary>
public sealed class StaffelKorting : IKortingStrategie
{
    public decimal BerekenKorting(decimal bedrag)
    {
        decimal percentage = bedrag >= 100m ? 15m : 5m;
        return bedrag * percentage / 100m;
    }
}
