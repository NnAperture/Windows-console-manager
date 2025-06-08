using System.ComponentModel;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using static Program;

public class Buffer
{
    public static Dictionary<string, string[]> buffers = new Dictionary<string, string[]>();
    public void Check(string comand, string[] args)
    {
        if(comand == "buffer")
        {
            Separator(args);
        }
    }

    void Separator(string[] args)
    {
        pr_cl(end: "", fg: ConsoleColor.Blue);
        bool nc = args.Contains("-nc");
        string last = "";
        string name = "";
        foreach (string arg in args)
        {
            if (last == "") { name = arg; }
            if(arg == "-list") { List(); }
            if(arg == "-print") { Print(name); }
            if (arg == "-fullremove") { FullRemove(name, nc); }
            if (last == "-add") { Add(name, arg, nc); }
            if (last == "-num") { Num(name, arg); }
            if (last == "-remove") { Remove(name, arg, nc); }
            if (last == "-pop") { Pop(name, arg, nc); }

            if(arg == "-execute") { Execute(name, nc); }


            last = arg;
        }
    }

    public static void Execute(string name, bool nc=true)
    {
        Conditional conditional = new Conditional();
        if (buffers.ContainsKey(name))
        {
            if (Settings.settings["comand_print"] == "name")
            {
                pr_cl(name, fg: ConsoleColor.Green, bg: ConsoleColor.Black);
            }
            Handler handler = new Handler();
            bool a = true;
            if (!nc) { print("Buffer " + name); }
            pr_cl(fg: ConsoleColor.Green, bg: ConsoleColor.Black, end: "");
            List<string> que = buffers[name].ToList();
            while (que.Count != 0)
            {
                string inp = que[0];
                if (Settings.settings["comand_print"] == "normal")
                {
                    pr_cl(" execute> ", end: "");
                    pr_cl(inp, fg: ConsoleColor.Green, bg: ConsoleColor.Black);
                }
                if (inp == "end") { return; }

                if (inp == "tfs")
                {
                    ConsoleFullScreen.ToggleFullScreen();
                }
                else
                {
                    if (inp.Length > 7 && inp.Substring(0, 7) == "execute")
                    {
                        string nam = "";
                        try
                        {
                            nam = inp.Split()[1];
                        }
                        catch { }
                        if (buffers.ContainsKey(nam)) {
                            que.RemoveAt(0);
                            List<string> n = new List<string>() { "" };
                            n.AddRange(buffers[nam]);
                            n.AddRange(que);
                            que = n;
                        }
                    }
                    else
                    {
                        string[] b = conditional.BCheck(inp.Trim('"'));
                        if (b.Length == 0)
                        {
                            a = handler.Handle(inp);
                            if (!a) { Environment.Exit(12); }
                        }
                        else
                        {
                            que.RemoveAt(0);
                            List<string> n = new List<string>() { "" };
                            n.AddRange(b);
                            n.AddRange(que);
                            que = n;
                        }
                    }
                }
                que.RemoveAt(0);
            }
        }
        else
        {
            print("Cannot find buffer " + name);
        }
    }

    void FullRemove(string name, bool nc)
    {
        buffers.Remove(name);
        if (!nc)
        {
            print("Buffer " + name + " is deleted.");
        }
    }

    void Remove(string name, string arg, bool nc)
    {
        if (buffers.ContainsKey(name))
        {
            List<string> newer = new List<string>();
            bool a = true;
            int i = 0;
            foreach (string com in buffers[name])
            {
                if (arg[0] == '"' & arg[arg.Length - 1] == '"')
                {
                    if (arg.Substring(1, arg.Length - 2) != com) { newer.Add(com); }
                }
                else
                {
                    if (arg != com) { newer.Add(com); }
                }
                i++;
            }
            buffers[name] = newer.ToArray();
            if (!nc) { Print(name); }
        }
        else
        {
            print("Cannot find buffer " + name);
        }
    }
    void Pop(string name, string arg, bool nc)
    {
        if (buffers.ContainsKey(name))
        {
            try
            {
                List<string> newer = new List<string>();
                int num = Int32.Parse(arg);
                bool a = true;
                int i = 0;
                foreach (string com in buffers[name])
                {
                    if (i != num) { newer.Add(com); }
                    i++;
                }
                buffers[name] = newer.ToArray();
                if (!nc) { Print(name); }
            }
            catch
            {
                print("Cannot interprete '" + arg + "' as number.");
            }
        }
        else
        {
            print("Cannot find buffer " + name);
        }
    }

    void Num(string name, string arg)
    {
        Variabler variabler = new Variabler();
        if (buffers.ContainsKey(name))
        {
            bool a = true;
            int i = 0;
            foreach (string com in buffers[name])
            {
                if (arg[0] == '"' & arg[arg.Length - 1] == '"')
                {
                    if (arg.Substring(1, arg.Length - 2) == com) { print(i.ToString()); Variabler.variables["BUFNUM"] = i.ToString(); }
                }
                else
                {
                    if (arg == com) { print(i.ToString()); Variabler.variables["BUFNUM"] = i.ToString(); }
                }
                i++;
            }
        }
        else
        {
            print("Cannot find buffer " + name);
        }
    }

    void Add(string name, string arg, bool nc)
    {
        if (!buffers.ContainsKey(name))
        {
            buffers[name] = new string[0];
        }
        string[] newer = new string[buffers[name].Length + 1];
        int i = 0;
        foreach(string str in buffers[name])
        {
            newer[i] = str;
            i++;
        }
        if (arg[0] == '"' & arg[arg.Length - 1] == '"') {
            newer[i] = arg.Substring(1, arg.Length - 2);
            if (!nc) { print("Added comand " + arg.Substring(1, arg.Length - 2)); }
        }
        else
        {
            newer[i] = arg;
            if (!nc) { print("Added comand " + arg); }
        }
        buffers[name] = newer;
    }

    void Print(string name)
    {
        if (buffers.ContainsKey(name))
        {
            print("Buffer " + name);
            int i = 0;
            foreach(string com in buffers[name])
            {
                print(i.ToString(), end: " "); print(com);
                i++;
            }
        }
        else
        {
            print("Cannot find buffer " + name);
        }
    }

    void List()
    {
        print("Buffers:");
        int i = 0;
        foreach(KeyValuePair<string, string[]> buf in buffers)
        {
            print(i.ToString() + " " + buf.Key);
            i++;
        }
    }
}