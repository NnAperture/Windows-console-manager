using static Program;

public class FS_main
{
    int page = 0;
    bool cmd_mode = false;
    Filer file = new Filer("fs_inf.win");
    List<Page> pages = new List<Page>();
    public FS_main() {

    }
    public void loop()
    {
        if (page >= pages.Count) { page = pages.Count - 1;
            if (page == -1) { Default(); }
        }
        Console.Clear();
        while (true)
        {
            Render();
            string key = KeyCombination.GetComb();
            if (FS_Menu.IsPressed("down_selected", key)) { pages[page].SelMove(1); }
            if (FS_Menu.IsPressed("up_selected", key)) { pages[page].SelMove(-1); }
            if (FS_Menu.IsPressed("menu", key)) { FS_Menu.Menu(); }
        }
    }
    void Render()
    {
        pr_cl();
        Console.Clear();
        print();
        for(int i = 1; i < 11; i++)
        {
            ConsoleColor bg = ConsoleColor.DarkBlue;
            ConsoleColor fg = ConsoleColor.White;
            if (i - 1 < pages.Count)
            {
                bg = ConsoleColor.Blue;
            }
            if (i - 1 == page)
            {
                fg = ConsoleColor.Black;
            }
            pr_cl("page", end: " ", bg: bg, fg: fg);
            if (i == 10) { print(0, end: ""); } else { print(i, end: ""); }
            pr_cl(end: " ");
        }
        print($"  Press {FS_Menu.controls["menu"][0]} to open menu.");
        pages[page].Render(cmd_mode);
    }
    void Load()
    {

    }
    void Default()
    {
        page = 0;
        pages.Add(new Page("", 0));
    }
    public void Save()
    {

    }
}