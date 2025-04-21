using System.Runtime.InteropServices;
using static Program;

class FileSystem
{
    public void Check(string comand, string[] args)
    {
        if(comand == "fs")
        {
            Console.Clear();
            Separate(args);
        }
    }

    void Separate(string[] args)
    {
        bool nc = args.Contains("-nc");
        string last = "";
        foreach(string arg in args)
        {

        }

        if (!nc) {
            loop();
        }
    }

    Filer file = new Filer("");
    int page = 1;
    void loop()
    {
        FS_main fs = new FS_main();
        fs.loop();
        fs.Save();
    }
}