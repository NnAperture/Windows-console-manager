using System.Runtime.InteropServices;
using System.Security.Principal;
using static Filer;
using static Help;
using static FileSystem;
using static Settings;
using static Program;
using System.Diagnostics;
using System.ComponentModel.Design;
class Handler
{
    Help help = new Help();
    FileSystem fs = new FileSystem();
    Settings settings = new Settings();
    public Variabler variabler = new Variabler();
    Buffer buffer = new Buffer();
    Conditional conditional = new Conditional();


    public bool Handle(string input)
    {
        List<string> sp = new List<string>();
        bool str = false;
        string word = "";
        char last = ' ';
        bool var = false;
        string vir = "";
        bool changed = false;
        foreach(char ch in input)
        {
            if (var)
            {
                vir += ch;
                if (last == '\\' & ch == '\\') { vir = vir.Substring(0, vir.Length - 1); last = ' '; }
                if (ch == ']')
                {
                    if(last == '\\')
                    {
                        vir = vir.Substring(0, vir.Length + 2) + ']';
                    }
                    else
                    {
                        if(Variabler.variables.ContainsKey(vir.Substring(1, vir.Length - 2)))
                        {
                            word += Variabler.variables[vir.Substring(1, vir.Length - 2)];
                            vir = "";
                            changed = true;
                        }
                        else
                        {
                            word += vir;
                            vir = "";
                        }
                        var = false;
                    }
                }
            }
            else
            {
                if ((!str) & ch == ' ' & word != "")
                {
                    sp.Add(word);
                    word = "";
                }
                else
                {
                    word += ch;
                }
                if (ch == '"' & last != '\\')
                {
                    str = !str;
                }
                if (ch == '"' & last == '\\')
                {
                    word = word.Substring(0, word.Length - 2) + '"';
                    last = ' ';
                }
                if (last == '\\' & ch == '\\')
                {
                    last = ' ';
                    word = word.Substring(0, word.Length - 1);
                }
                if (last != '\\' & ch == '[')
                {
                    var = true;
                    vir = "[";
                    word = word.Substring(0, word.Length - 1);
                }
            }
            last = ch;
        }
        if(word != "") { sp.Add(word + vir); }
        if (changed) { print("Some variables inserted to your comand: " + string.Join(" ", sp)); }

        if (sp.Count == 0)
        {
            return (true);
        }
        else
        {
            if(sp.Count == 1)
            {
                return Executer(sp[0], new string[0]);
            }
            else
            {
                return Executer(sp[0], sp[1..].ToArray());
            }
        }
    }
    public bool Executer(string comand, string[] args)
    {
        help.Check(comand);
        fs.Check(comand, args);
        settings.Check(comand, args);
        variabler.Check(comand, args);
        buffer.Check(comand, args);
        conditional.Check(comand, args);

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