
static int GetIndex(string[] updateSet, string rule)
{
    if (updateSet == null)
        return -1;

    for (int i = 0; i < updateSet.Length; i++)
    {
        if (updateSet[i] == rule)
            return i;
    }

    return -1;
}

string line;
List<string[]> pageOrderingRules = new List<string[]>();
List<string[]> updates = new List<string[]>();
int middlePageSum = 0;
int invalidMiddlePageSum = 0;
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
        reorderLine:

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
        middlePageSum += Convert.ToInt32(updateSet[updateSet.Length / 2]);
    }

    if (!isInCorrectOrder)
    {
        bool orderChanged = false;
        do
        {
            orderChanged = false;
            for (int ruleNumber = 0; ruleNumber<pageOrderingRules.Count; ruleNumber++)
            {
            string[] rule = pageOrderingRules[ruleNumber];

                // If both halves of a rule exist in this update set, make sure they're in the correct order
                int index0 = GetIndex(updateSet, rule[0]);
                int index1 = GetIndex(updateSet, rule[1]);
                if (index0 != -1 && index1 != -1 && index0 > index1)
                {
                    string tempUpdate = updateSet[index0];
                    updateSet[index0] = updateSet[index1];
                    updateSet[index1] = tempUpdate;
                    orderChanged = true;
                }
            }
        } while (orderChanged);

        invalidMiddlePageSum += Convert.ToInt32(updateSet[updateSet.Length / 2]);
    }
}

Console.WriteLine("Sum of middle numbers in valid updates: " + middlePageSum);
Console.WriteLine("Sum of middle numbers in re-ordered invalid updates: " + invalidMiddlePageSum);
