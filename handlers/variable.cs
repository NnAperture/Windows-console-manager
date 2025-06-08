using static Program;

public class Variabler
{
    public static Dictionary<string, string> variables = new Dictionary<string, string>()
    {
        ["COMPRINT"] = "true"
    };
    public void Check(string comand, string[] args)
    {
        if(comand == "var")
        {
            Separator(args);
        }
    }

    void Separator(string[] args)
    {
        pr_cl(end: "", fg: ConsoleColor.Red);
        bool nc = args.Contains("-nc");
        string last = "";
        string name = "";
        foreach(string arg in args)
        {
            if(last == "") { name = arg; }
            if(arg == "-list") { List(); }
            if(arg == "-remove_all") { RemoveAll(nc); }
            if(arg == "-remove") { Remove(name, nc); }
            if (last == "+=") { Add(name, arg, nc); }
            if (last == "-=") { Sub(name, arg, nc); }
            if (last == "/=") { Divt(name, arg, nc); }
            if (last == "//=") { Div(name, arg, nc); }
            if (last == "%=") { Divo(name, arg, nc); }
            if (last == "*=") { Mult(name, arg, nc); }
            if (last == "'*=") { Mult(name, arg, nc, true); }

            last = arg;
        }
    }

    void Add(string name, string arg, bool nc)
    {
        if (name == "")
        { if (!nc) { print("Please write variable name!"); } return; }
        bool exists = true;
        if (!variables.ContainsKey(name))
        {
            exists = false;
            if ((arg[0] == '"' & arg[arg.Length - 1] == '"')) { variables[name] = ""; }
            else { variables[name] = "0"; }
        }
        if ((arg[0] == '"' & arg[arg.Length - 1] == '"')) { variables[name] += arg.Substring(1, arg.Length - 2); }
        else
        {
            try
            {
                try
                {
                    long num = Int64.Parse(variables[name]);
                    num += Int64.Parse(arg);
                    variables[name] = num.ToString();
                }
                catch
                {
                    float num = Single.Parse(variables[name]);
                    num += Single.Parse(arg);
                    variables[name] = num.ToString();
                }
            }
            catch
            {
                try
                {
                    try
                    {
                        long num = Int64.Parse(variables[name]);
                        num += Int64.Parse(variables[arg]);
                        variables[name] = num.ToString();
                    }
                    catch
                    {
                        float num = Single.Parse(variables[name]);
                        num += Single.Parse(variables[arg]);
                        variables[name] = num.ToString();
                    }
                }
                catch
                {
                    if (!exists) { variables[name] = ""; }
                    if (variables.ContainsKey(arg))
                    {
                        variables[name] += variables[arg];
                    }
                    else
                    {
                        try
                        { variables[name] += arg; }
                        catch { variables[name] = arg; }
                    }
                }
            }
        }
        if (!nc) { print("Var " + name + " is now =" + variables[name]); }
    }
    void Mult(string name, string arg, bool nc, bool str = false)
    {
        if (name == "")
        { if (!nc) { print("Please write variable name!"); } return; }
        bool exists = true;
        if (!variables.ContainsKey(name))
        {
            exists = false;
            if ((arg[0] == '"' & arg[arg.Length - 1] == '"')) { variables[name] = ""; }
            else { variables[name] = "0"; }
        }
        if ((arg[0] == '"' & arg[arg.Length - 1] == '"')) { variables[name] += arg.Substring(1, arg.Length - 2); }
        else
        {
            try
            {
                if (str) { Int32.Parse("ad"); }
                try
                {
                    long num = Int64.Parse(variables[name]);
                    num *= Int64.Parse(arg);
                    variables[name] = num.ToString();
                }
                catch
                {
                    float num = Single.Parse(variables[name]);
                    num *= Single.Parse(arg);
                    variables[name] = num.ToString();
                }
            }
            catch
            {
                try
                {
                    if (str) { Int32.Parse("ad"); }
                    try
                    {
                        long num = Int64.Parse(variables[name]);
                        num *= Int64.Parse(variables[arg]);
                        variables[name] = num.ToString();
                    }
                    catch
                    {
                        float num = Single.Parse(variables[name]);
                        num *= Single.Parse(variables[arg]);
                        variables[name] = num.ToString();
                    }
                }
                catch
                {
                    if (!exists) { variables[name] = ""; }
                    if (variables.ContainsKey(arg))
                    {
                        variables[name] = String.Concat(Enumerable.Repeat(variables[name], Int32.Parse(variables[arg])));
                    }
                    else
                    {
                        try
                        { variables[name] = String.Concat(Enumerable.Repeat(variables[name], Int32.Parse(arg))); }
                        catch { variables[name] = arg; }
                    }
                }
            }
        }
        if (!nc) { print("Var " + name + " is now =" + variables[name]); }
    }


    void Sub(string name, string arg, bool nc)
    {
        if (!variables.ContainsKey(name))
        {
            variables[name] = "0";
        }
        try
        {
            try
            {
                long num = Int64.Parse(variables[name]);
                num -= Int64.Parse(arg);
                variables[name] = num.ToString();
                if (!nc) { print("Var " + name + " is now =" + variables[name]); }
            }
            catch
            {
                float num = Single.Parse(variables[name]);
                num -= Single.Parse(arg);
                variables[name] = num.ToString();
                if (!nc) { print("Var " + name + " is now =" + variables[name]); }
            }
        }
        catch
        {
            if (variables.ContainsKey(arg))
            {
                try
                {
                    long num = Int64.Parse(variables[name]);
                    num -= Int64.Parse(variables[arg]);
                    variables[name] = num.ToString();
                    if (!nc) { print("Var " + name + " is now =" + variables[name]); }
                }
                catch
                {
                    float num = Single.Parse(variables[name]);
                    num -= Single.Parse(variables[arg]);
                    variables[name] = num.ToString();
                    if (!nc) { print("Var " + name + " is now =" + variables[name]); }
                }
            }
            else
            {
                if (!nc) { print("Error: -= can work only with NEMBER. cuurent value of " + name + " is " + variables[name]); }
            }
        }
    }

    void Div(string name, string arg, bool nc)
    {
        if (!variables.ContainsKey(name))
        {
            variables[name] = "0";
        }
        try
        {
            float num = Single.Parse(variables[name]);
            num /= Int64.Parse(arg);
            variables[name] = ((long)(num)).ToString();
            if (!nc) { print("Var " + name + " is now =" + variables[name]); }
        }
        catch
        {
            if (variables.ContainsKey(arg))
            {
                try {
                    long num = Int64.Parse(variables[name]);
                    num /= Int64.Parse(variables[arg]);
                    variables[name] = num.ToString();
                    if (!nc) { print("Var " + name + " is now =" + variables[name]); }
                }
                catch
                {
                    float num = Single.Parse(variables[name]);
                    num /= Int64.Parse(variables[arg]);
                    variables[name] = ((long)(num)).ToString();
                    if (!nc) { print("Var " + name + " is now =" + variables[name]); }
                }
            }
            else
            {
                if (!nc) { print("Error: -= can work only with NEMBER. cuurent value of " + name + " is " + variables[name]); }
            }
        }
    }
    void Divo(string name, string arg, bool nc)
    {
        if (!variables.ContainsKey(name))
        {
            variables[name] = "0";
        }
        try
        {
            try
            {
                long num = Int64.Parse(variables[name]);
                num /= Int64.Parse(arg);
                variables[name] = num.ToString();
                if (!nc) { print("Var " + name + " is now =" + variables[name]); }
            }
            catch
            {
                float num = Single.Parse(variables[name]);
                num /= Single.Parse(arg);
                variables[name] = num.ToString();
                if (!nc) { print("Var " + name + " is now =" + variables[name]); }
            }
        }
        catch
        {
            if (variables.ContainsKey(arg))
            {
                long num = Int64.Parse(variables[name]);
                num /= Int64.Parse(variables[arg]);
                variables[name] = num.ToString();
                if (!nc) { print("Var " + name + " is now =" + variables[name]); }
            }
            else
            {
                if (!nc) { print("Error: -= can work only with NEMBER. cuurent value of " + name + " is " + variables[name]); }
            }
        }
    }
    void Divt(string name, string arg, bool nc)
    {
        if (!variables.ContainsKey(name))
        {
            variables[name] = "0";
        }
        try
        {
            float num = Single.Parse(variables[name]);
            num /= Single.Parse(arg);
            variables[name] = num.ToString();
            if (!nc) { print("Var " + name + " is now =" + variables[name]); }
        }
        catch
        {
            if (variables.ContainsKey(arg))
            {
                float num = Single.Parse(variables[name]);
                num /= Single.Parse(variables[arg]);
                variables[name] = num.ToString();
                if (!nc) { print("Var " + name + " is now =" + variables[name]); }
            }
            else
            {
                if (!nc) { print("Error: -= can work only with NEMBER. cuurent value of " + name + " is " + variables[name]); }
            }
        }
    }
    void Remove(string name, bool nc)
    {
        variables.Remove(name);
        if (!nc) { print("Variable " + name + " is deleted."); }
    }

    void RemoveAll(bool nc)
    {
        variables = new Dictionary<string, string>();
        if (!nc) { print("All variables deleted."); }
    }

    public static void List()
    {
        pr_cl("Variables:", fg:ConsoleColor.Cyan);
        print();
        int i = 0;
        foreach(KeyValuePair<string, string> pair in variables)
        {
            print(i.ToString() + " " + pair.Key + ": " + pair.Value);
            i++;
        }
    }
}