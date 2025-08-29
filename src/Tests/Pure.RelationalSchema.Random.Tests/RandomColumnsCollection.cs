using System.Collections;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomColumnsCollectionTests
{
    [Fact]
    public void DefaultConstructorProduceLessThan100Values()
    {
        IEnumerable<IColumn> randoms = new RandomColumnsCollection();

        Assert.True(randoms.Count() < 100);
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        const int count = 30;

        IEnumerable randomColumnTypes = new RandomColumnsCollection(new UShort(count));

        ICollection<IColumn> castedColumnTypes = [];

        foreach (object columnType in randomColumnTypes)
        {
            IColumn castedColumnType = (IColumn)columnType;
            castedColumnTypes.Add(castedColumnType);
        }

        Assert.Equal(count, castedColumnTypes.Count);
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 30;

        Random random = new Random();

        IEnumerable<IColumn> randomColumnTypes = new RandomColumnsCollection(new UShort(count), random);

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
        const int count = 30;

        IEnumerable<IColumn> randoms = new RandomColumnsCollection(new UShort(count));

        Assert.Equal(
            count,
            randoms
                .Select(x => new ColumnHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void TrowsExceptionWhenNamesCountLess()
    {
        const int count = 30;

        IEnumerable<IColumn> randomColumns = new RandomColumnsCollection(
            new UShort(count),
            new RandomStringCollection(new UShort(count - 1), new UShort(10)),
            new RandomColumnTypesCollection()
        );

        _ = Assert.Throws<ArgumentException>(() => randomColumns.Count());
    }

    [Fact]
    public void TrowsExceptionWhenColumnTypesCountLess()
    {
        const int count = 30;

        IEnumerable<IColumn> randomColumns = new RandomColumnsCollection(
            new UShort(count),
            new RandomStringCollection(),
            new RandomColumnTypesCollection(new UShort(count - 1))
        );

        _ = Assert.Throws<ArgumentException>(() => randomColumns.Count());
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
