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

    static void Main()
    {
        //User can setup the size of window

        Console.BufferHeight = Console.WindowHeight = 50;
        Console.BufferWidth = Console.WindowWidth = 50;

        //Remove Scrolls
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;


        //Hide cursor
        Console.CursorVisible = false;
        Console.WriteLine();

        //Play background music - songs by PlayOnLoop.com
        backgroundMusic = new SoundPlayer(playlist[0]);
        backgroundMusic.PlayLooping();

        //Logo
        PrintTelerikAcademyLogo();

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