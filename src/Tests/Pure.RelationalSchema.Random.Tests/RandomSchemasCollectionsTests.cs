using System.Collections;
using Pure.Primitives.Number;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomSchemasCollectionsTests
{
    [Fact]
    public void DefaultConstructorProduceLessThan100Values()
    {
        IEnumerable<ISchema> randoms = new RandomSchemasCollection();

        Assert.True(randoms.Count() < 10);
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        IEnumerable randoms = new RandomSchemasCollection();

        ICollection<ISchema> casted = [];

        foreach (object item in randoms)
        {
            ISchema castedItem = (ISchema)item;
            casted.Add(castedItem);
        }

        Assert.NotEmpty(casted);
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 5;

        IEnumerable<ISchema> randoms = new RandomSchemasCollection(
            new UShort(count),
            new Random()
        );

        Assert.Equal(
            count,
            randoms
                .Select(x => new SchemaHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        const int count = 5;

        IEnumerable<ISchema> randoms = new RandomSchemasCollection(new UShort(count));

        Assert.Equal(
            count,
            randoms
                .Select(x => new SchemaHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomSchemasCollection().GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomSchemasCollection().ToString()
        );
    }
}
