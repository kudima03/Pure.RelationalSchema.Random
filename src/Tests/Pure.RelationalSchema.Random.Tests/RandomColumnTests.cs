using Pure.HashCodes;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

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

        Random random = new Random();

        IEnumerable<IColumn> randomColumns = Enumerable
            .Range(0, count)
            .Select(_ => new RandomColumn(
                new RandomString(
                    new RandomUShort(new MinUshort(), new UShort(100), random),
                    random
                ),
                new RandomColumnType(
                    new RandomString(
                        new RandomUShort(new MinUshort(), new UShort(100), random),
                        random
                    )
                )
            ));

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
            .Select(_ => new RandomColumn(
                new RandomString(new RandomUShort(new MinUshort(), new UShort(100))),
                new RandomColumnType(
                    new RandomString(new RandomUShort(new MinUshort(), new UShort(100)))
                )
            ));

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
