using System.Runtime.InteropServices;
using static Handler;
class Program
{
    public static Settings settings = new Settings();
    public static void print(string text = "", string end = "\n")
    {
        Console.Write(text + end);
    }
    public static void pr_cl(string text = "", ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black, string end = "\n")
    {
        Console.ForegroundColor = fg;
        Console.BackgroundColor = bg;
        Console.Write(text + end);
    }

    public void Loop()
    {
        Handler handler = new Handler();
        print();
        print();
        pr_cl("    WELCOME               ", fg:ConsoleColor.White, bg:ConsoleColor.Blue);
        print("  DOS-win_10 Console      ");
        print("  Console Control System  ");
        print();
        print();
        pr_cl("execute> ", end:"");
        pr_cl(fg: ConsoleColor.Green, bg: ConsoleColor.Black, end:"");
        bool a = handler.Handle(Console.ReadLine());
        while(a)
        {
            pr_cl("\nexecute> ", end: "");
            pr_cl(fg: ConsoleColor.Green, bg: ConsoleColor.Black, end: "");
            a = handler.Handle(Console.ReadLine());
        }
    }

    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Program program = new Program();
        settings = new Settings();

        ConsoleFullScreen.GoFullScreen();
        program.Loop();

        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
    }
}

public class ConsoleFullScreen
{
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_MAXIMIZE = 3;
    private const int SW_RESTORE = 9;

    private static bool _isFullScreen = false; // Флаг для отслеживания текущего состояния

    public static void GoFullScreen()
    {
        IntPtr handle = GetConsoleWindow();
        if (handle != IntPtr.Zero)
        {
            ShowWindow(handle, SW_MAXIMIZE);
            _isFullScreen = true; // Обновляем флаг состояния
        }
    }
    public static void RestoreScreen()
    {
        IntPtr handle = GetConsoleWindow();
        if (handle != IntPtr.Zero)
        {
            ShowWindow(handle, SW_RESTORE);
            _isFullScreen = false; // Обновляем флаг состояния
        }
    }
    public static void ToggleFullScreen()
    {
        if (_isFullScreen)
        {
            RestoreScreen();
        }
        else
        {
            GoFullScreen();
        }
    }
}