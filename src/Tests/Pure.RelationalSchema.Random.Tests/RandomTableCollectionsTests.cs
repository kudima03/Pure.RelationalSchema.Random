using System.Collections;
using Pure.Primitives.Number;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomTableCollectionsTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        IEnumerable randoms = new RandomTablesCollection();

        ICollection<ITable> castedIndexes = [];

        foreach (object index in randoms)
        {
            ITable castedIndex = (ITable)index;
            castedIndexes.Add(castedIndex);
        }

        Assert.NotEmpty(castedIndexes);
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 5;

        Random random = new Random();

        IEnumerable<ITable> randoms = new RandomTablesCollection(
            new UShort(5),
            new RandomColumnsCollection(new UShort(2), random),
            new RandomIndexesCollection(
                new UShort(2),
                new RandomColumnsCollection(new UShort(2), random),
                random
            )
        );

        Assert.Equal(
            count,
            randoms
                .Select(x => new TableHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        const int count = 5;

        IEnumerable<ITable> randoms = new RandomTablesCollection(
            new UShort(5),
            new RandomColumnsCollection(new UShort(2)),
            new RandomIndexesCollection(
                new UShort(2),
                new RandomColumnsCollection(new UShort(2))
            )
        );

        Assert.Equal(
            count,
            randoms
                .Select(x => new TableHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomTablesCollection(new RandomColumnsCollection()).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomTablesCollection(new RandomIndexesCollection()).ToString()
        );
    }
}
