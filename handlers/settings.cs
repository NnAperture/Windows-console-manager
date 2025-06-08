using System.Runtime.InteropServices;
using System.Xml.Linq;
using static Program;
public class Settings
{
    public Settings()
    {
        if (File.Exists("settings.conf"))
        {
            Filer file = new Filer("settings.conf");
            foreach(string str in file)
            {
                string[] sp = str.Trim('\n').Split("=");
                if (sp.Length == 2) { settings[sp[0]] = sp[1]; }
            }
            file.Close();
        }
        else
        {
            Save();
        }
    }

    public void Check(string comand, string[] args)
    {
        if(comand == "settings")
        {
            Loop(args);
        }
    }

    public static Dictionary<string, string> settings = new Dictionary<string, string>()
    {
        ["strings"] = "10",
        ["exit_noconfirm"] = "false",
        ["shutdown_noconfirm"] = "false",
        ["shutdown_comand"] = "true",
        ["fullscreen"] = "true",
        [""] = "",
        ["variable_max"] = "-1",
        ["buffer_max"] = "-1",
        ["buffer_maxsize"] = "-1",
        ["comand_print"] = "normal",
    };

    Dictionary<string, string> description = new Dictionary<string, string>()
    {
        ["strings"] = "(must be INT) maximum strings on one screen (for some modes, like 'help')",
        ["exit_noconfirm"] = "always use exit comand with -noconfirm argument",
        ["shutdown_noconfirm"] = "always use shutdown comand with -noconfirm argument",
        ["shutdown_comand"] = "allow User to use shutdown comand",
        ["fullscreen"] = "activate fullscreen when starting console",
        [""] = "Just a string to separate settings (does nothing)",
        ["variable_max"] = "Max number of variables can be stored (-1 to deactivate limit)",
        ["buffer_max"] = "Max number of buffers can be stored (-1 to deactivate limit)",
        ["buffer_maxsize"] = "Max number of comands in one buffer can be stored (-1 to deactivate limit)",
        ["comand_print"] = "What is output during buffer execution: normal - all commands, name - buffer name, null - nothing",
    };

    void Loop(string[] args)
    {
        int line = 0;
        {
            bool nc = false;
            string last = "";
            foreach (string arg in args)
            {
                if(arg == "-nc") { nc = true; }
                if(last == "-ch")
                {
                    string[] sp = arg.Split("=");
                    if(sp.Length == 2)
                    {
                        string key = sp[0];
                        string value = sp[1];
                        if (settings.ContainsKey(key))
                        {
                            settings[key] = value;
                        }
                        else
                        {
                            pr_cl("ARGUMENT -ch: no setting with this name");
                        }
                    }
                }
                if(last == "-line")
                {
                    try
                    {
                        line = Int32.Parse(arg);
                    }catch{
                        if (settings.ContainsKey(arg))
                        {
                            line = 0;
                            foreach(KeyValuePair<string, string> set in settings)
                            {
                                if (set.Key == arg)
                                {
                                    break;
                                }line++;
                            }
                        }
                        else
                        {
                            pr_cl("AGUMENT -line: Can't recognize string.");
                        }
                    }
                }
                last = arg;
            }
            if (nc) { return; }
            if (line >= settings.Count)
            {
                line = 0;
            }
            if(!nc) { Console.Clear(); }
        }
        Render(line, false);
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            if(key.Key == ConsoleKey.Escape) { return; }

            if(key.Key == ConsoleKey.W | key.Key == ConsoleKey.UpArrow){
                line--;
                if(line < 0) { line = settings.Count - 1; }
                Render(line);
            }

            if (key.Key == ConsoleKey.S | key.Key == ConsoleKey.DownArrow)
            {
                line++;
                if (line >= settings.Count) { line = 0; }
                Render(line);
            }

            if (key.Key == ConsoleKey.E | key.Key == ConsoleKey.Enter)
            {
                Change(line);
                Render(line);
            }
        }
    }

    void Change(int line_num)
    {
        string line_str = "";
        {int i = 0;
            foreach (KeyValuePair<string, string> line in settings)
            {
                if(i == line_num)
                {
                    line_str = line.Key;
                    break;
                }
                i++;
        }}
        if (settings[line_str] == "false")
        {
            settings[line_str] = "true";
        }
        else
        {
            if (settings[line_str] == "true")
            {
                settings[line_str] = "false";
            }
            else
            {
                Console.Clear();
                pr_cl(fg: ConsoleColor.Green);
                print();
                print();
                print();
                print("   Write new value for " + line_str);
                print("     ", end: "");
                pr_cl($"({line_str} - {description[line_str]})", fg:ConsoleColor.Black, bg:ConsoleColor.Cyan);
                pr_cl("      Old value - " + settings[line_str]);
                print("   write new value or just press enter to cancel> ", end:"");
                string a = Console.ReadLine();
                if(a != null & a != "") { settings[line_str] = a; }
            }
        }
        Save();
    }

    void Render(int line_num, bool cl=true)
    {
        if (cl) { Console.Clear(); }
        print("    __________________ ");
        pr_cl("   |     SETTINGS     |");
        print("   |To exit, press Esc|");
        print("");
        int i = 0;
        int start = 0;
        int end = settings.Count;
        try
        {
            if (Int32.Parse(settings["strings"]) < settings.Count + 5)
            {
                int b = (Int32.Parse(settings["strings"]) - 6) / 2;
                end = line_num + b;
                start = end - Int32.Parse(settings["strings"]) + 6;
                if (start < 0)
                {
                    end -= start;
                    start = 0;
                }
            }
        } catch { }
        foreach (KeyValuePair<string, string> line in settings)
        {
            if (i >= start & i <= end)
            {
                pr_cl("  |-", end: "");
                if (i == line_num)
                {
                    pr_cl(line.Key, fg: ConsoleColor.Black, bg: ConsoleColor.Cyan, end: "");
                }
                else
                {
                    pr_cl(line.Key, fg: ConsoleColor.Black, bg: ConsoleColor.DarkCyan, end: "");
                }

                pr_cl("", end: " ");
                if (settings[line.Key] == "true")
                {
                    pr_cl(line.Value, fg: ConsoleColor.Black, bg: ConsoleColor.Green);
                }
                else
                {
                    if (settings[line.Key] == "false")
                    {
                        pr_cl(line.Value, fg: ConsoleColor.Black, bg: ConsoleColor.Red);
                    }
                    else
                    {
                        pr_cl(line.Value, fg: ConsoleColor.Black, bg: ConsoleColor.Cyan);
                    }
                }

                if (i == line_num)
                {
                    pr_cl("  |   \\" + description[line.Key]);
                }
            }
            i++;
        }
        pr_cl(fg:ConsoleColor.Black);
    }

    public static void Save()
    {
        Filer file = new Filer("settings.conf");
        file.Clear();
        foreach (KeyValuePair<string, string> str in settings)
        {
            file.WriteLine($"{str.Key}={str.Value}");
        }
        file.Close();
    }
}