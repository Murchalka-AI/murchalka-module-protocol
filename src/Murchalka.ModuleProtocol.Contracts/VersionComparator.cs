namespace Murchalka.ModuleProtocol.Contracts;

internal sealed record VersionComparator(VersionComparatorOperator Operator, SemanticVersion Version)
{
    public bool Matches(SemanticVersion candidate)
    {
        var comparison = candidate.CompareTo(Version);
        return Operator switch
        {
            VersionComparatorOperator.Equal => comparison == 0,
            VersionComparatorOperator.GreaterThan => comparison > 0,
            VersionComparatorOperator.GreaterThanOrEqual => comparison >= 0,
            VersionComparatorOperator.LessThan => comparison < 0,
            VersionComparatorOperator.LessThanOrEqual => comparison <= 0,
            _ => false
        };
    }
}
