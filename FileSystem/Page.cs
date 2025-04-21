using System.Runtime.InteropServices;
using static Program;
public class Page
{
    string path = "";
    int selected = 0;
    int start = 0;
    string[] folders = new string[0];
    string[] files = new string[0];
    DriveInfo[] drives = DriveInfo.GetDrives();
    public Page(string path_, int selected_)
    {
        bool f = true;
        path = path_;
        int i = 1;
        string[] drves = new string[drives.Length];
        while (path == "")
        {
            if (f)
            {
                print();
                print();
                print();
                pr_cl("    Select disk for work in this page:", fg: ConsoleColor.Cyan);
                print();
                foreach (DriveInfo drive in drives)
                {
                    drves[i - 1] = drive.Name;
                    print(i, end: " ");
                    print(drive.Name);
                    i++;
                }
                print();
                f = false;
            }
            string inp = Console.ReadLine();
            try
            {
                path = drves[Int32.Parse(inp) - 1];
            }
            catch
            {
                foreach(string drive in drves)
                {
                    if(drive == inp) { path = inp; }
                }
            }
        }
        selected = selected_;
    }
    public void SelMove(int displacement)
    {
        selected += displacement;
        if (selected < 0) { selected = folders.Length + files.Length - 1; }
        if (selected >= folders.Length + files.Length) { selected = 0; }
    }
    public void Render(bool cmd)
    {
        print(path);
        Load();
        int strings = 10;
        try
        {
            strings = Int32.Parse(Settings.settings["strings"]);
        } catch { }
        {
            int str = 0;
            int st = 0;
            foreach (string folder in folders)
            {
                if (st >= start)
                {
                    str++;
                    if (str == 0) { pr_cl("----- FOLDERS:"); }
                    string l_path = path + folder;
                    ConsoleColor bg = ConsoleColor.Black;
                    ConsoleColor fg = ConsoleColor.White;
                    if (selected == st)
                    {
                        bg = ConsoleColor.Blue;
                        fg = ConsoleColor.Black;
                    }
                    pr_cl(" |-", end: "", fg:fg, bg:bg);
                    print(folder);
                }
                st++;
            }
            if (st >= start)
            {
                pr_cl("----- FILES:");
                str++;
            }
            foreach (string file in files)
            {
                if (st >= start)
                {
                    str++;
                    string l_path = path + file;
                    ConsoleColor bg = ConsoleColor.Black;
                    ConsoleColor fg = ConsoleColor.White;
                    if (selected == st)
                    {
                        bg = ConsoleColor.Blue;
                        fg = ConsoleColor.Black;
                    }
                    pr_cl(" |-", end: "", fg: fg, bg: bg);
                    print(file);
                }st++;
            }
        }
        if (cmd)
        {
            print("  fs_comand > ", end: "");
            string comand = Console.ReadLine();
        }
    }

    public void ReDrives()
    {
        drives = DriveInfo.GetDrives();
    }

    void Load()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        if (directoryInfo.Exists)
        {
            {
                DirectoryInfo[] inf = directoryInfo.GetDirectories();
                int i = 0;
                folders = new string[inf.Length];
                foreach (DirectoryInfo subDir in inf)
                {
                    folders[i] = subDir.Name;
                    i++;
                }
            }
            {
                FileInfo[] inf = directoryInfo.GetFiles();
                int i = 0;
                files = new string[inf.Length];
                foreach (FileInfo Fil in inf)
                {
                    files[i] = Fil.Name;
                    i++;
                }
            }
        }
        else
        {
            path = "";
        }
    }

    public string DriveInfoString(DriveInfo drive)
    {
        if (drive.IsReady) // Проверяем, доступен ли диск
        {
            return ($"- {drive.Name} ({drive.DriveType}, {drive.TotalFreeSpace / (1024 * 1024 * 1024)} ГБ свободно)");
        }
        else
        {
            return "Non-active";
        }
    }
}