using System;
using System.IO;
using System.Threading;

class Program
{
    static void Main()
    {
        //User can setup the size of window
               
        Console.BufferHeight = Console.WindowHeight = 50;              
        Console.BufferWidth = Console.WindowWidth = 50;

        //Remove Scrolls
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;
        //Test vvn052
        //Test vvn053
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
