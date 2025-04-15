using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using static Program;
public class Conditional
{
    public void Check(string comand, string[] args)
    {
        if (comand == "if")
        {
            Buffer.buffers["TEMP"] = If(args);
            Buffer.Execute("TEMP");
        }
    }

    public string[] BCheck(string a)
    {
        return Handle(a);
    }

    public string[] Handle(string input)
    {
        List<string> sp = new List<string>();
        bool str = false;
        string word = "";
        char last = ' ';
        bool var = false;
        string vir = "";
        bool changed = false;
        foreach (char ch in input)
        {
            if (var)
            {
                vir += ch;
                if (last == '\\' & ch == '\\') { vir = vir.Substring(0, vir.Length - 1); last = ' '; }
                if (ch == ']')
                {
                    if (last == '\\')
                    {
                        vir = vir.Substring(0, vir.Length + 2) + ']';
                    }
                    else
                    {
                        if (Variabler.variables.ContainsKey(vir.Substring(1, vir.Length - 2)))
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
        if (word != "") { sp.Add(word + vir); }

        if (sp[0] == "if") { return If(sp.ToArray()[1..]); }
        return new string[0];
    }

    string[] If(string[] args)
    {
        try
        {
            List<string> tr = new List<string>();
            List<string> fl = new List<string>();
            string last = "";
            foreach (string arg in args)
            {
                if (arg[0] != '-' && last != "")
                {
                    if (last == "-else")
                    {
                        fl.Add(arg);
                    }
                    else
                    {
                        tr.Add(arg);
                    }
                }
                last = arg;
            }
            string a = args[0];
            if (a[0] == '"' && a[a.Length - 1] == '"') { a = a.Substring(1, a.Length - 2); }
            a = a.Replace(" ", "");
            if (Expr(a))
            {
                List<string> com = new List<string>();
                foreach (string buf in tr)
                {
                    if (Buffer.buffers.ContainsKey(buf))
                    {
                        com.AddRange(Buffer.buffers[buf]);
                    }
                }
                return com.ToArray();
            }
            else
            {
                List<string> com = new List<string>();
                foreach (string buf in fl)
                {
                    if (Buffer.buffers.ContainsKey(buf))
                    {
                        com.AddRange(Buffer.buffers[buf]);
                    }
                }
                return com.ToArray();
            }
        }
        catch
        {
            return new string[0];
        }
    }

    string[] While(string comand)
    {
        return new string[0];
    }

    bool Expr(string a)
    {
        char[] operators = new char[] { '&', '|' };
        List<string> micros = new List<string>();
        List<string> sign = new List<string>();
        int br = 0;
        string thing = "";
        bool m = false;

        foreach(char ch in a)
        {
            if(ch == '(')
            {
                br++;
            }
            else
            {
                if(br > 0)
                {
                    if (ch == ')') { br--; }
                    if(br == 0) {
                        if (Expr(thing.Substring(1, thing.Length - 1)))
                        {
                            thing = ("1=1");
                        }
                        else
                        {
                            thing = ("1=2");
                        }
                        m = true;
                    }
                }
                else
                {
                    if (operators.Contains(ch))
                    {
                        micros.Add(thing);
                        sign.Add(ch.ToString());
                        thing = "";
                        m = true;
                    }
                }
            }
            if (m) { m = false; } else { thing += ch; }
        }
        if(thing != "") { micros.Add(thing); }
        bool current = Prime(micros[0]);

        for(int i = 0; i < sign.Count; i++)
        {
            if (sign[i] == "|") { current |= Prime(micros[i + 1]); }
            if (sign[i] == "&") { current &= Prime(micros[i + 1]); }
        }
        return current;
    }

    bool Prime(string prim)
    {
        if (prim.Contains('>')) { return (Int64.Parse(prim.Split(">")[0]) > Int64.Parse(prim.Split(">")[1])); }
        if (prim.Contains(">=")) { return (Int64.Parse(prim.Split(">=")[0]) >= Int64.Parse(prim.Split(">=")[1])); }
        if (prim.Contains('<')) { return (Int64.Parse(prim.Split("<")[0]) < Int64.Parse(prim.Split("<")[1])); }
        if (prim.Contains("<=")) { return (Int64.Parse(prim.Split("<=")[0]) <= Int64.Parse(prim.Split("<=")[1])); }
        if (prim.Contains("=")) { return (prim.Split("=")[0] == prim.Split("=")[1]); }

        throw new Exception();
    }
}