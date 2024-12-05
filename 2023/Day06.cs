namespace AdventOfCode2023;

public static class Day06
{
    public static void Run(string input)
    {
        var splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
        var lines = input.Split('\n', splitOptions);

        Part1();
        Part2();

        void Part1()
        {
            var times = lines[0].Split(' ', splitOptions).Skip(1).Select(int.Parse);
            var distances = lines[1].Split(' ', splitOptions).Skip(1).Select(int.Parse);

            var result = 1;
            foreach (var (duration, record) in times.Zip(distances))
            {
                result *= PossibleWins(duration, record);
            }

            Console.WriteLine(result);
        }

        void Part2()
        {
            var duration = long.Parse(lines[0].Replace("Time:", "").Replace(" ", ""));
            var record = long.Parse(lines[1].Replace("Distance:", "").Replace(" ", ""));

            Console.WriteLine(PossibleWins(duration, record));
        }

        static int PossibleWins(long duration, long record)
        {
            var count = 0;
            for (var timePressed = 1L; timePressed < duration; timePressed++)
            {
                var speed = timePressed;
                var timeLeft = duration - timePressed;
                var distance = speed * timeLeft;

                if (distance > record)
                {
                    count++;
                }
            }
            return count;
        }
    }
}