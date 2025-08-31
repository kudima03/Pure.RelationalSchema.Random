using System.Collections;
using Pure.Primitives.Number;
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

        Assert.True(randoms.Count() < 10);
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        const ushort count = 10;

        IEnumerable randoms = new RandomTablesCollection(new UShort(count));

        ICollection<ITable> castedIndexes = [];

        foreach (object index in randoms)
        {
            ITable castedIndex = (ITable)index;
            castedIndexes.Add(castedIndex);
        }

        Assert.Equal(count, castedIndexes.Count);
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        IEnumerable<ITable> randoms = new RandomTablesCollection(
            new UShort(5),
            new Random()
        );

        IEnumerable<ITable> randomsWithNotEmptyFields = [.. randoms.Where(x => x.Columns.Any() && x.Indexes.Any() && x.Name.Any())];

        Assert.Equal(
            randomsWithNotEmptyFields.Count(),
            randomsWithNotEmptyFields
                .Select(x => new TableHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        IEnumerable<ITable> randoms = new RandomTablesCollection(new UShort(5));

        IEnumerable<ITable> randomsWithNotEmptyFields = [.. randoms.Where(x => x.Columns.Any() && x.Indexes.Any() && x.Name.Any())];

        Assert.Equal(
            randomsWithNotEmptyFields.Count(),
            randomsWithNotEmptyFields
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
