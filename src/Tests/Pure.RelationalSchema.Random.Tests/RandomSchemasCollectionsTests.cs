using System.Collections;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomSchemasCollectionsTests
{
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

        Random random = new Random();

        IEnumerable<ISchema> randoms = new RandomSchemasCollection(
            new UShort(count),
            new RandomStringCollection(new UShort(50), new UShort(50), random),
            Enumerable
                .Range(0, count)
                .Select(_ => new RandomTablesCollection(
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
                                ))
                        ))
                )),
            Enumerable
                .Range(0, count)
                .Select(_ => new RandomForeignKeysCollection(
                    new UShort(count),
                    new RandomTablesCollection(
                        new UShort(count),
                        Enumerable
                            .Repeat(0, count)
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
                                    ))
                            )),
                        random
                    ),
                    new RandomColumnsCollection(),
                    new RandomTablesCollection(
                        new UShort(count),
                        Enumerable
                            .Repeat(0, count)
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
                                    ))
                            )),
                        random
                    ),
                    new RandomColumnsCollection()
                ))
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

        IEnumerable<ISchema> randoms = new RandomSchemasCollection(
            new UShort(count),
            new RandomStringCollection(new UShort(50), new UShort(50)),
            Enumerable
                .Range(0, count)
                .Select(_ => new RandomTablesCollection(
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
                )),
            Enumerable
                .Range(0, count)
                .Select(_ => new RandomForeignKeysCollection(
                    new UShort(count),
                    new RandomTablesCollection(
                        new UShort(count),
                        Enumerable
                            .Repeat(0, count)
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
                    ),
                    new RandomColumnsCollection(),
                    new RandomTablesCollection(
                        new UShort(count),
                        Enumerable
                            .Repeat(0, count)
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
                    ),
                    new RandomColumnsCollection()
                ))
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
