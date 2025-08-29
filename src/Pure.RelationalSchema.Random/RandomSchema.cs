using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.ForeignKey;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.Abstractions.Table;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomSchema : ISchema
{
    private readonly Lazy<IEnumerable<ITable>> _tables;

    private readonly Lazy<IEnumerable<IForeignKey>> _foreignKeys;

    public RandomSchema()
        : this(Random.Shared) { }

    public RandomSchema(Random random)
        : this(
            new RandomString(
                new RandomUShort(new MinUshort(), new UShort(100), random),
                random
            ),
            new RandomTablesCollection(random),
            new RandomForeignKeysCollection(random)
        )
    { }

    public RandomSchema(RandomString name)
        : this(name, Random.Shared) { }

    public RandomSchema(RandomString name, Random random)
        : this(name, new RandomTablesCollection(random), random) { }

    public RandomSchema(RandomString name, RandomTablesCollection randomTables)
        : this(name, randomTables, Random.Shared) { }

    public RandomSchema(RandomTablesCollection randomTables)
        : this(randomTables, Random.Shared) { }

    public RandomSchema(RandomTablesCollection randomTables, Random random)
        : this(new RandomString(random), randomTables, random) { }

    public RandomSchema(
        RandomTablesCollection randomTables,
        RandomForeignKeysCollection randomForeignKeys
    )
        : this(randomTables, randomForeignKeys, Random.Shared) { }

    public RandomSchema(
        RandomTablesCollection randomTables,
        RandomForeignKeysCollection randomForeignKeys,
        Random random
    )
        : this(new RandomString(random), randomTables, randomForeignKeys) { }

    public RandomSchema(RandomForeignKeysCollection randomForeignKeys)
        : this(randomForeignKeys, Random.Shared) { }

    public RandomSchema(RandomForeignKeysCollection randomForeignKeys, Random random)
        : this(new RandomTablesCollection(random), randomForeignKeys, random) { }

    public RandomSchema(
        RandomString randomName,
        RandomForeignKeysCollection randomForeignKeys
    )
        : this(randomName, randomForeignKeys, Random.Shared) { }

    public RandomSchema(
        RandomString randomName,
        RandomForeignKeysCollection randomForeignKeys,
        Random random
    )
        : this(randomName, new RandomTablesCollection(random), randomForeignKeys) { }

    public RandomSchema(
        RandomString name,
        RandomTablesCollection randomTables,
        Random random
    )
        : this(name, randomTables, new RandomForeignKeysCollection(random)) { }

    public RandomSchema(
        RandomString name,
        RandomTablesCollection tables,
        RandomForeignKeysCollection foreignKeys
    )
        // Stryker disable once linq
        : this(name, tables.AsEnumerable(), foreignKeys.AsEnumerable()) { }

    internal RandomSchema(
        IString name,
        IEnumerable<ITable> tables,
        IEnumerable<IForeignKey> foreignKeys
    )
        : this(
            name,
            new Lazy<IEnumerable<ITable>>(tables.ToArray),
            new Lazy<IEnumerable<IForeignKey>>(foreignKeys.ToArray)
        )
    { }

    private RandomSchema(
        IString name,
        Lazy<IEnumerable<ITable>> tables,
        Lazy<IEnumerable<IForeignKey>> foreignKeys
    )
    {
        Name = name;
        _tables = tables;
        _foreignKeys = foreignKeys;
    }

    public IString Name { get; }

    public IEnumerable<ITable> Tables => _tables.Value;

    public IEnumerable<IForeignKey> ForeignKeys => _foreignKeys.Value;

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
