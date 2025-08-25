using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.ColumnType;

namespace Pure.RelationalSchema.Random;

public sealed record RandomColumnType : IColumnType
{
    public RandomColumnType()
        : this(new System.Random()) { }

    public RandomColumnType(System.Random random)
        : this(new RandomString(new RandomUShort(random))) { }

    private RandomColumnType(IString name)
    {
        Name = name;
    }

    public IString Name { get; }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
