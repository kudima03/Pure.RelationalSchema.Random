using Pure.HashCodes;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomColumnTests
{
    [Fact]
    public void DefaultConstructorProduceNameLessThan100Symbols()
    {
        IEnumerable<IColumn> randoms = Enumerable
            .Range(0, 100)
            .Select(_ => new RandomColumn());

        Assert.True(randoms.All(x => x.Name.TextValue.Length < 10));
    }

    [Fact]
    public void InitializeFromRandomName()
    {
        RandomString randomName = new RandomString(new UShort(10));

        IColumn column = new RandomColumn(randomName);

        Assert.Equal(
            new DeterminedHash(randomName),
            new DeterminedHash(column.Name),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeFromRandomColumnType()
    {
        RandomColumnType randomColumnType = new RandomColumnType();

        IColumn column = new RandomColumn(randomColumnType);

        Assert.Equal(
            new ColumnTypeHash(randomColumnType),
            new ColumnTypeHash(column.Type),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeTypeAsCached()
    {
        IColumn column = new RandomColumn();

        Assert.Equal(
            new ColumnTypeHash(column.Type),
            new ColumnTypeHash(column.Type),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeNameAsCached()
    {
        IColumn column = new RandomColumn();

        Assert.Equal(
            new DeterminedHash(column.Name),
            new DeterminedHash(column.Name),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 10;

        Random random = new Random();

        IEnumerable<IColumn> randomColumns = Enumerable
            .Range(0, count)
            .Select(_ => new RandomColumn(random));

        Assert.Equal(
            count,
            randomColumns
                .Select(x => new ColumnHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        const int count = 10;

        IEnumerable<IColumn> randomColumns = Enumerable
            .Range(0, count)
            .Select(_ => new RandomColumn());

        Assert.Equal(
            count,
            randomColumns
                .Select(x => new ColumnHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() => new RandomColumn().GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() => new RandomColumn().ToString());
    }
}
