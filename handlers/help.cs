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
        str = 0;
        Console.Clear();
        pr_cl(fg: ConsoleColor.Gray, bg: ConsoleColor.DarkGray);
        print();
        print("  __HELP:");
        print("  | ");
        print("  |-help");
        print("  |  > prints comand list.");
        str += 5; if (ch_pg(2)) { return; }
        print("  |-tfs");
        print("  |  > toggles full screen.");
        str += 2; if (ch_pg(2)) { return; }
        print("  |-keycomb");
        print("  |  > Prints pressed key combinations. Esc to exit.");
        str += 2; if (ch_pg(2)) { return; }
        print("  |-exit [ -nc -noconfirm ]");
        print("  |  > exit the DOS-win_10.    Optional args: -nc, -noconfirm - exit without confirm.");
        str += 2; if (ch_pg(2)) { return; }
        print("  |-shutdown [ -nc -noconfirm ]");
        print("  |  > turn off your computer.    Optional args: -nc, -noconfirm - exit without confirm.");
        str += 2; if (ch_pg(4)) { return; }
        print("  |-settings [ -nc -line (number|setting) -ch key=value ]");
        print("  |  > settings menu.    Optional args: -nc: noconsole mode ();");
        print("  |    -line: next argument needs to be number / name of setting you want to select at first.");
        print("  |    -ch: next argument needs to be pair of settings_name=new_value");
        str += 4; if (ch_pg(7)) { return; }
        print("  |");
        print("  |-buffer <name> [ -add \"comand\" -remove \"comand\" -pop <num> -execute -fullremove -print -list -nc -num \"comand\" ]");
        print("  |  > batch buffer. First argument - name of buffer");
        print("  |    -nc: no print; -print: print all comands from this buffer; -list:print all buffers;");
        print("  |    -add: add new comand;  -num: print number by full comand (Insertes result to BUFNUM variable);  -execute: execute buffer's comands in console (batch)");
        print("  |    -remove: removes a string by full string;  -pop: removes a string by number; -fullremove: delete this buffer");
        print("  |    -fullremove: remove full buffer;");
        str += 7; if (ch_pg(2)) { return; }
        print("  |-end");
        print("  |  > (ONLY FOR BUFFERS) end execution");
        str += 2; if (ch_pg(2)) { return; }
        print("  |-execute <name>");
        print("  |  > (ONLY FOR BUFFERS) better execution");
        str += 2; if (ch_pg(2)) { return; }
        print("  |-if \"expression\" <name> -else name");
        print("  |  > expression: NO variables (\"2 < 4 & (3 == 3 | (!5 >= 1))\"). To use variables, \"[name] > 2\"");
        print("  |    name - buffer name.");
        str += 2; if (ch_pg(6)) { return; }
        print("  |-var <name> [ -nc -list -remove_all -remove += -= *= /= \"expression\" -buf_len -lis_len -str_len <name> ]");
        print("  |  > variables manager. -list: print list of all variables (<name> can be empty) -remove_all: remove all variables (<name> can be empty)");
        print("  |    -remove: remove this variable; += -= *= /= //= %= works with NUMBER; += *= '*= works with STRINGS ('*= - multiply STRING to NUMBER);");
        print("  |     !!! If variable is not defined, it uses '0' or ''.");
        print("  |    -buf_len -lis_len -str_len - takes as next argument name of object. Sets variable as length of object");
        print("  |    To use variable, in comand write [name] (this constuction will be raplaced by value); If you want to write ] or [ (in str), write \\[");
        str += 6; if (ch_pg(2)) { return; }
        print("  |-env");
        print("  |  > prints enviroment file. TUTOR: write output to file with name 'test.ssue', double-click it and select SSU as program to open this file extension.");
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