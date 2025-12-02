namespace AdventOfCode.Util;

public static class Extensions
{
    extension(Range range)
    {
        public RangeEnumerator GetEnumerator() => new(range);
    }

    public record struct RangeEnumerator
    {
        // start INCLUSIVE - end EXCLUSIVE
        private int _current;
        private readonly int _end;
        public readonly int Current => _current;

        public RangeEnumerator(Range range)
        {
            var (offset, length) = range.GetOffsetAndLength(int.MaxValue);

            _current = offset - 1;
            _end = offset + length;
        }

        public bool MoveNext()
        {
            _current++;
            return _current < _end;
        }
    }
}
