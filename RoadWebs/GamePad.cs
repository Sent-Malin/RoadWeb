namespace RoadWebs;

public class GamePad
{
    private RoadWebs RoadWebs { get; set; }

    public GamePad()
    {
        RoadWebs = new RoadWebs();
    }

    public void StartGame()
    {
        PrintField();
        PrintInstruction();
        RoadWebs.RollDices();
        PrintAvailableRoadToSet();
        int numMove;
        string[] data;
        while (RoadWebs.setCountRounds > RoadWebs._roundNow)
        {
            Console.WriteLine("please enter yor move, type \'19\' for print instruction");
            numMove = int.Parse(Console.ReadLine());
            switch (numMove)
            {
                case 0:
                    PrintField();
                    break;
                case 1:
                    PrintAvailableRoadToSet();
                    break;
                case 2:
                    RoadWebs.RollDices();
                    RoadWebs._roundNow++;
                    break;
                case 3:
                    Console.WriteLine("enter number required available road");
                    RoadWebs.Rotate(int.Parse(Console.ReadLine()));
                    PrintAvailableRoadToSet();
                    break;
                case 4:
                    Console.WriteLine("enter x pos and y pos wished cell and number available road \n" +
                                      "format: x,y,number");
                    data = Console.ReadLine().Split(",");
                    RoadWebs.SetActiveSection(int.Parse(data[1]), int.Parse(data[0]), int.Parse(data[2]));
                    PrintField();
                    break;
                case 19:
                    PrintInstruction();
                    break;
                default: 
                    break;
            }
        }

        PrintField();
        Console.WriteLine("You finish the game! \n" +
                          "Here table for counting points for linked road webs: \n" +
                          "roads: | 2 | 3 |  4 |  5 |  6 |  7 |  8 |  9 | 10 | 11 | 12 |\n" +
                          "points:| 4 | 8 | 12 | 16 | 20 | 24 | 28 | 32 | 36 | 40 | 45 |\n" +
                          "Current record - 30");
        Console.Read();
    }

    public void PrintField()
    {
        int i;
        Console.Write(" ");
        for (i = 0; i < RoadWebs.field.Length; i++) 
        {
            Console.Write(i);
        }
        Console.WriteLine();
        i = 0;
        foreach (RoadWebs.Cell[] row in RoadWebs.field)
        {
            Console.Write(i); i++;
            foreach(RoadWebs.Cell cell in row)
            {
                Console.Write(cell.symbol + "");
            }
            Console.WriteLine();
        }
    }

    public void PrintAvailableRoadToSet()
    {
        foreach(RoadWebs.Cell cell in RoadWebs.cellsToBuild)
        {
            if (cell != null) 
            { 
                Console.Write(cell.symbol + " ");
            }
            else
            {
                Console.Write("[]" + " ");
            }
        }
    }

    public void PrintInstruction()
    {
        Console.WriteLine("\n Enter \n" +
                          "0 - print field \n" +
                          "1 - print road available to set \n" +
                          "2 - start new round, available road are drop \n" +
                          "3 - rotate available road \n" +
                          "4 - set up road \n");
    }
}
