namespace UploadCSVFile
{
    public class IngestionManager
    {
      public ColumnMappings mappings = new ColumnMappings();

      public char[] separators = { ',' };

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

      }

      public Row PopulateRow(string currentLine) {
        Row row = new Row();

        var data = currentLine.Split(separators);
        int column = 0;

        foreach (var cell in data) {
          if (column == mappings.UserId) {
            row.UserId = cell;
          }
          if (column == mappings.FirstName) {
            row.FirstName = cell;
          }
          if (column == mappings.LastName) {
            row.LastName = cell;
          }
          if (column == mappings.Version) {
            row.Version = Int32.Parse(cell);
          }
          if (column == mappings.InsuranceCompany) {
            row.InsuranceCompany = cell;
          }

          column++;
        }

        return row;
      }

      public void SetColumnMappings(string header) 
      {
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