using System;

namespace UploadCSVFile
{
    class Program
    {
        static void Main(string[] args)
        {
            IngestionManager manager = new IngestionManager();
            manager?.IngestDataset("sample.csv");
        }
    }
}