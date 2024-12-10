
const int DIRECTION_UP = 1;
const int DIRECTION_RIGHT = 2;
const int DIRECTION_DOWN = 3;
const int DIRECTION_LEFT = 4;

void GetGuardPos(char[][] map, ref int xPos, ref int yPos, ref int direction)
{
    xPos = -1;
    yPos = -1;

    for (int i = 0; i < map[0].Length; i++)
    {
        for (int j = 0; j < map[0].Length; j++)
        {
            if (map[i][j] == '^')
            {
                xPos = j;
                yPos = i;
                direction = DIRECTION_UP;
            }
            else if (map[i][j] == '>')
            {
                xPos = j;
                yPos = i;
                direction = DIRECTION_RIGHT;
            }
            else if (map[i][j] == 'v')
            {
                xPos = j;
                yPos = i;
                direction = DIRECTION_DOWN;
            }
            else if (map[i][j] == '<')
            {
                xPos = j;
                yPos = i;
                direction = DIRECTION_LEFT;
            }
        }
    }
}

string line;
int lineCounter = 0; 
char[][] map = null;
int numPositions = 1;
try
{
    StreamReader sr = new StreamReader(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\input");
    //StreamReader sr = new StreamReader(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\test\\simpleInput.txt");

    line = sr.ReadLine();
    map = new char[line.Length][];

    while (line != null)
    {
        map[lineCounter] = new char[line.Length];

        for (int i = 0; i < line.Length; i++)
        {
            char ch = line[i];
            map[lineCounter][i] = ch;
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

int x = -1, y = -1, direction = -1;
GetGuardPos(map, ref x, ref y, ref direction);

bool finished = false;
while (!finished)
{
    //for (int i = 0; i < map[0].Length; i++)
    //{
    //    for (int j = 0; j < map[0].Length; j++)
    //    {
    //        Console.Write(map[i][j]);
    //    }
    //    Console.WriteLine();
    //}
    //Console.WriteLine();

    if (direction == DIRECTION_UP)
    {
        if (y == 0)
        {
            finished = true;
        }
        else if (map[y - 1][x] != '#')
        {
            //map[y][x] = '.';
            if (map[y - 1][x] == '.') numPositions++;
            map[y - 1][x] = '^';
            y--;
        }
        else
        {
            map[y][x] = '>';
            direction = DIRECTION_RIGHT;
        }
    }
    else if (direction == DIRECTION_RIGHT)
    {
        if (x == (map[0].Length - 1))
        {
            finished = true;
        }
        else if (map[y][x + 1] != '#')
        {
            //map[y][x] = '.';
            if (map[y][x + 1] == '.') numPositions++;
            map[y][x + 1] = '>';
            x++;
        }
        else
        {
            map[y][x] = 'v';
            direction = DIRECTION_DOWN;
        }
    }
    else if (direction == DIRECTION_DOWN)
    {
        if (y == (map[0].Length - 1))
        {
            finished = true;
        }
        else if (map[y + 1][x] != '#')
        {
            //map[y][x] = '.';
            if (map[y + 1][x] == '.') numPositions++;
            map[y + 1][x] = 'v';
            y++;
        }
        else
        {
            map[y][x] = '<';
            direction = DIRECTION_LEFT;
        }
    }
    else if (direction == DIRECTION_LEFT)
    {
        if (x == 0)
        {
            finished = true;
        }
        else if (map[y][x - 1] != '#')
        {
            //map[y][x] = '.';
            if (map[y][x - 1] == '.') numPositions++;
            map[y][x - 1] = '<';
            x--;
        }
        else
        {
            map[y][x] = '^';
            direction = DIRECTION_UP;
        }
    }
}


Console.WriteLine("Exited map from (" + x + ", " + y + "). Total positions: " + numPositions);
