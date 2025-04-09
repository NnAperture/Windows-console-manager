using System.Runtime.InteropServices;
using static Program;
public class Page
{
    string path = "";
    int selected = 0;
    string[] folders = new string[0];
    string[] files = new string[0];
    DriveInfo[] drives = DriveInfo.GetDrives();
    public Page(string path_, int selected_)
    {
        path = path_;
        selected = selected_;
    }

    public void Render()
    {
        Load();

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