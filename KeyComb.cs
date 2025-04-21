using System.Windows.Forms;
using static Program;

public class KeyCombination
{
    public void Check(string comand, string[] args)
    {
        if(comand == "keycomb")
        {
            Loop();
        }
    }
    void Loop()
    {
        string a = GetComb();
        while (a != "Escape")
        {
            print("  " + a);
            a = GetComb();
        }
    }
    public static string GetComb()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        return GetComb(keyInfo);
    }
    public static string GetComb(ConsoleKeyInfo keyInfo)
    {
        
        string outp = "";

        if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0)
        {
            outp += "Ctrl+";
        }
        if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0)
        {
            outp += "Shift+";
        }
        if ((keyInfo.Modifiers & ConsoleModifiers.Alt) != 0)
        {
            outp += "Alt+";
        }
        return outp + keyInfo.Key;
    }
}