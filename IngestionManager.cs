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
        int column = 0;

        foreach (var cell in data) 
        {
          if (cell == "User Id") {
            mappings.UserId = column;
          }
          if (cell == "First Name") {
            mappings.FirstName = column;
          }
          if (cell == "Last Name") {
            mappings.LastName = column;
          }
          if (cell == "Version") {
            mappings.Version = column;
          }
          if (cell == "Insurance Company") {
            mappings.InsuranceCompany = column;
          }

          column++;
        }

        return mappings;
      }
    }
}