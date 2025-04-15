using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Program;
public class Enviroment_saver
{
    public void Check(string comand, string[] args)
    {
        if(comand == "env")
        {
            Env();
        }
    }

    void Env()
    {
        string env = "SETTINGS {\n";
        foreach(KeyValuePair<string, string> pair in Settings.settings)
        {
            env += $"  {pair.Key}={pair.Value}\n";
        }env += "}\n\nVARS {\n";
        foreach (KeyValuePair<string, string> pair in Variabler.variables)
        {
            env += $"  {pair.Key}={pair.Value}\n";
        }env += "}\n\n";
        foreach(KeyValuePair<string, string[]> pair in Buffer.buffers)
        {
            env += $"BUFFER {pair.Key} {'{'}\n";
            foreach(string str in pair.Value)
            {
                env += "  " + str + '\n';
            }env += "}\n\n";
        }
        print(env);
    }
    public void Argument(string[] args)
    {
        foreach (string filePath in args)
        {
            print("import " + filePath);
            if (File.Exists(filePath) && filePath.ToLower().EndsWith(".ssue"))
            {
                try
                {
                    FileSplit(filePath);
                }
                catch (Exception ex)
                {
                    print("Enviroment file reading ERROR: " + ex.ToString());
                }
            }
        }
    }

    void FileSplit(string fname)
    {
        Filer file = new Filer(fname);
        string mode = "";
        List<string> page = new List<string>();

        foreach(string line1 in file)
        {
            string line = line1.Trim();
            if (line != "" && line[0] != '#')
            {
                if (mode == "")
                {
                    mode = line;
                }
                else
                {
                    if (line.Trim() == "}")
                    {
                        mode = mode.Replace("\0", "");
                        Separate(mode, page.ToArray(), fname.Split("\\").Last());
                        page = new List<string>();
                        mode = "";
                    }
                    else
                    {
                        page.Add(line.Trim());
                    }
                }
            }
        }
    }

    void Separate(string mode, string[] page, string fname)
    {
        if (mode.TrimEnd() == "VAR {") { Variable(page, fname); }
        if (mode.TrimEnd() == "SETTINGS {") { Settinger(page, fname); }
        if (mode.StartsWith("BUFFER")) { Buffer_read(mode, page, fname); }
        if (mode.StartsWith("SETUP")) { 
            string a = Buffer_read(mode, page, fname);
            if(a != "") { Buffer.Execute(a); }
        }
    }

    void Settinger(string[] page, string fname)
    {
        foreach (string line in page)
        {
            string[] sp = line.Split("=");
            if (sp.Length != 2)
            {
                print("Enviroment file reading ERROR <syntax>: file " + fname + " line " + line + " defined as VAR, but number of elements is wrong.");
            }
            else
            {
                print(sp[0]);
                if (Settings.settings.ContainsKey(sp[0]))
                {
                    print(sp[1]);
                    Settings.settings[sp[0]] = sp[1];
                }
                else
                {
                    print("Enviroment file reading ERROR <KeyError>: file " + fname + " line " + line + ": setting is not found.");
                }
            }
            Settings.Save();
        }
    }

    string Buffer_read(string mode, string[] page, string fname)
    {
        string[] md = mode.Trim().Split();
        if (md.Length != 3)
        {
            print(md.Length.ToString());
            foreach(string a in md) { print(a); }
            Console.WriteLine("Enviroment file reading ERROR <syntax>: file " + fname + ":");
            Console.WriteLine("       header " + mode + " defined as BUFFER, but number of elements is wrong. (Correct syntax: 'BUFFER <name> {')");
            print(mode);
            return "";
        }
        Buffer.buffers[md[1]] = page;
        return md[1];
    }

    void Variable(string[] page, string fname)
    {
        foreach(string line in page)
        {
            string[] sp = line.Split("=");
            if(sp.Length != 2) {
                print("Enviroment file reading ERROR <syntax>: file " + fname + " line " + line + " defined as VAR, but number of elements is wrong.");}
            else
            {
                Variabler.variables[sp[0]] = sp[1];
            }
        }
    }
}
