using System.Runtime.InteropServices;
using System.Security.Principal;
using static Filer;
using static Help;
using static FileSystem;
using static Settings;
using static Program;
using System.Diagnostics;
class Handler
{
    Help help = new Help();
    FileSystem fs = new FileSystem();
    Settings settings = new Settings();

    public bool Handle(string input)
    {
        string[] sp = input.Split();
        if (sp.Length == 0)
        {
            return (true);
        }
        else
        {
            if(sp.Length == 1)
            {
                return Executer(sp[0], new string[0]);
            }
            else
            {
                return Executer(sp[0], sp[1..]);
            }
        }
    }
    public bool Executer(string comand, string[] args)
    {
        help.Check(comand);
        fs.Check(comand, args);
        settings.Check(comand, args);

        if (comand == "exit")
        {
            return (exit_com(comand, args));
        }
        if (comand == "shutdown" & Settings.settings["shutdown_comand"] == "true")
        {
            shut_com(comand, args);
        }
        return (true);
    }

    bool exit_com(string comand, string[] args)
    {
        if(args.Contains("-nc") | args.Contains("-noconfirm") | Settings.settings["exit_noconfirm"] == "true")
        {
            return false;
        }
        bool y = false;
        exiter(y);
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.DownArrow | key.Key == ConsoleKey.S)
            {
                y = true;
            }
            if (key.Key == ConsoleKey.UpArrow | key.Key == ConsoleKey.W)
            {
                y = false;
            }
            if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                return (!y);
            }
            exiter(y);
        }
    }

    void exiter(bool y)
    {
        Console.Clear();
        for (int i = 0; i < 10; i++)
        {
            pr_cl();
        }
        print("      Do you really want to exit?");
        print();
        if (y)
        {
            pr_cl("     NO  ");
            pr_cl("    ", end: "");
            pr_cl("<YES>", bg: ConsoleColor.Green);
        }
        else
        {
            pr_cl("    ", end:"");
            pr_cl("<NO >", bg:ConsoleColor.Red);
            pr_cl("     YES ");
        }
        pr_cl();
    }

    void shut_com(string comand, string[] args)
    {
        if (args.Contains("-nc") | args.Contains("-noconfirm") | Settings.settings["shutdown_noconfirm"] == "true")
        {
            Process.Start("shutdown", "/s /t 0");
        }
        bool y = false;
        shutter(y);
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.DownArrow | key.Key == ConsoleKey.S)
            {
                y = true;
            }
            if (key.Key == ConsoleKey.UpArrow | key.Key == ConsoleKey.W)
            {
                y = false;
            }
            if (key.Key == ConsoleKey.Enter)
            {
                if (y)
                {
                    Process.Start("shutdown", "/s /t 0");
                }
                return;
            }
            shutter(y);
        }
    }
    void shutter(bool y)
    {
        Console.Clear();
        for (int i = 0; i < 10; i++)
        {
            pr_cl();
        }
        print("      Do you really want to shut down your computer?");
        print();
        if (y)
        {
            pr_cl("     NO  ");
            pr_cl("    ", end: "");
            pr_cl("<YES>", bg: ConsoleColor.Green);
        }
        else
        {
            pr_cl("    ", end: "");
            pr_cl("<NO >", bg: ConsoleColor.Red);
            pr_cl("     YES ");
        }
        pr_cl();
        print();
        print("      If you select YES, your computer will shut down");
    }
}