using System.Collections;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.ForeignKey;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomForeignKeysCollectionsTests
{
    [Fact]
    public void DefaultConstructorProduceLessThan100Values()
    {
        IEnumerable<IForeignKey> randoms = new RandomForeignKeysCollection();

        Assert.True(randoms.Count() < 100);
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        IEnumerable randoms = new RandomForeignKeysCollection(new UShort(10));

        ICollection<IForeignKey> casted = [];

        foreach (object item in randoms)
        {
            IForeignKey castedItem = (IForeignKey)item;
            casted.Add(castedItem);
        }

        Assert.NotEmpty(casted);
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 5;

        Random random = new Random();

        RandomTablesCollection randomReferencingTables = new RandomTablesCollection(
            new UShort(count),
            Enumerable
                .Range(0, count)
                .Select(_ => new RandomColumnsCollection(
                    new UShort(count),
                    new RandomStringCollection(
                        new UShort(count),
                        new UShort(count),
                        random
                    ),
                    new RandomColumnTypesCollection(
                        new UShort(count),
                        new RandomStringCollection(
                            new UShort(count),
                            new UShort(count),
                            random
                        )
                    )
                )),
            Enumerable
                .Range(0, count)
                .Select(_ => new RandomIndexesCollection(
                    new UShort(count),
                    Enumerable
                        .Range(0, count)
                        .Select(_ => new RandomColumnsCollection(
                            new UShort(count),
                            new RandomStringCollection(
                                new UShort(count),
                                new UShort(count),
                                random
                            ),
                            new RandomColumnTypesCollection(
                                new UShort(count),
                                new RandomStringCollection(
                                    new UShort(count),
                                    new UShort(count),
                                    random
                                )
                            )
                        )),
                    random
                )),
            random
        );

        RandomTablesCollection randomReferencedTables = new RandomTablesCollection(
            new UShort(count),
            Enumerable
                .Range(0, count)
                .Select(_ => new RandomColumnsCollection(
                    new UShort(count),
                    new RandomStringCollection(
                        new UShort(count),
                        new UShort(count),
                        random
                    ),
                    new RandomColumnTypesCollection(
                        new UShort(count),
                        new RandomStringCollection(
                            new UShort(count),
                            new UShort(count),
                            random
                        )
                    )
                )),
            Enumerable
                .Range(0, count)
                .Select(_ => new RandomIndexesCollection(
                    new UShort(count),
                    Enumerable
                        .Range(0, count)
                        .Select(_ => new RandomColumnsCollection(
                            new UShort(count),
                            new RandomStringCollection(
                                new UShort(count),
                                new UShort(count),
                                random
                            ),
                            new RandomColumnTypesCollection(
                                new UShort(count),
                                new RandomStringCollection(
                                    new UShort(count),
                                    new UShort(count),
                                    random
                                )
                            )
                        )),
                    random
                )),
            random
        );

        IEnumerable<IForeignKey> foreignKeys = new RandomForeignKeysCollection(
            new UShort(count),
            randomReferencingTables,
            new RandomColumnsCollection(
                new UShort(count),
                new RandomStringCollection(new UShort(count), new UShort(count), random),
                new RandomColumnTypesCollection(
                    new UShort(count),
                    new RandomStringCollection(
                        new UShort(count),
                        new UShort(count),
                        random
                    )
                )
            ),
            randomReferencedTables,
            new RandomColumnsCollection(
                new UShort(count),
                new RandomStringCollection(new UShort(count), new UShort(count), random),
                new RandomColumnTypesCollection(
                    new UShort(count),
                    new RandomStringCollection(
                        new UShort(count),
                        new UShort(count),
                        random
                    )
                )
            )
        );

        Assert.Equal(
            count,
            foreignKeys
                .Select(x => new ForeignKeyHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        const int count = 5;

        RandomTablesCollection randomReferencingTables = new RandomTablesCollection(
            new UShort(count),
            Enumerable
                .Range(0, count)
                .Select(_ => new RandomColumnsCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count)),
                    new RandomColumnTypesCollection(
                        new UShort(count),
                        new RandomStringCollection(new UShort(count), new UShort(count))
                    )
                )),
            Enumerable
                .Range(0, count)
                .Select(_ => new RandomIndexesCollection(
                    new UShort(count),
                    Enumerable
                        .Range(0, count)
                        .Select(_ => new RandomColumnsCollection(
                            new UShort(count),
                            new RandomStringCollection(
                                new UShort(count),
                                new UShort(count)
                            ),
                            new RandomColumnTypesCollection(
                                new UShort(count),
                                new RandomStringCollection(
                                    new UShort(count),
                                    new UShort(count)
                                )
                            )
                        ))
                ))
        );

        RandomTablesCollection randomReferencedTables = new RandomTablesCollection(
            new UShort(count),
            Enumerable
                .Range(0, count)
                .Select(_ => new RandomColumnsCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count)),
                    new RandomColumnTypesCollection(
                        new UShort(count),
                        new RandomStringCollection(new UShort(count), new UShort(count))
                    )
                )),
            Enumerable
                .Range(0, count)
                .Select(_ => new RandomIndexesCollection(
                    new UShort(count),
                    Enumerable
                        .Range(0, count)
                        .Select(_ => new RandomColumnsCollection(
                            new UShort(count),
                            new RandomStringCollection(
                                new UShort(count),
                                new UShort(count)
                            ),
                            new RandomColumnTypesCollection(
                                new UShort(count),
                                new RandomStringCollection(
                                    new UShort(count),
                                    new UShort(count)
                                )
                            )
                        ))
                ))
        );

        IEnumerable<IForeignKey> foreignKeys = new RandomForeignKeysCollection(
            new UShort(count),
            randomReferencingTables,
            new RandomColumnsCollection(
                new UShort(count),
                new RandomStringCollection(new UShort(count), new UShort(count)),
                new RandomColumnTypesCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count))
                )
            ),
            randomReferencedTables,
            new RandomColumnsCollection(
                new UShort(count),
                new RandomStringCollection(new UShort(count), new UShort(count)),
                new RandomColumnTypesCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count))
                )
            )
        );

        Assert.Equal(
            count,
            foreignKeys
                .Select(x => new ForeignKeyHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomForeignKeysCollection().GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomForeignKeysCollection().ToString()
        );
    }
}
