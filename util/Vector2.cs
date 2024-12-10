namespace AdventOfCode.Util;

public readonly record struct Vector2(int X, int Y)
{
    public static Vector2 Up { get; } = new(0, -1);
    public static Vector2 Down { get; } = new(0, 1);
    public static Vector2 Right { get; } = new(1, 0);
    public static Vector2 Left { get; } = new(-1, 0);
    public int MagnitudeSqr => X * X + Y * Y;

    public static Vector2 operator +(Vector2 left, Vector2 right)
        => new(left.X + right.X, left.Y + right.Y);
    public static Vector2 operator -(Vector2 left, Vector2 right)
        => new(left.X - right.X, left.Y - right.Y);
}
