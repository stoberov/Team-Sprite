using System;

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

        Console.WriteLine("Test");
    }
}