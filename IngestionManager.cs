namespace UploadCSVFile
{
    public class IngestionManager
    {
      public ColumnMappings mappings = new ColumnMappings();

      public char[] separators = { ',' };

      public string header;

      public void IngestDataset(string fileName) 
      {
        StreamReader reader = new StreamReader(fileName);
        header = reader.ReadLine();

        SetColumnMappings(header);

        List<Row> data = new List<Row>();

        string currentLine;

        while ((currentLine = reader.ReadLine()) != null) {
          Row row = PopulateRow(currentLine);
          data.Add(row);
        }

        var query = data.GroupBy(
          r => r.InsuranceCompany,
          r => r,
          (key, g) => new Grouping { InsuranceCompany = key, Rows = g.ToList()}
        );

        foreach (var grouping in query) {
          OutputInsuranceData(grouping);
        }

      }

      public void OutputInsuranceData(Grouping grouping) {
        List<Row> output = new List<Row>();

        var query = grouping.Rows.GroupBy(
          r => r.UserId,
          r => r,
          (key, g) => new { UserId = key, Rows = g.ToList() }
        );

        foreach (var group in query) {
          if (group.Rows.Count > 1) {
            var highestVersion = group.Rows.Max(row => row.Version);
            var row = group.Rows.First(row => row.Version == highestVersion);
            output.Add(row);
          }
          else {
            output.Add(group.Rows.First());
          }
        }

        List<Row> sortedOutput = output.OrderBy(row => row.LastName + row.FirstName).ToList();

        System.Text.StringBuilder csv = new System.Text.StringBuilder();
        csv.AppendLine(header);

        foreach (var row in sortedOutput) {
          string line = "";
          
          for (int column = 0; column < 5; column++) {
            if (column == mappings.UserId) {
              line = line + $"{row.UserId},";
            }
            if (column == mappings.FirstName) {
              line = line + $"{row.FirstName},";
            }
            if (column == mappings.LastName) {
              line = line + $"{row.LastName},";
            }
            if (column == mappings.Version) {
              line = line + $"{row.Version},";
            }
            if (column == mappings.InsuranceCompany) {
              line = line + $"{row.InsuranceCompany},";
            }
          }
          line = line.Remove(line.Length - 1, 1);
          
          csv.AppendLine(line);
        }
        Directory.CreateDirectory("./OutputFiles/");
        File.WriteAllText($"./OutputFiles/{grouping.InsuranceCompany}.csv", csv.ToString());
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