using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;

class Program
{
    struct Holes
    {
        public int x;
        public int y;
        public string symbol;
        public ConsoleColor color;
    }
    
    // Variables
    static string[] playlist = {
                                   @"..\..\purple-hills-short.wav",
                                        @"..\..\house_impact.wav",
                                        @"..\..\technology.wav"
                               };
    public static SoundPlayer backgroundMusic;
    public static bool mainMenu = true;
    public static bool exitGame = false;

    static void Main()
    {
        InitialSetUp();
        PrintTelerikAcademyLogo();
        DrawInitialScreen();

        //The car
        string[] car = { "     _ ",
                       "  0=[_]=0",
                         "    /T\\ " ,
                        "   |(o)|",
                     " []=\\_/=[]",
                        "   __V__",
                       "  '-----' " };

        int y = Console.WindowHeight - car.Length;
        int x = 10;

        Random cordinates = new Random();
        List<Holes> holes = new List<Holes>();
        int b = 0;

        while (true)
        {
            if (exitGame)
            {
                return;
            }

            Console.Clear();

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressed = Console.ReadKey(true);
                while (Console.KeyAvailable) Console.ReadKey(true);
                if (pressed.Key == ConsoleKey.LeftArrow)
                {

                    if (x > 5) --x;


                }
                else if (pressed.Key == ConsoleKey.RightArrow)
                {
                    if (x < Console.WindowWidth - 5 - car.Length * 2) ++x;
                }
            }

            if (b % 7 == 0)
            {
                Holes otherCar = new Holes();
                otherCar.color = ConsoleColor.Blue;
                otherCar.x = cordinates.Next(5, 45);
                otherCar.y = 10;
                otherCar.symbol = "O";

                holes.Add(otherCar);
            }
            b++;
            //Thread.Sleep(50);

            List<Holes> newList = new List<Holes>();
            for (int i = 0; i < holes.Count; i++)
            {
                Holes oldCar = holes[i];
                Holes newCar = new Holes();
                newCar.x = oldCar.x;
                newCar.y = oldCar.y + 1;
                newCar.symbol = oldCar.symbol;
                newCar.color = oldCar.color;
                if (newCar.y < 50)
                {
                    newList.Add(newCar);
                }

            }
            holes = newList;
            Console.Clear();

            PrintCar(car, y, x);
            //Thread.Sleep(100);


            foreach (Holes hole in holes)
            {
                PrintHole(hole.x, hole.y, hole.symbol, hole.color);
            }

            Thread.Sleep(100);

            //Hide cursor
            Console.CursorVisible = false;


        }
    }

    private static void InitialSetUp()
    {
        //Set window size
        Console.BufferHeight = Console.WindowHeight = 50;
        Console.BufferWidth = Console.WindowWidth = 80;

        //Remove Scrolls
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;

        //Hide cursor
        Console.CursorVisible = false;
        Console.WriteLine();

        //Play background music - songs by PlayOnLoop.com
        backgroundMusic = new SoundPlayer(playlist[0]);
        backgroundMusic.PlayLooping();
    }

    private static void PrintTelerikAcademyLogo()
    {
        StreamReader logoTelerikAcademy = new StreamReader(@"..\..\telerik-logo.txt");
        using (logoTelerikAcademy)
        {
            string line = logoTelerikAcademy.ReadLine();
            int lineNum = 0;
            while (line != null)
            {
                Console.SetCursorPosition(16, lineNum);
                Console.ForegroundColor = ConsoleColor.Red;

                // Print the logo slowly
                Thread.Sleep(100);

                Console.WriteLine(line);
                line = logoTelerikAcademy.ReadLine();
                lineNum++;
            }

            Console.SetCursorPosition(32, lineNum + 2);
            Console.WriteLine("Telerik Academy");
            Console.SetCursorPosition(26, lineNum + 3);
            Console.WriteLine("A Console Game by Team Sprite");
        }

        // Pause the logo for 3secs
        Thread.Sleep(3000);
    }

    private static void DrawInitialScreen()
    {
        // Clean the console to begin the game
        Console.Clear();

        StreamReader introMenu = new StreamReader(@"..\..\introMenu.txt");

        using (introMenu)
        {
            string line = introMenu.ReadLine();
            int lineNum = 0;

            while (line != null)
            {
                Console.SetCursorPosition(3, lineNum);
                Console.ForegroundColor = ConsoleColor.Green;

                // get the menu slowly printed on the console
                Thread.Sleep(10);
                Console.WriteLine(line);

                line = introMenu.ReadLine();
                lineNum++;
            }

            ConsoleKeyInfo pressedKey = Console.ReadKey(true);

            if (pressedKey.Key == ConsoleKey.E)
            {
                exitGame = true;
            }
            else if (pressedKey.Key == ConsoleKey.N)
            {
                mainMenu = false;
            }
        }
    }

    static void PrintHole(int x, int y, string symbol, ConsoleColor color)
    {

        Console.SetCursorPosition(x, y);
        Console.WriteLine(symbol);
    }

    private static void PrintCar(string[] car, int y, int x)
    {
        for (int i = 0; i < car.Length; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write(car[i]);
        }
    }
}