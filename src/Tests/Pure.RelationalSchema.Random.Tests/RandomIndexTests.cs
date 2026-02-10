using Pure.HashCodes;
using Pure.Primitives.Number;
using Pure.RelationalSchema.Abstractions.Index;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomIndexTests
{
    [Fact]
    public void InitializeColumnsAsCached()
    {
        IIndex index = new RandomIndex(new RandomColumnsCollection());

        Assert.Equal(
            new DeterminedHash(index.Columns.Select(x => new ColumnHash(x))),
            new DeterminedHash(index.Columns.Select(x => new ColumnHash(x))),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeUniquenessAsCached()
    {
        IIndex index = new RandomIndex(new RandomColumnsCollection(new UShort(5)));

        Assert.Equal(
            new DeterminedHash(index.IsUnique),
            new DeterminedHash(index.IsUnique),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        Random random = new Random();

        IEnumerable<IIndex> randoms = Enumerable
            .Range(0, 10)
            .Select(_ => new RandomIndex(random));

        IEnumerable<IIndex> randomsWithNotEmptyFields =
        [
            .. randoms.Where(x => x.Columns.Any()),
        ];

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
        const int count = 10;

        IEnumerable<IIndex> randoms = Enumerable
            .Range(0, count)
            .Select(_ => new RandomIndex());

        IEnumerable<IIndex> randomsWithNotEmptyFields =
        [
            .. randoms.Where(x => x.Columns.Any()),
        ];

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
        _ = Assert.Throws<NotSupportedException>(() => new RandomIndex().GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() => new RandomIndex().ToString());
    }
}
