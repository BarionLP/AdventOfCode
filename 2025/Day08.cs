namespace AdventOfCode2025;

public static class Day08
{
    public static void RunExample() => Run("""
        162,817,812
        57,618,57
        906,360,560
        592,479,940
        352,342,300
        466,668,158
        542,29,236
        431,825,988
        739,650,466
        52,470,668
        216,146,977
        819,987,18
        117,168,530
        805,96,715
        346,949,466
        970,615,88
        941,993,340
        862,61,35
        984,92,344
        425,690,689
        """);
    public static async Task Run() => Run(await InputHelper.GetInput(2025, 8));

    public static void Run(ReadOnlySpan<char> input)
    {
        RunPart1(input);
    }

    public static void RunPart1(ReadOnlySpan<char> input)
    {
        var dictionary = new Dictionary<Vector3, HashSet<Vector3>>();

        foreach (var range in input.Split('\n'))
        {
            var point = Vector3.Parse(input[range].Trim());
            dictionary[point] = [];
        }

        foreach (var i in ..10)
        {
            (Vector3 a, Vector3 b) closest = default;
            var closestDistance = double.MaxValue;
            foreach (var p1 in dictionary.Keys)
            {
                foreach (var p2 in dictionary.Keys)
                {
                    if (p1 == p2) continue;

                    if (dictionary[p1].Contains(p2))
                    {
                        Debug.Assert(dictionary[p2].Contains(p1));
                        continue;
                    }

                    var distance = (p1 - p2).Magnitude;

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closest = (p1, p2);
                    }
                }
            }

            if (closestDistance < double.MaxValue)
            {
                dictionary[closest.a].Add(closest.b);
                dictionary[closest.b].Add(closest.a);
            }
        }

        var circuits = new List<HashSet<Vector3>>();

        foreach (var (p, connections) in dictionary)
        {
            bool added = false;
            foreach (var circuit in circuits)
            {
                if (circuit.Contains(p))
                {
                    added = true;
                    foreach (var c in connections) circuit.Add(c);
                }
            }
            if (!added)
            {
                circuits.Add([p, .. connections]);
            }
        }

        Console.WriteLine($"Part 1: {circuits.OrderByDescending(c => c.Count).Take(3).Aggregate(1L, (acc, c) => acc * c.Count)}");

    }
}