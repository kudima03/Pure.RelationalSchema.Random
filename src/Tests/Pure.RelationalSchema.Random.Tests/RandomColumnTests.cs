using Pure.HashCodes;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

public sealed record RandomColumnTests
{
    [Fact]
    public void InitializeTypeAsCached()
    {
        IColumn column = new RandomColumn();

        Assert.Equal(
            new ColumnTypeHash(column.Type),
            new ColumnTypeHash(column.Type),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeNameAsCached()
    {
        IColumn column = new RandomColumn();

        Assert.Equal(
            new DeterminedHash(column.Name),
            new DeterminedHash(column.Name),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 100;

        System.Random random = new System.Random();

        IEnumerable<IColumn> randomColumns = Enumerable
            .Range(0, count)
            .Select(_ => new RandomColumn(random));

        Assert.Equal(
            count,
            randomColumns
                .Select(x => new ColumnHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        const int count = 100;

        IEnumerable<IColumn> randomColumns = Enumerable
            .Range(0, count)
            .Select(_ => new RandomColumn());

        Assert.Equal(
            count,
            randomColumns
                .Select(x => new ColumnHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() => new RandomColumn().GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() => new RandomColumn().ToString());
    }
}
