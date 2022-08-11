namespace UploadCSVFile
{
    public class IngestionManager
    {
      public void IngestDataset(string fileName) 
      {
        StreamReader reader = new StreamReader(fileName);
        string header = reader.ReadLine();

        ColumnMappings mappings = GetColumnMappings(header);
        //string line1Data = reader.ReadLine();
      }

      public ColumnMappings GetColumnMappings(string header) 
      {
        ColumnMappings mappings = new ColumnMappings();
        char[] separators = { ',' };
        var data = header.Split(separators);
        int counter = 0;

        foreach (var cell in data) 
        {
          counter++;
        }

        return mappings;
      }
    }
}