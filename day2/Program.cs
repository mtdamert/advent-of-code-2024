
// Put numbers from input file into a set of lists
using System.Globalization;

string line;
int numSafeReports = 0;
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

            // Check whether the line is safe
            if (numbers[0] < numbers[1]) // ascending
            {
                bool isSafe = true;
                for (int i = 0; i < (numbers.Length - 1); i++)
                {
                    if (!(numbers[i] < numbers[i + 1] && (numbers[i + 1] - numbers[i]) >= 1 && (numbers[i + 1] - numbers[i]) <= 3))
                        isSafe = false;
                }

                if (isSafe)
                {
                    //Console.WriteLine("Safe ascending line: " + line);
                    numSafeReports++;
                }
            }
            else // descending
            {
                bool isSafe = true;
                for (int i = 0; i < (numbers.Length - 1); i++)
                {
                    if (!(numbers[i] > numbers[i + 1] && (numbers[i] - numbers[i + 1]) >= 1 && (numbers[i] - numbers[i + 1]) <= 3))
                        isSafe = false;
                }

                if (isSafe)
                {
                    //Console.WriteLine("Safe descending line: " + line);
                    numSafeReports++;
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
