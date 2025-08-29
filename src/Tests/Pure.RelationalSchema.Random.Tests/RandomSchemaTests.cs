using Pure.HashCodes;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomSchemaTests
{
    [Fact]
    public void DefaultConstructorProduceNameLessThan100Symbols()
    {
        IEnumerable<ISchema> randoms = Enumerable
            .Range(0, 100)
            .Select(_ => new RandomSchema());

        Assert.True(randoms.All(x => x.Name.TextValue.Length < 10));
    }

    [Fact]
    public void InitializeNameAsCached()
    {
        ISchema schema = new RandomSchema();

        Assert.Equal(
            new DeterminedHash(schema.Name),
            new DeterminedHash(schema.Name),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeTablesAsCached()
    {
        ISchema schema = new RandomSchema();

        Assert.Equal(
            new AggregatedHash(schema.Tables.Select(x => new TableHash(x))),
            new AggregatedHash(schema.Tables.Select(x => new TableHash(x))),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeForeignKeysAsCached()
    {
        ISchema schema = new RandomSchema();

        Assert.Equal(
            new AggregatedHash(schema.ForeignKeys.Select(x => new ForeignKeyHash(x))),
            new AggregatedHash(schema.ForeignKeys.Select(x => new ForeignKeyHash(x))),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 5;

        Random random = new Random();

        IEnumerable<ISchema> randoms = Enumerable
            .Range(0, count)
            .Select(_ => new RandomSchema(random));

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

        IEnumerable<ISchema> randoms = Enumerable
            .Range(0, count)
            .Select(_ => new RandomSchema());

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
        _ = Assert.Throws<NotSupportedException>(() => new RandomSchema().GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() => new RandomSchema().ToString());
    }
}
