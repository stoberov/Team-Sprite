﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;

public class Program
{
    private struct Holes
    {
        public int x;
        public int y;
        public string symbol;
        public ConsoleColor color;
    }

    // Variables
    private static string[] playlist = {
                                   @"..\..\purple-hills-short.wav",
                                        @"..\..\house_impact.wav",
                                        @"..\..\technology.wav"
                               };

    private static SoundPlayer backgroundMusic;
    private static bool mainMenu = true;
    private static bool exitGame = false;


    static void PrintStringOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(str);
    }
    static void Main()
    {
        InitialSetUp();
        PrintTelerikAcademyLogo();
        DrawInitialScreen();

        int livesCount = 5;
        int speed = 50;
        int acceleration = 50;

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
                PrintStringOnPosition(35, 25, "GAME OVER!!!", ConsoleColor.White);
                Console.WriteLine();
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
                otherCar.symbol = "(..)";

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

                if (livesCount <= 0)
                {
                    PrintStringOnPosition(35, 25, "GAME OVER!!!", ConsoleColor.Red);
                    PrintStringOnPosition(30, 26, "Press [enter] to exit", ConsoleColor.Red);
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                holes = newList;
                Console.Clear();

                PrintCar(car, y, x);

                foreach (Holes hole in holes)
                {
                    PrintHole(hole.x, hole.y, hole.symbol, hole.color);
                }

                PrintStringOnPosition(35, 4, "Lives: " + livesCount, ConsoleColor.White);
                PrintStringOnPosition(35, 5, "Speed: " + speed, ConsoleColor.White);
                PrintStringOnPosition(30, 6, "Acceleration: " + acceleration, ConsoleColor.White);

                Thread.Sleep(100);

                //Hide cursor
                Console.CursorVisible = false;

            }
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



    private static void PrintHole(int x, int y, string symbol, ConsoleColor color)
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