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
<<<<<<< HEAD
        //Test vvn052
        //Test vvn053
        //Hide cursor
        Console.CursorVisible = false;
        Console.WriteLine();
        
=======

        //Logo
>>>>>>> origin/master
        PrintTelerikAcademyLogo();

        //The car
        string[] car = {   "[]|[]",
                               "  |  ",
                              "[]|[]" };

        int y = Console.WindowHeight - car.Length;
        int x = 10;

        while (true)
        {
            Console.Clear();

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressed = Console.ReadKey(true);
                while (Console.KeyAvailable) Console.ReadKey(true);
                if (pressed.Key == ConsoleKey.LeftArrow)
                {
                    if (x > 0) --x;
                }
                else if (pressed.Key == ConsoleKey.RightArrow)
                {
                    if (x < Console.WindowWidth - car.Length * 2) ++x;
                }
            }
            PrintCar(car, y, x);
            Thread.Sleep(100);

            //Hide cursor
            Console.CursorVisible = false;


        }
    }

    private static void PrintCar(string[] car, int y, int x)
    {
        for (int i = 0; i < car.Length; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write(car[i]);
        }
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

            Console.Clear();
        }

        // Pause the logo for 3secs
        Thread.Sleep(300);

        // Clean the console to begin the game
        Console.Clear();

    }


}
