using System.Runtime.InteropServices;
using static Program;
public class Settings
{
    Dictionary<string, string> settings = new Dictionary<string, string>()
    {
        ["strings"]="10",
    };
    public Settings()
    {
        print("aaa");
        if (File.Exists("settings.conf"))
        {
            Filer file = new Filer("settings.conf");
            foreach(string str in file)
            {
                string[] sp = str.Split("=");
                settings[sp[0]] = sp[1];
            }
            file.Close();
        }
        else
        {
            Filer file = new Filer("settings.conf");
            foreach (KeyValuePair<string, string> str in settings)
            {
                file.Write($"{str.Key}={str.Value}");
            }
        }
    }
}