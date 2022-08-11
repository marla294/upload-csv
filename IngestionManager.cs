namespace UploadCSVFile
{
    public class IngestionManager
    {
      public ColumnMappings mappings;

      public void IngestDataset(string fileName) 
      {
        StreamReader reader = new StreamReader(fileName);
        string header = reader.ReadLine();

        SetColumnMappings(header);

        List<Row> data = new List<Row>();

        string currentLine;

        while ((currentLine = reader.ReadLine()) != null) {
          Row row = PopulateRow(currentLine);
          data.Add(row);
        }

        //string line1Data = reader.ReadLine();
      }

      public Row PopulateRow(string currentLine) {
        Row row = new Row();

        return row;
      }

      public void SetColumnMappings(string header) 
      {
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
      }
    }
}