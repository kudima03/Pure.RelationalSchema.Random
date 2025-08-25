using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.ColumnType;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomColumn : IColumn
{
    public RandomColumn()
        : this(Random.Shared) { }

    public RandomColumn(Random random)
        : this(
            new RandomString(new RandomUShort(random), random),
            new RandomColumnType(random)
        )
    { }

    public RandomColumn(RandomColumnType type)
        : this(type, Random.Shared) { }

    public RandomColumn(RandomColumnType type, Random random)
        : this(new RandomString(new RandomUShort(random), random), type) { }

    public RandomColumn(RandomString name)
        : this(name, Random.Shared) { }

    public RandomColumn(RandomString name, Random random)
        : this(name, new RandomColumnType(random)) { }

    public RandomColumn(RandomString name, RandomColumnType type)
        : this((IString)name, type) { }

    private RandomColumn(IString name, IColumnType type)
    {
        Name = name;
        Type = type;
    }

    public IString Name { get; }

    public IColumnType Type { get; }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
