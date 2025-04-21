using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using static Program;
public class FS_Menu{
    public static bool IsPressed(string control, string pressed)
    {
        foreach (string keycomb in controls[control])
        {
            if (pressed == keycomb) { return (true); }
        }
        return false;
    }
    public static Dictionary<string, string> settings = new Dictionary<string, string>()
    {

    };
    public static Dictionary<string, string[]> controls = new Dictionary<string, string[]>()
    {
        ["down_selected"] = new string[1] { "DownArrow" },
        ["up_selected"] = new string[1] { "UpArrow" },
        ["menu"] = new string[1] { "M" },
    };
    public static Dictionary<string, string> description = new Dictionary<string, string>()
    {
        ["down_selected"] = "Buttons to move down in file/folder list.",
        ["up_selected"] = "Buttons to move up in file/folder list.",
        ["menu"] = "Menu button",
    };
    public static void Menu()
    {
        while (true)
        {

        }
    }
}