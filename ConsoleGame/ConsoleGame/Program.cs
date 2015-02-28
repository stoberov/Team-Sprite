using System;
using System.IO;
using System.Threading;

class Program
{
    static void Main()
    {
        //User can setup the size of window
        Console.Write("Enter the size of the game window\nEnter the Hight of the window - 15 is minimum, 84 is maximum: ");
        int wHight = 15;
        string consoleInputWH = null;
        while ((!int.TryParse(consoleInputWH, out wHight)) || (wHight > 84) || (wHight < 15))
        {
            Console.Write("Enter the Hight of the window - 15 is minimum, 84 is maximum: ");
            consoleInputWH = Console.ReadLine();
        }
        Console.WindowHeight = wHight;
        Console.BufferHeight = Console.WindowHeight;
        Console.Write("Enter the Width of the window - 60 is minimum, 240 is maximum: ");
        int wWidth = 70;
        string consoleInputWW = Console.ReadLine();
        while ((!int.TryParse(consoleInputWW, out wWidth)) || (wWidth > 240) || (wWidth < 60))
        {
            Console.Write("Enter the Hight of the window - 60 is minimum, 240 is maximum: ");
            consoleInputWW = Console.ReadLine();
        }
        Console.WindowWidth = wWidth;
        Console.BufferWidth = Console.WindowWidth;

        //Remove Scrolls
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;

        //Hide cursor
        Console.CursorVisible = false;
        Console.WriteLine();
        
        PrintTelerikAcademyLogo();
    }

    static void PrintTelerikAcademyLogo()
    {
        StreamReader logoTelerikAcademy = new StreamReader(@"..\..\telerik-logo.txt");

        using (logoTelerikAcademy)
        {
            string line = logoTelerikAcademy.ReadLine();
            int lineNum = 0;

            while (line != null)
            {
                Console.SetCursorPosition(15, lineNum);
                Console.ForegroundColor = ConsoleColor.Blue;

                // Print the logo slowly
                Thread.Sleep(100);
                Console.WriteLine(line);

                line = logoTelerikAcademy.ReadLine();
                lineNum++;
            }

            Console.SetCursorPosition(30, lineNum);
            Console.WriteLine("Telerik Academy");

            Console.SetCursorPosition(22, lineNum + 1);
            Console.WriteLine("A Console Game by Team Sprite");
        }

        // Pause the logo for 3secs
        Thread.Sleep(3000);

        // Clean the console to begin the game
        Console.Clear();
        Console.WriteLine("iliev");
    }
}
