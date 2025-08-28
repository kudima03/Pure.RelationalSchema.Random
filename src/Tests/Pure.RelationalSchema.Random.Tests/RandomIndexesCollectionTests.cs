using System.Collections;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Index;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomIndexesCollectionTests
{
    [Fact]
    public void EnumeratesAsUntyped()
    {
        const int count = 5;

        IEnumerable randoms = new RandomIndexesCollection(new UShort(count));

        ICollection<IIndex> castedIndexes = [];

        foreach (object index in randoms)
        {
            IIndex castedIndex = (IIndex)index;
            castedIndexes.Add(castedIndex);
        }

        Assert.Equal(count, castedIndexes.Count);
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 100;

        Random random = new Random();

        IEnumerable<IIndex> randoms = new RandomIndexesCollection(
            new UShort(100),
            Enumerable
                .Range(0, 100)
                .Select(_ => new RandomColumnsCollection(
                    new UShort(10),
                    new RandomStringCollection(new UShort(10), new UShort(10), random),
                    new RandomColumnTypesCollection(
                        new UShort(10),
                        new RandomStringCollection(new UShort(10), new UShort(10), random)
                    )
                ))
        );

        Assert.Equal(
            count,
            randoms
                .Select(x => new IndexHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        const int count = 100;

        IEnumerable<IIndex> randoms = new RandomIndexesCollection(
            new UShort(100),
            Enumerable
                .Range(0, 100)
                .Select(_ => new RandomColumnsCollection(
                    new UShort(10),
                    new RandomStringCollection(new UShort(10), new UShort(10)),
                    new RandomColumnTypesCollection(
                        new UShort(10),
                        new RandomStringCollection(new UShort(10), new UShort(10))
                    )
                ))
        );

        Assert.Equal(
            count,
            randoms
                .Select(x => new IndexHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomIndexesCollection().GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomIndexesCollection().ToString()
        );
    }
}
