using System.Collections.Immutable;

namespace AdventOfCode2023;

public static class Day05
{
    public static void Run(string input)
    {
        using var lines = InputHelper.EnumerateLines(input).GetEnumerator();

        lines.MoveNext();
        var seeds = lines.Current
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Skip(1).Select(ulong.Parse).ToImmutableArray();
        lines.MoveNext();

        var seedToSoil = ParseMap("seed-to-soil map:", lines);
        var soilToFertilizer = ParseMap("soil-to-fertilizer map:", lines);
        var fertilizerToWater = ParseMap("fertilizer-to-water map:", lines);
        var waterToLight = ParseMap("water-to-light map:", lines);
        var lightToTemperature = ParseMap("light-to-temperature map:", lines);
        var temperatureToHumidity = ParseMap("temperature-to-humidity map:", lines);
        var humidityToLocation = ParseMap("humidity-to-location map:", lines);

        Part1();
        Part2();

        void Part2()
        {
            var locations = SeedRangeToLocationRange(SeedGroups(seeds)).ToArray();

            Console.WriteLine(locations.Min(r => r.Start));

            IEnumerable<Range> SeedGroups(ImmutableArray<ulong> ranges)
            {
                for (var i = 0; i < ranges.Length; i += 2)
                {
                    yield return new(ranges[i], ranges[i + 1]);
                }
            }
        }

        void Part1()
        {
            var locations = new List<ulong>();

            foreach (var seed in seeds)
            {
                locations.Add(SeedToLocation(seed));
            }

            Console.WriteLine(locations.Min());
        }

        ulong SeedToLocation(ulong seed)
        {
            var soil = seedToSoil.Get(seed);
            var fertilizer = soilToFertilizer.Get(soil);
            var water = fertilizerToWater.Get(fertilizer);
            var light = waterToLight.Get(water);
            var temperature = lightToTemperature.Get(light);
            var humidity = temperatureToHumidity.Get(temperature);
            return humidityToLocation.Get(humidity);
        }

        IEnumerable<Range> SeedRangeToLocationRange(IEnumerable<Range> seed)
        {
            var soil = seedToSoil.Get(seed).ToArray();
            var fertilizer = soilToFertilizer.Get(soil).ToArray();
            var water = fertilizerToWater.Get(fertilizer).ToArray();
            var light = waterToLight.Get(water).ToArray();
            var temperature = lightToTemperature.Get(light).ToArray();
            var humidity = temperatureToHumidity.Get(temperature).ToArray();
            return humidityToLocation.Get(humidity).ToArray();
        }
    }

    private static Map ParseMap(string name, IEnumerator<string> lines)
    {
        if (lines.Current != name)
        {
            throw new InvalidOperationException();
        }

        var result = new Map();
        Span<System.Range> ranges = stackalloc System.Range[3];
        while (lines.MoveNext() && !lines.Current.EndsWith(':'))
        {
            var line = lines.Current.AsSpan();
            line.Split(ranges, ' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            result.Define(ulong.Parse(line[ranges[1]]), ulong.Parse(line[ranges[0]]), ulong.Parse(line[ranges[2]]));
        }
        return result;
    }

    private sealed class Map
    {
        private readonly List<(Range Source, ulong TargetStart)> map = [];

        public ulong Get(ulong input)
        {
            foreach (var (range, target) in map)
            {
                if (range.Contains(input))
                {
                    return target + input - range.Start;
                }
            }

            return input;
        }

        public IEnumerable<Range> Get(IEnumerable<Range> input)
        {
            foreach (var range in input)
            {
                var remainder = range;
                while (true)
                {
                    var (result, rem) = GetSingle(remainder);
                    yield return result;
                    if (rem is null)
                    {
                        break;
                    }
                    remainder = rem.Value;
                }
            }


            (Range mapped, Range? remainding) GetSingle(Range input)
            {
                var minDistance = ulong.MaxValue;
                foreach (var (range, target) in map)
                {
                    if (range.Contains(input.Start))
                    {
                        var dif = input.Start - range.Start;
                        if (range.Contains(input.EndInclusive))
                        {
                            return (new(target + dif, input.Count), null);
                        }

                        var mappedCount = range.Count - dif;
                        return (new(target + dif, mappedCount), new(input.Start + mappedCount, checked(input.Count - mappedCount)));
                    }

                    if (range.Start < input.Start)
                    {
                        continue;
                    }

                    var dis = range.Start - input.Start;
                    if (dis < minDistance)
                    {
                        minDistance = dis;
                    }
                }

                if (input.Count <= minDistance)
                {
                    return (input, null);
                }
                return (new(input.Start, minDistance), new(input.Start + minDistance, input.Count - minDistance));
            }
        }

        public void Define(ulong sourceStart, ulong destinationStart, ulong count)
        {
            map.Add((new(sourceStart, count), destinationStart));
        }
    }


    private record struct Range(ulong Start, ulong Count)
    {
        public ulong EndInclusive => Start + Count - 1;
        public readonly bool Contains(ulong value)
        {
            var distance = value - Start;
            return distance < Count && distance >= 0;
        }
    }
}