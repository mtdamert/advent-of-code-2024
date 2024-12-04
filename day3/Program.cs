﻿using System.Text.RegularExpressions;

string line;
int counter = 0;
int totalResults = 0;
try
{
    StreamReader sr = new StreamReader(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\input");

    line = sr.ReadLine();
    while (line != null)
    {
        counter++;

        Regex validInstructionRegex = new Regex("mul\\([0-9]{1,3},[0-9]{1,3}\\)");
        MatchCollection validInstructionMatches = validInstructionRegex.Matches(line);

        // Process line
        if (validInstructionMatches.Count > 0)
        {
            foreach (Match match in validInstructionMatches)
            {
                string matchNumbers = match.Value.Substring(4, match.Value.Length - 5);

                string[] numbers = matchNumbers.Split(",");
                totalResults += Convert.ToInt32(numbers[0]) * Convert.ToInt32(numbers[1]);
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

Console.WriteLine("Total of results: " + totalResults);