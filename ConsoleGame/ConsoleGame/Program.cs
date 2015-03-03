﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;

public class Program
{
    // Variables
    private static string[] playlist = 
    {
                                   @"..\..\purple-hills-short.wav",
                                        @"..\..\house_impact.wav",
                                        @"..\..\technology.wav"
    };

    private static SoundPlayer backgroundMusic;
    private static bool exitGame = false;
    private static bool mute = false;
    private static bool levelReset = false;
    private static int livesCount = 5;
    private static int score = 100;
    private static int speed = 0;
    private static DateTime start = DateTime.Now;
    private static Random cordinates = new Random();
    private static List<Holes> holes = new List<Holes>();

    // Methods
    private static void Main()
    {
        InitialSetUp();
        PrintTelerikAcademyLogo();
        DrawInitialScreen();

        // The car
        string[] car = 
        {
                           "      _ ",
                         "  []=[_]=[]",
                         "     /T\\ " ,
                         "    |(o)|",
                        "  []=\\_/=[]",
                         "    __V__",
                         "   '-----' " 
                       };
        int y = Console.WindowHeight - car.Length;
        int x = 10;

        while (true)
        {
            if (exitGame)
            {
                PrintStringOnPosition(30, 25, "You Failed Pesho!!!", ConsoleColor.White);
                Console.WriteLine();
                return;
            }

            if (levelReset)
            {
                start = DateTime.Now;
                levelReset = false;
                continue;
            }

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressed = Console.ReadKey(true);

                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }

                if (pressed.Key == ConsoleKey.LeftArrow)
                {
                    if (x > 0) 
                    {
                        x--;
                    }
                }
                else if (pressed.Key == ConsoleKey.RightArrow)
                {
                    if (x < (Console.WindowWidth - 30 - (car.Length * 2)))
                    {
                        x++;
                    }
                }
                else if (pressed.Key == ConsoleKey.E)
                {
                    exitGame = true;
                }
                else if (pressed.Key == ConsoleKey.R)
                {
                    levelReset = true;
                }
                else if (pressed.Key == ConsoleKey.M)
                {
                    backgroundMusic.Stop();
                    mute = true;
                }
                else if (pressed.Key == ConsoleKey.S)
                {
                    backgroundMusic.PlayLooping();
                    mute = false;
                }
                else if (pressed.Key == ConsoleKey.D1 ||
                     pressed.Key == ConsoleKey.D2 ||
                     pressed.Key == ConsoleKey.D0)
                {
                    PlaylistControls(pressed);
                }
            }

            Random numHoles = new Random();
            for (int i = 0; i < numHoles.Next(1, 3); i++)
            {
                Holes newHole = new Holes();
                newHole.X = cordinates.Next(0, 50);
                newHole.Y = 0;
                newHole.Symbol = "(!)";
                if (newHole.X % 9 == 0)
                {
                    holes.Add(newHole);
                }
            }

            // Thread.Sleep(50);
            List<Holes> newList = new List<Holes>();
            for (int i = 0; i < holes.Count; i++)
            {
                Holes oldHole = holes[i];
                Holes newHole = new Holes();
                newHole.X = oldHole.X;
                newHole.Y = oldHole.Y + 1;
                newHole.Symbol = oldHole.Symbol;
                newHole.Color = oldHole.Color;

                if (newHole.Y < Console.WindowHeight)
                {
                    newList.Add(newHole);
                }

                if (oldHole.Y == y && ((oldHole.X > x && oldHole.X < x + 10) || (x < oldHole.X + oldHole.Symbol.Length - 2 && x > oldHole.X)))
                {
                    livesCount--;
                    score = score - 100;
                    speed = speed - 50;
                }
                else
                {
                    speed++;
                    score++;
                }

                if (speed > 200)
                {
                    speed = 200;
                }
                else if (speed < 0)
                {
                    speed = 0;
                }

                if (livesCount < 0)
                {
                    GameOverScreen();
                }
            }

            holes = newList;

            Console.Clear();

            PrintCar(car, y, x);

            foreach (Holes hole in holes)
            {
                PrintHole(hole.X, hole.Y, hole.Symbol, hole.Color);
            }

            Infoboard(start);

            Thread.Sleep(250 - speed);
        }
    }

    private static void InitialSetUp()
    {
        // Set window size
        Console.BufferHeight = Console.WindowHeight = 50;
        Console.BufferWidth = Console.WindowWidth = 80;

        // Remove Scrolls
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;

        // Hide cursor
        Console.CursorVisible = false;
        Console.WriteLine();

        // Play background music - songs by PlayOnLoop.com
        try
        {
            backgroundMusic = new SoundPlayer(playlist[0]);
            backgroundMusic.PlayLooping();
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Only 3 songs in playlist");
        }
    }

    private static void PrintTelerikAcademyLogo()
    {
        try
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
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: The file containing the logo was not found in the program's directory. Please, search for the file in other directories or create a new one");
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
        }
    }

    private static void Infoboard(DateTime start)
    {
        PrintStringOnPosition(50, 0, "Score: " + score, ConsoleColor.Green);
        if (livesCount % 2 == 1)
        {
            PrintStringOnPosition(50, 1, "Lives: " + livesCount, ConsoleColor.Green);
        }

        if (livesCount % 2 == 0)
        {
            PrintStringOnPosition(50, 1, "Lives: " + livesCount, ConsoleColor.Red);
        }

        PrintStringOnPosition(50, 2, "Speed: " + speed, ConsoleColor.Green);

        TimeSpan time = DateTime.Now - start;

        Console.SetCursorPosition(50, 4);
        Console.WriteLine("Time Elapsed: {0:mm\\:ss}", time);
        PrintOnPosition(50, 6, "<R> Reset Level", ConsoleColor.Cyan);
        PrintOnPosition(50, 9, new string('-', 15), ConsoleColor.Cyan);
        PrintOnPosition(50, 10, mute ? "<S> Turn ON Music" : "<M> Mute Music", ConsoleColor.Cyan);
        PrintOnPosition(50, 11, new string('-', 15), ConsoleColor.Cyan);
        PrintOnPosition(50, 12, "Playlist", ConsoleColor.Cyan);
        PrintOnPosition(50, 13, "<0><1><2>", ConsoleColor.Cyan);
        PrintOnPosition(50, 14, new string('-', 15), ConsoleColor.Cyan);
        PrintOnPosition(50, 16, "<E> Exit Game", ConsoleColor.Cyan);
    }

    private static void PlaylistControls(ConsoleKeyInfo pressed)
    {
        backgroundMusic.Stop();
        backgroundMusic.SoundLocation = playlist[int.Parse(pressed.KeyChar.ToString())];
        backgroundMusic.PlayLooping();
    }

    private static void GameOverScreen()
    {
        PrintStringOnPosition(32, 24, "Your Score is: " + score, ConsoleColor.Red);
        PrintStringOnPosition(35, 25, "GAME OVER!!!", ConsoleColor.Red);
        PrintStringOnPosition(30, 26, "Press [enter] to exit", ConsoleColor.Red);
        Console.ReadLine();
        Environment.Exit(0);
    }

    private static void PrintStringOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(str);
    }

    private static void PrintOnPosition(int x, int y, string c, ConsoleColor color = ConsoleColor.White)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(c);
    }

    private static void PrintHole(int x, int y, string symbol, ConsoleColor color)
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.SetCursorPosition(x, y);
        Console.Write(symbol);
    }

    private static void PrintCar(string[] car, int y, int x)
    {
        for (int i = 0; i < car.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(x, y + i);
            Console.Write(car[i]);
        }
    }

    private struct Holes
    {
        public int X;
        public int Y;
        public string Symbol;
        public ConsoleColor Color;
    }
}
