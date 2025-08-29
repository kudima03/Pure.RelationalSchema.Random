using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.ForeignKey;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.Abstractions.Table;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomSchemasCollection : IEnumerable<ISchema>
{
    private readonly INumber<ushort> _count;

    private readonly IEnumerable<IString> _names;

    private readonly IEnumerable<IEnumerable<ITable>> _tables;

    private readonly IEnumerable<IEnumerable<IForeignKey>> _foreignKeys;

    public RandomSchemasCollection()
        : this(Random.Shared) { }

    public RandomSchemasCollection(Random random)
        : this(new RandomUShort(random), random) { }

    public RandomSchemasCollection(INumber<ushort> count)
        : this(count, Random.Shared) { }

    public RandomSchemasCollection(INumber<ushort> count, Random random)
        : this(
            count,
            new RandomStringCollection(count, new RandomUShort(random), random),
            Enumerable
                .Range(0, count.NumberValue)
                .Select(_ => new RandomTablesCollection(count, random)),
            Enumerable
                .Range(0, count.NumberValue)
                .Select(_ => new RandomForeignKeysCollection(count, random))
        )
    { }

    public RandomSchemasCollection(
        INumber<ushort> count,
        RandomStringCollection randomNames,
        IEnumerable<RandomTablesCollection> randomTables,
        IEnumerable<RandomForeignKeysCollection> randomForeignKeys
    )
        // Stryker disable once linq
        : this(
            count,
            randomNames.AsEnumerable(),
            randomTables.AsEnumerable(),
            randomForeignKeys.AsEnumerable()
        )
    { }

    private RandomSchemasCollection(
        INumber<ushort> count,
        IEnumerable<IString> names,
        IEnumerable<IEnumerable<ITable>> tables,
        IEnumerable<IEnumerable<IForeignKey>> foreignKeys
    )
    {
        _count = count;
        _names = names;
        _tables = tables;
        _foreignKeys = foreignKeys;
    }

    public IEnumerator<ISchema> GetEnumerator()
    {
        using IEnumerator<IString> namesEnumerator = _names.GetEnumerator();

        using IEnumerator<IEnumerable<ITable>> tablesEnumerator = _tables.GetEnumerator();

        using IEnumerator<IEnumerable<IForeignKey>> foreignKeysEnumerator =
            _foreignKeys.GetEnumerator();

        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return !namesEnumerator.MoveNext()
            || !tablesEnumerator.MoveNext()
            || !foreignKeysEnumerator.MoveNext()
                ? throw new ArgumentException()
                : new RandomSchema(
                    namesEnumerator.Current,
                    tablesEnumerator.Current,
                    foreignKeysEnumerator.Current
                );
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
