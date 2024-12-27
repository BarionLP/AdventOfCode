namespace AdventOfCode2024;

public static class Day23
{
    public static void Run(string input)
    {
        var graph = new Dictionary<string, HashSet<string>>();
        foreach (var connection in InputHelper.EnumerateLines(input))
        {
            var parts = connection.Split('-');
            Debug.Assert(parts.Length == 2);
            Connect(parts[0], parts[1]);
        }

        var sets = new HashSet<HashSet<string>>(new HashSetElementComparer<string>());

        foreach (var (computer, connections) in graph.Where(p => p.Key.StartsWith('t')))
        {
            foreach (var connection in connections)
            {
                foreach (var connection2 in connections.Where(c => graph[c].Contains(connection)))
                {
                    if (connection != connection2)
                    {
                        sets.Add([computer, connection, connection2]);
                    }
                }
            }
        }

        Console.WriteLine(sets.Count);


        //Console.WriteLine(string.Join(", ", biggest.Value.Append(biggest.Key).Order()));

        void Connect(string a, string b)
        {
            AddConnection(a, b);
            AddConnection(b, a);
        }
        void FindLargestClique()
        {
            var maxClique = new HashSet<string>();
            foreach (var computer in graph.Keys)
            {
                var potentialClique = new HashSet<string> { computer };
                foreach (var other in graph.Keys)
                {
                    if (other != computer && IsConnectedToAll(other, potentialClique))
                    {
                        potentialClique.Add(other);
                    }
                }
                if (potentialClique.Count > maxClique.Count)
                {
                    maxClique = potentialClique;
                }
            }
            Console.WriteLine(string.Join(',', maxClique.Order()));
        }

        bool IsConnectedToAll(string computer, HashSet<string> others)
        {
            return others.All(other => graph[computer].Contains(other) && graph[other].Contains(computer));
        }

        FindLargestClique();

        void AddConnection(string computer, string other)
        {
            if (graph.TryGetValue(computer, out var set))
            {
                set.Add(other);
            }
            else
            {
                graph[computer] = [other];
            }
        }
    }

    private sealed class HashSetElementComparer<T> : IEqualityComparer<HashSet<T>>
    {
        public bool Equals(HashSet<T>? x, HashSet<T>? y)
        {
            return x is null ? y is null : y is not null && x.SetEquals(y);
        }

        public int GetHashCode(HashSet<T> obj)
        {
            return obj.Order().Aggregate(0, (hash, item) => HashCode.Combine(hash, item!.GetHashCode()));
        }
    }
}