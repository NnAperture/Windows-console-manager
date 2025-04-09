using static Program;

class Help
{
    public void Check(string comand)
    {
        if(comand == "help")
        {
            Output();
        }
    }
    int str = 0;
    public void Output()
    {
        Console.Clear();
        pr_cl(fg: ConsoleColor.Gray, bg: ConsoleColor.DarkGray);
        print();
        print("  __HELP:");
        print("  | ");
        print("  |-help");
        print("  |  > prints comand list.");
        str += 5; if(ch_pg(2)){ return; }
        print("  |-tfs");
        print("  |  > toggles full screen.");
        str += 2; if (ch_pg(2)) { return; }
        print("  |-exit [ -nc -noconfirm ]");
        print("  |  > exit the DOS-win_10.    Optional args: -nc, -noconfirm - exit without confirm.");
        str += 2; if (ch_pg(2)) { return; }
        print("  |-shutdown [ -nc -noconfirm ]");
        print("  |  > turn off your computer.    Optional args: -nc, -noconfirm - exit without confirm.");
        str += 2; if (ch_pg(2)) { return; }
        print("  |-settings [ -line (number|setting) ]");
        print("  |  > settings menu.    Optional args: -line: next argument needs to be number / name of setting you want to select at first.");
        str += 2; if (ch_pg(3)) { return; }
        print("  |");
        print("  |-fs");
        print("  |  > enter FileSystem mode.");
        str += 3; if (ch_pg(0)) { return; }


        print();
    }

    bool ch_pg(int next)
    {
        if(str >= Int32.Parse(Settings.settings["strings"]) - next)
        {
            str = 0;
            pr_cl("End of page. Press enter to next page or write 'end' to exit", fg:ConsoleColor.Black, bg:ConsoleColor.Magenta, end:"");
            pr_cl(fg: ConsoleColor.Green, bg: ConsoleColor.Black);
            string a = Console.ReadLine();
            pr_cl();
            if(a == "end")
            {
                return true;
            }
            else
            {
                Console.Clear();
            }
        }
        pr_cl(fg: ConsoleColor.Gray, bg: ConsoleColor.DarkGray, end:"");
        return (false);
    }
}