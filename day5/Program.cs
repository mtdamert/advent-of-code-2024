
string line;
List<string[]> pageOrderingRules = new List<string[]>();
List<string[]> updates = new List<string[]>();
int middlePageSum = 0;
try
{
    StreamReader sr = new StreamReader(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\input");
    //StreamReader sr = new StreamReader(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\test\\simpleInput.txt");

    line = sr.ReadLine();

    bool loadingUpdates = false;
    while (line != null)
    {
        if (line == string.Empty)
            loadingUpdates = true;

        if (line != string.Empty)
        {
            if (!loadingUpdates)
            {
                // These are separated based on the |
                string[] orderedPages = line.Split('|');
                pageOrderingRules.Add(orderedPages);
            }
            else
            {
                // These are separated based on the ,
                string[] updateList = line.Split(',');
                updates.Add(updateList);
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

foreach (string[] updateSet in updates)
{
    bool isInCorrectOrder = true;

    for (int i=0; i<updateSet.Length; i++)
    {
        string update = updateSet[i];

        // If this number appears as the second part of any rule, make sure the first part of the rule doesn't appear after it
        foreach (string[] rule in pageOrderingRules)
        {
            if (rule[1] == update)
            {
                for (int j=i + 1; j<updateSet.Length; j++)
                {
                    string laterUpdate = updateSet[j];
                    if (laterUpdate == rule[0])
                    {
                        isInCorrectOrder = false;
                    }
                }
            }
        }
    }

    if (isInCorrectOrder)
    {
        //Console.WriteLine("Line with middle of " + updateSet[updateSet.Length / 2] + " was valid");
        middlePageSum += Convert.ToInt32(updateSet[updateSet.Length / 2]);
    }
}

Console.WriteLine("Sum of middle numbers in valid updates: " + middlePageSum);
