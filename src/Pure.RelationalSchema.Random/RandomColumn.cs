using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Cached.String;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.ColumnType;

namespace Pure.RelationalSchema.Random;

public sealed record RandomColumn : IColumn
{
    public RandomColumn()
        : this(new System.Random()) { }

    public RandomColumn(System.Random random)
        : this(
            new CachedString(new RandomString(new RandomUShort(new MinUshort(), new UShort(1000), random), random)),
            new RandomColumnType(random)
        )
    { }

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
