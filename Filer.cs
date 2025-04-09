using System.ComponentModel;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;


public class Filer
{
    public string path = "";
    public bool read = false;
    public bool write = false;
    StreamWriter? writer;
    StreamReader? reader;

    public Filer(string p)
    {
        path = p;
    }
    public Filer(string p, bool clear)
    {
        path = p;
        Clear();
    }

    public void Close()
    {
        if(read)
        {
            reader.Close();
        }
        if(write)
        {
            writer.Close();
        }
        read = write = false;
    }

    public void Clear()
    {
        write = true;
        writer = new StreamWriter(path);
    }

    public void Write(string content)
    {
        if (!write)
        {
            Close();
            write = true;
            writer = new StreamWriter(path, true);
        }
        writer.Write(content);
    }
    public void WriteLine(string content)
    {
        if (!write)
        {
            Close();
            write = true;
            writer = new StreamWriter(path, true);
        }
        writer.Write(content + "\n");
    }
    public void Write(byte[] content)
    {
        if (!write)
        {
            Close();
            write = true;
            writer = new StreamWriter(path, true);
        }
        writer.Write(content);
    }

    public string Read(int count = 0)
    {
        if(!read)
        {
            Close();
            read = true;
            reader = new StreamReader(path);
        }
        if(count > 0)
        {
            string str = "";
            int character;
            for (int i = 0; i < count; i++)
            {
                character = reader.Read();
                if (character == -1)
                {
                    break;
                }
                str += (char)character;
            }
            return str;
        }
        else
        {
            string str = "";
            int character;
            while ((character = reader.Read()) != -1)
            {
                str += (char)character;
            }
            return str;
        }
    }
    public string Readline()
    {
        if (!read)
        {
            Close();
            read = true;
            reader = new StreamReader(path);
        }
        string str = "";
        string cur = Read(1);
        while (cur != "\n" && cur != "")
        {
            str += cur;
            cur = Read(1);
        }
        return str + cur;
    }
    public IEnumerator<string> GetEnumerator()
    {
        return new Enum(new Filer(path)); // Возвращаем экземпляр собственного класса IEnumerator
    }

    private class Enum : IEnumerator<string>
    {
        private Filer file = new Filer("");
        string cur = "";
        public Enum(Filer f)
        {
            file = f;
            //cur = file.Readline();
        }
        public bool MoveNext()
        {
            cur = file.Readline();
            return (cur != "");
        }
        public void Reset()
        {
            file.Close();
            //cur = file.Readline();
        }
        public string Current
        {
            get
            {
                return(cur);
            }
        }
        object IEnumerator.Current
        {
            get { return Current; } // Явная реализация нетипизированной версии
        }
        public void Dispose()
        {
            file.Close();
        }
    }
}