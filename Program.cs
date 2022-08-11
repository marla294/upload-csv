using System;

namespace UploadCSVFile
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("sample.csv");
            string data = reader.ReadLine();
            data = reader.ReadLine();
        }
    }
}