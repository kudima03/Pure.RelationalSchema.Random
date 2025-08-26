using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.ColumnType;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomColumnType : IColumnType
{
    public RandomColumnType()
        : this(Random.Shared) { }

    public RandomColumnType(Random random)
        : this(new RandomString(random)) { }

    public RandomColumnType(RandomString name)
        : this((IString)name) { }

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
