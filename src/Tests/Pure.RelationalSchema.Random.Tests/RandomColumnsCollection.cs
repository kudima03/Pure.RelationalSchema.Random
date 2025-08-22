using System.Collections;
using Pure.Primitives.Number;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

public sealed record RandomColumnsCollectionTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        const int count = 100;

        IEnumerable randomColumnTypes = new RandomColumnsCollection(new UShort(100));

        ICollection<IColumn> castedColumnTypes = [];

        foreach (object columnType in randomColumnTypes)
        {
            IColumn castedColumnType = (IColumn)columnType;
            castedColumnTypes.Add(castedColumnType);
        }

        Assert.Equal(
            count,
            castedColumnTypes
                .Select(x => new ColumnHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 100;

        IEnumerable<IColumn> randomColumnTypes = new RandomColumnsCollection(
            new UShort(100)
        );

        Assert.Equal(
            count,
            randomColumnTypes
                .Select(x => new ColumnHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        const int count = 100;

        IEnumerable<IColumn> randoms = new RandomColumnsCollection(new UShort(100));

        Assert.Equal(
            count,
            randoms
                .Select(x => new ColumnHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomColumnsCollection().GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomColumnsCollection().ToString()
        );
    }
}
