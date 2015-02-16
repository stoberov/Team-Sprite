using System;
using System.IO;
using System.Threading;

class Program
{
    static void Main()
    {
        const int winWidth = 80;
        const int winHeight = 40;

        //Setting console borders
        Console.SetWindowSize(winWidth, winHeight);

        //Remove Scrolls
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;

        //Hide cursor
        Console.CursorVisible = false;

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
    }
}