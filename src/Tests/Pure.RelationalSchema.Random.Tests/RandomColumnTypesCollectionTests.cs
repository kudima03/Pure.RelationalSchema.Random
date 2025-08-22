using System.Collections;
using Pure.Primitives.Number;
using Pure.RelationalSchema.Abstractions.ColumnType;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

public sealed record RandomColumnTypesCollectionTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        const int count = 100;

        IEnumerable randomColumnTypes = new RandomColumnTypesCollection(
            new UShort(count),
            new System.Random()
        );

        ICollection<IColumnType> castedColumnTypes = [];

        foreach (object columnType in randomColumnTypes)
        {
            IColumnType castedColumnType = (IColumnType)columnType;
            castedColumnTypes.Add(castedColumnType);
        }

        Assert.Equal(
            count,
            castedColumnTypes
                .Select(x => new ColumnTypeHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 100;

        IEnumerable<IColumnType> randomColumnTypes = new RandomColumnTypesCollection(
            new UShort(count),
            new System.Random()
        );

        Assert.Equal(
            count,
            randomColumnTypes
                .Select(x => new ColumnTypeHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        const int count = 100;

        IEnumerable<IColumnType> randomColumnTypes = new RandomColumnTypesCollection(
            new UShort(count)
        );

        Assert.Equal(
            count,
            randomColumnTypes
                .Select(x => new ColumnTypeHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomColumnTypesCollection().GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomColumnTypesCollection().ToString()
        );
    }
}
