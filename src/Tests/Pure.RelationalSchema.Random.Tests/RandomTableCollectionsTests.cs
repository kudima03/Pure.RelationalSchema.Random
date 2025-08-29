using System.Collections;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomTableCollectionsTests
{
    [Fact]
    public void DefaultConstructorProduceLessThan100Values()
    {
        IEnumerable<ITable> randoms = new RandomTablesCollection();

        Assert.True(randoms.Count() < 100);
    }

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

        IEnumerable<ITable> randoms = new RandomTablesCollection(new UShort(count), new Random());

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

        IEnumerable<ITable> randoms = new RandomTablesCollection(new UShort(count));

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
            new RandomTablesCollection().GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomTablesCollection().ToString()
        );
    }
}
