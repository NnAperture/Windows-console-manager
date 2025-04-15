using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Program;
public class Enviroment_saver
{
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
            print('"' + line + '"');
            if (line != "")
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
            else
            {
                print("aa");
            }
        }
    }

    void Separate(string mode, string[] page, string fname)
    {
        if (mode.TrimEnd() == "VAR {") { Variable(page, fname); }
        if (mode.StartsWith("BUFFER")) { Buffer_read(mode, page, fname); }
        if (mode.StartsWith("SETUP")) { 
            string a = Buffer_read(mode, page, fname);
            if(a != "") { Buffer.Execute(a); }
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
