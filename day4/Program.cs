using System.Text.RegularExpressions;

// Count the number of matches both forward and backward in this string
static int countXmasInString(string str)
{
    int numMatches = 0;

    Regex xmasRegex = new Regex("XMAS");
    MatchCollection xmasMatches = xmasRegex.Matches(str);
    numMatches += xmasMatches.Count;

    //str = str.Reverse().ToString();
    char[] charArray = str.ToCharArray();
    Array.Reverse(charArray);
    str = new string(charArray);

    xmasMatches = xmasRegex.Matches(str);
    numMatches += xmasMatches.Count;

    return numMatches;
}

string line;
int lineCounter = 0;
char[][] lines = null;
int numXmases = 0;
try
{
    StreamReader sr = new StreamReader(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\input");
    //StreamReader sr = new StreamReader(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\test\\simpleInput3.txt");

    line = sr.ReadLine();
    lines = new char[line.Length][];

    while (line != null)
    {
        lines[lineCounter] = new char[line.Length];

        for (int i = 0; i < line.Length; i++)
        {
            char ch = line[i];
            lines[lineCounter][i] = ch;
        }

        lineCounter++;
        line = sr.ReadLine();
    }

    sr.Close();
}
catch (Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}

// Check for vertical matches
string currentLine;
for (int i = 0; i < lines[0].Length; i++)
{
    currentLine = "";
    for (int j = 0; j < lines[0].Length; j++)
        currentLine += lines[i][j];

    int verticalMatches = countXmasInString(currentLine);
    //if (verticalMatches > 0) Console.WriteLine("Found " + verticalMatches + " matches vertically in line " + i);
    numXmases += verticalMatches;
}

// Check for horizontal matches
for (int i = 0;i < lines[0].Length; i++)
{
    currentLine = "";
    for (int j = 0; j < lines[0].Length; j++)
        currentLine += lines[j][i];

    int horizontalMatches = countXmasInString(currentLine);
    //if (horizontalMatches > 0) Console.WriteLine("Found " + horizontalMatches + " matches horizontally in line " + i);
    numXmases += horizontalMatches;
}

// Check for diagonals - descending left-to-right
bool isFirstLine = true;
for (int i = 0; i < lines[0].Length; i++)
{
    currentLine = "";
    for (int j = 0; j < lines[0].Length; j++)
        if (i + j < lines[0].Length)
            currentLine += lines[i + j][j];

    int leftRightDiagonalMatches = countXmasInString(currentLine);
    //if (leftRightDiagonalMatches > 0) Console.WriteLine("Found " + leftRightDiagonalMatches + " matches in left-right diagonal in line " + i);
    numXmases += countXmasInString(currentLine);

    if (!isFirstLine)
    {
        currentLine = "";
        for (int j = 0; j < lines[0].Length; j++)
            if (i + j < lines[0].Length)
                currentLine += lines[j][i + j];

        leftRightDiagonalMatches = countXmasInString(currentLine);
        //if (leftRightDiagonalMatches > 0) Console.WriteLine("Found " + leftRightDiagonalMatches + " matches in left-right diagonal in line " + i);
        numXmases += leftRightDiagonalMatches;
    }

    isFirstLine = false;
}

// Check for diagonals - descending right-to-left
isFirstLine = true;
for (int i = 0; i < lines[0].Length; i++)
{
    currentLine = "";
    for (int j = 0; j < lines[0].Length; j++)
        if (i + j < lines[0].Length)
            currentLine += lines[(lines[0].Length - i) - j - 1][j];

    int rightLeftDiagonalMatches = countXmasInString(currentLine);
    //if (rightLeftDiagonalMatches > 0) Console.WriteLine("Found " + rightLeftDiagonalMatches + " matches in the right-left diagonal in line " + i);
    numXmases += countXmasInString(currentLine);

    if (!isFirstLine)
    {
        currentLine = "";
        for (int j = 0; (j + i) < lines[0].Length; j++)
            if (i + j < lines[0].Length)
                currentLine += lines[lines[0].Length - j - 1][i + j];

        rightLeftDiagonalMatches = countXmasInString(currentLine);
        //if (rightLeftDiagonalMatches > 0) Console.WriteLine("Found " + rightLeftDiagonalMatches + " matches in right-left diagonal iin line " + i);
        numXmases += rightLeftDiagonalMatches;
    }

    isFirstLine = false;
}

// Part 2 - check for XMAS in tree format
int numXmasCrosses = 0;
for (int i=0; i < lines[0].Length - 2;i++)
{
    for (int j = 0; j < lines[0].Length - 2; j++)
    {
        string leftX = lines[i][j].ToString() + lines[i + 1][j + 1] + lines[i + 2][j + 2];
        string rightX = lines[i][j + 2].ToString() + lines[i + 1][j + 1] + lines[i + 2][j];

        if ((leftX == "MAS" || leftX == "SAM") && (rightX == "MAS" || rightX == "SAM"))
            numXmasCrosses++;
    }
}


Console.WriteLine("Number of XMAS matches: " + numXmases);
Console.WriteLine("Number of XMAS crosses: " + numXmasCrosses);

