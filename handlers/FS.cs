using System.Runtime.InteropServices;
using static Program;

class FileSystem
{
    public void Check(string comand, string[] args)
    {
        if(comand == "fs")
        {
            Console.Clear();
            loop();
        }
    }

    Filer file = new Filer("");
    int page = 1;
    void loop()
    {
        
    }

    void NormalRender()
    {

    }
}