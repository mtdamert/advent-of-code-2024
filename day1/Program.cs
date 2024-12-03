using System.IO;

string line;
List<int> list1 = new List<int>();
List<int> list2 = new List<int>();
int totalDistance = 0;
int similarityScore = 0;

// Put numbers from input file into two corresponding lists
try
{
    StreamReader sr = new StreamReader(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\input");

    line = sr.ReadLine();
    while (line != null)
    {
        // Process line
        if (line.Contains("   "))
        {
            string[] numbers = line.Split("   ");

            list1.Add(Convert.ToInt32(numbers[0]));
            list2.Add(Convert.ToInt32(numbers[1]));
        }

        line = sr.ReadLine();
    }

    sr.Close();
}
catch (Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}

// Get the similarity score of the lists
for (int i = 0; i < list1.Count; i++)
{
    int numAppearances = list2.FindAll(x => x == list1[i]).Count;
    if (numAppearances > 0) Console.WriteLine("Found a match of " + list1[i]);
    similarityScore += (list1[i] * numAppearances);
}

// Get the distance between the two lists
while (list1.Count > 0)
{
    totalDistance += Math.Abs(list1.Min() - list2.Min());

    list1.Remove(list1.Min());
    list2.Remove(list2.Min());
}

Console.WriteLine("Total distance: " + totalDistance);
Console.WriteLine("Similarity score: " + similarityScore);
