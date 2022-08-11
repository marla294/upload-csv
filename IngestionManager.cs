namespace UploadCSVFile
{
    public class IngestionManager
    {
        public void IngestDataset(string dataSetName) 
        {
            StreamReader reader = new StreamReader(dataSetName);
            string header = reader.ReadLine();
            string line1Data = reader.ReadLine();
        }
    }
}