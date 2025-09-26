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

        Assert.True(randoms.Count() < 10);
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        const int count = 10;

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
        IEnumerable<IColumn> randomColumnTypes = new RandomColumnsCollection(
            new UShort(10),
            new Random()
        );

        IEnumerable<IColumn> randomColumnTypesWithNotEmptyFields =
        [
            .. randomColumnTypes.Where(x => x.Name.Any() && x.Type.Name.Any()),
        ];

        Assert.Equal(
            randomColumnTypesWithNotEmptyFields.Count(),
            randomColumnTypesWithNotEmptyFields
                .Select(x => new ColumnHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        IEnumerable<IColumn> randomColumnTypes = new RandomColumnsCollection(
            new UShort(10)
        );

        IEnumerable<IColumn> randomColumnTypesWithNotEmptyFields =
        [
            .. randomColumnTypes.Where(x => x.Name.Any() && x.Type.Name.Any()),
        ];

        Assert.Equal(
            randomColumnTypesWithNotEmptyFields.Count(),
            randomColumnTypesWithNotEmptyFields
                .Select(x => new ColumnHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void TrowsExceptionWhenNamesCountLess()
    {
        const int count = 10;

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
        const int count = 10;

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
