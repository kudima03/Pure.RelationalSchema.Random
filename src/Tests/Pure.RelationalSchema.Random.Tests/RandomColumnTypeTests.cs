using Pure.HashCodes;
using Pure.RelationalSchema.Abstractions.ColumnType;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomColumnTypeTests
{
    [Fact]
    public void DefaultConstructorProduceNameLessThan100Symbols()
    {
        IEnumerable<IColumnType> columnType = Enumerable
            .Range(0, 100)
            .Select(_ => new RandomColumnType());

        Assert.True(columnType.All(x => x.Name.TextValue.Length < 10));
    }

    [Fact]
    public void InitializeNameAsCached()
    {
        IColumnType columnType = new RandomColumnType();

        Assert.Equal(
            new DeterminedHash(columnType.Name),
            new DeterminedHash(columnType.Name),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 10;

        Random random = new Random();

        IEnumerable<IColumnType> randomColumnTypes = Enumerable
            .Range(0, count)
            .Select(_ => new RandomColumnType(random));

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
        const int count = 10;

        IEnumerable<IColumnType> randomColumnTypes = Enumerable
            .Range(0, count)
            .Select(_ => new RandomColumnType());

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
            new RandomColumnType().GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() => new RandomColumnType().ToString());
    }
}
