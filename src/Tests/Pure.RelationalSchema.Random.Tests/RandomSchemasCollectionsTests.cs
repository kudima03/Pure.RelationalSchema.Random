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
        const ushort count = 10;

        IEnumerable randoms = new RandomSchemasCollection(new UShort(count));

        ICollection<ISchema> casted = [];

        foreach (object item in randoms)
        {
            ISchema castedItem = (ISchema)item;
            casted.Add(castedItem);
        }

        Assert.Equal(count, casted.Count);
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        IEnumerable<ISchema> randoms = new RandomSchemasCollection(
            new UShort(10),
            new Random()
        );

        IEnumerable<ISchema> randomsWithNotEmptyFields = [.. randoms.Where(x => x.ForeignKeys.Any() && x.Tables.Any() && x.Name.Any())];

        Assert.Equal(
            randomsWithNotEmptyFields.Count(),
            randomsWithNotEmptyFields
                .Select(x => new SchemaHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        IEnumerable<ISchema> randoms = new RandomSchemasCollection(new UShort(10));

        IEnumerable<ISchema> randomsWithNotEmptyFields = [.. randoms.Where(x => x.ForeignKeys.Any() && x.Tables.Any() && x.Name.Any())];

        Assert.Equal(
            randomsWithNotEmptyFields.Count(),
            randomsWithNotEmptyFields
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
