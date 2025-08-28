using Pure.HashCodes;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomSchemaTests
{
    [Fact]
    public void InitializeNameAsCached()
    {
        ISchema schema = new RandomSchema(new RandomString(new UShort(20)));

        Assert.Equal(
            new DeterminedHash(schema.Name),
            new DeterminedHash(schema.Name),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeTablesAsCached()
    {
        const int count = 5;

        ISchema schema = new RandomSchema(
            new RandomString(new UShort(count)),
            new RandomTablesCollection(
                new UShort(count),
                new RandomStringCollection(new UShort(count), new UShort(count)),
                Enumerable
                    .Range(0, count)
                    .Select(_ => new RandomColumnsCollection(
                        new UShort(count),
                        new RandomStringCollection(new UShort(count), new UShort(count)),
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
            new RandomForeignKeysCollection(
                new UShort(count),
                new RandomTablesCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count)),
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
                ),
                new RandomColumnsCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count)),
                    new RandomColumnTypesCollection(
                        new UShort(count),
                        new RandomStringCollection(new UShort(count), new UShort(count))
                    )
                ),
                new RandomTablesCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count)),
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
                ),
                new RandomColumnsCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count)),
                    new RandomColumnTypesCollection(
                        new UShort(count),
                        new RandomStringCollection(new UShort(count), new UShort(count))
                    )
                )
            )
        );

        Assert.Equal(
            new AggregatedHash(schema.Tables.Select(x => new TableHash(x))),
            new AggregatedHash(schema.Tables.Select(x => new TableHash(x))),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeForeignKeysAsCached()
    {
        const ushort count = 5;

        ISchema schema = new RandomSchema(
            new RandomString(new UShort(count)),
            new RandomTablesCollection(
                new UShort(count),
                new RandomStringCollection(new UShort(count), new UShort(count)),
                Enumerable
                    .Range(0, count)
                    .Select(_ => new RandomColumnsCollection(
                        new UShort(count),
                        new RandomStringCollection(new UShort(count), new UShort(count)),
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
            new RandomForeignKeysCollection(
                new UShort(count),
                new RandomTablesCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count)),
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
                ),
                new RandomColumnsCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count)),
                    new RandomColumnTypesCollection(
                        new UShort(count),
                        new RandomStringCollection(new UShort(count), new UShort(count))
                    )
                ),
                new RandomTablesCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count)),
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
                ),
                new RandomColumnsCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count)),
                    new RandomColumnTypesCollection(
                        new UShort(count),
                        new RandomStringCollection(new UShort(count), new UShort(count))
                    )
                )
            )
        );

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
            .Select(_ => new RandomSchema(
                new RandomString(new UShort(count), random),
                new RandomTablesCollection(
                    new UShort(count),
                    new RandomStringCollection(
                        new UShort(count),
                        new UShort(count),
                        random
                    ),
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
                ),
                new RandomForeignKeysCollection(
                    new UShort(count),
                    new RandomTablesCollection(
                        new UShort(count),
                        new RandomStringCollection(
                            new UShort(count),
                            new UShort(count),
                            random
                        ),
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
                    ),
                    new RandomColumnsCollection(
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
                    ),
                    new RandomTablesCollection(
                        new UShort(count),
                        new RandomStringCollection(
                            new UShort(count),
                            new UShort(count),
                            random
                        ),
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
                    ),
                    new RandomColumnsCollection(
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
                    )
                )
            ));

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
            .Select(_ => new RandomSchema(
                new RandomString(new UShort(count)),
                new RandomTablesCollection(
                    new UShort(count),
                    new RandomStringCollection(new UShort(count), new UShort(count)),
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
                ),
                new RandomForeignKeysCollection(
                    new UShort(count),
                    new RandomTablesCollection(
                        new UShort(count),
                        new RandomStringCollection(new UShort(count), new UShort(count)),
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
                    ),
                    new RandomColumnsCollection(
                        new UShort(count),
                        new RandomStringCollection(new UShort(count), new UShort(count)),
                        new RandomColumnTypesCollection(
                            new UShort(count),
                            new RandomStringCollection(
                                new UShort(count),
                                new UShort(count)
                            )
                        )
                    ),
                    new RandomTablesCollection(
                        new UShort(count),
                        new RandomStringCollection(new UShort(count), new UShort(count)),
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
                    ),
                    new RandomColumnsCollection(
                        new UShort(count),
                        new RandomStringCollection(new UShort(count), new UShort(count)),
                        new RandomColumnTypesCollection(
                            new UShort(count),
                            new RandomStringCollection(
                                new UShort(count),
                                new UShort(count)
                            )
                        )
                    )
                )
            ));

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
