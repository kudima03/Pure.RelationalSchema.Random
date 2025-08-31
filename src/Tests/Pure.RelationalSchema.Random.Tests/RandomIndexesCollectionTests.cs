using System.Collections;
using Pure.Primitives.Number;
using Pure.RelationalSchema.Abstractions.Index;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomIndexesCollectionTests
{
    [Fact]
    public void DefaultConstructorProduceLessThan100Values()
    {
        IEnumerable<IIndex> randoms = new RandomIndexesCollection();

        Assert.True(randoms.Count() < 10);
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        const int count = 10;

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
        IEnumerable<IIndex> randoms = new RandomIndexesCollection(
            new UShort(10),
            new Random()
        );

        IEnumerable<IIndex> randomsWithNotEmptyFields = [.. randoms.Where(x => x.Columns.Any())];

        Assert.Equal(
            randomsWithNotEmptyFields.Count(),
            randomsWithNotEmptyFields
                .Select(x => new IndexHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        IEnumerable<IIndex> randoms = new RandomIndexesCollection(new UShort(10));

        IEnumerable<IIndex> randomsWithNotEmptyFields = [.. randoms.Where(x => x.Columns.Any())];

        Assert.Equal(
            randomsWithNotEmptyFields.Count(),
            randomsWithNotEmptyFields
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
