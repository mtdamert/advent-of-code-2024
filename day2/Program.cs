
// Put numbers from input file into a set of lists
using System.Globalization;

static bool IsLineSafe(int[] numbers)
{
    bool isSafe = true;

    // Check whether the line is safe
    if (numbers[0] < numbers[1]) // ascending
    {
        for (int i = 0; i < (numbers.Length - 1); i++)
        {
            if (!(numbers[i] < numbers[i + 1] && (numbers[i + 1] - numbers[i]) >= 1 && (numbers[i + 1] - numbers[i]) <= 3))
            {
                isSafe = false;
            }
        }
    }
    else // descending
    {
        for (int i = 0; i < (numbers.Length - 1); i++)
        {
            if (!(numbers[i] > numbers[i + 1] && (numbers[i] - numbers[i + 1]) >= 1 && (numbers[i] - numbers[i + 1]) <= 3))
                isSafe = false;
        }
    }

    return isSafe;
}

string line;
int numSafeReports = 0;
int numSafeDampenedReports = 0;
try
{
    StreamReader sr = new StreamReader(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\input");

    line = sr.ReadLine();
    while (line != null)
    {
        // Process line
        if (line.Contains(" "))
        {
            string[] numbersStrings = line.Split(" ");
            int[] numbers = new int[numbersStrings.Length];
            for (int i = 0; i<numbersStrings.Length; i++)
            {
                numbers[i] = int.Parse(numbersStrings[i]);
            }

            if (IsLineSafe(numbers))
            {
                numSafeReports++;
                numSafeDampenedReports++;
            }
            else
            {
                // Line is not safe. Is it safe when dampened?
                // Test all possible versions of this line that are missing a single number
                for (int i = 0; i < numbers.Length; i++)
                {
                    List<int> dampenedNumbers = new List<int>();

                    for (int j = 0; j < numbers.Length; j++)
                    {
                        if (i != j)
                            dampenedNumbers.Add(numbers[j]);
                    }

                    if (IsLineSafe(dampenedNumbers.ToArray()))
                    {
                        numSafeDampenedReports++;
                        break;
                    }
                }

            }
        }

        line = sr.ReadLine();
    }

    sr.Close();
}
catch (Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}

Console.WriteLine("Number of safe reports: " + numSafeReports);
Console.WriteLine("Number of safe dampened reports: " + numSafeDampenedReports);
