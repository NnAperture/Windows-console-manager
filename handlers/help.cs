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
    public void Output()
    {
        Console.Clear();
        pr_cl(fg: ConsoleColor.Gray, bg: ConsoleColor.DarkGray);
        print();
        print("  __HELP:");
        pr_cl("  | ", fg: ConsoleColor.Green, bg: ConsoleColor.Black);
        print("  |-help");
        print("  |  > prints comand list.");
        print("  |-exit [ -nc -noconfirm ]");
        print("  |  > exit the DOS-win_10.    Optional args: -nc, -noconfirm - exit without confirm.");
        print("  |-shutdown [ -nc -noconfirm ]");
        print("  |  > turn off your computer.    Optional args: -nc, -noconfirm - exit without confirm.");
        print("  |-fs");
        print("  |  > enter FileSystem mode.");


        print();
    }
}