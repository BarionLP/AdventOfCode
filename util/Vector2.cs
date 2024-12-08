namespace AdventOfCode.Util;

public readonly record struct Vector2(int X, int Y)
{
    public int MagnitudeSqr => X * X + Y * Y;
    public static Vector2 operator +(Vector2 left, Vector2 right)
        => new(left.X + right.X, left.Y + right.Y);
    public static Vector2 operator -(Vector2 left, Vector2 right)
        => new(left.X - right.X, left.Y - right.Y);
}
