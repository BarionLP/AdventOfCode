using Ametrin.Optional;
using Ametrin.Optional.Operations;
using Ametrin.Optional.Parsing;

namespace AdventOfCode.Util;

[GenerateISpanParsable]
public readonly partial record struct Vector3(int X, int Y, int Z) : IComparable<Vector3>, IOptionSpanParsable<Vector3>
{
    public static Vector3 Up { get; } = new(0, -1, 0);
    public static Vector3 Down { get; } = new(0, 1, 0);
    public static Vector3 Right { get; } = new(1, 0, 0);
    public static Vector3 Left { get; } = new(-1, 0, 0);
    public int MagnitudeSqr => X * X + Y * Y + Z * Z;
    public double Magnitude => Math.Sqrt(MagnitudeSqr);

    public override string ToString() => $"({X}, {Y}, {Z})";
    public int CompareTo(Vector3 other) => MagnitudeSqr.CompareTo(other.MagnitudeSqr);


    public static Vector3 operator +(Vector3 left, Vector3 right)
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    public static Vector3 operator -(Vector3 left, Vector3 right)
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    public static Vector3 operator *(Vector3 vector, int scalar)
        => new(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
    public static Vector3 operator *(int scalar, Vector3 vector)
        => new(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
    public static Vector3 operator /(Vector3 vector, int scalar)
        => new(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
    public static Vector3 operator /(int scalar, Vector3 vector)
        => new(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);

    public static Vector3 operator %(Vector3 left, Vector3 right)
        => new(left.X % right.X, left.Y % right.Y, left.Z % right.Z);

    public static bool operator <(Vector3 left, Vector3 right) => left.CompareTo(right) < 0;
    public static bool operator >(Vector3 left, Vector3 right) => left.CompareTo(right) > 0;
    public static bool operator <=(Vector3 left, Vector3 right) => left.CompareTo(right) <= 0;
    public static bool operator >=(Vector3 left, Vector3 right) => left.CompareTo(right) >= 0;

    public static implicit operator Vector3((int, int, int) tuple) => new(tuple.Item1, tuple.Item2, tuple.Item3);

    public static Option<Vector3> TryParse(ReadOnlySpan<char> span, IFormatProvider? provider = null)
    {
        span = span.TrimStart('(');
        span = span.TrimEnd(')');

        Span<Range> ranges = stackalloc Range[4];

        var count = span.Split(ranges, ',', StringSplitOptions.TrimEntries);

        if (count is not 3) return default;

        return int.TryParse(span[ranges[0]], provider)
                .Join(int.TryParse(span[ranges[1]], provider))
                .Join(int.TryParse(span[ranges[2]], provider))
                .Map(static x => (Vector3)x);
    }
}