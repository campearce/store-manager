using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace StoreManager.Model.Endpoints
{
    public class JsonFilesStoreManagerEndpoint : BaseStoreManagerEndpoint
    {
        public JsonFilesStoreManagerEndpoint(string directory)
        {
            if (!string.IsNullOrEmpty(directory))
            {
                var absoluteDirectory = Path.GetDirectoryName(directory);
                if (!Directory.Exists(absoluteDirectory))
                {
                    var directoryInfo = Directory.CreateDirectory(absoluteDirectory);
                    absoluteDirectory = directoryInfo.FullName;
                }

                FileStoreLocation = absoluteDirectory;
            }

            Console.WriteLine($"Json file store setup to read from: {FileStoreLocation}");
        }

        public JsonFilesStoreManagerEndpoint()
            :this(string.Empty)
        { }

        private string FileStoreLocation { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StoreManager.Store");

        public override string ValidationFailMessage { get; set; }

        public override IEnumerable<T> Execute<T>(string endpoint, HttpMethod method)
        {
            return Execute<T>(endpoint, method, null);
        }

        public override IEnumerable<T> Execute<T>(string endpoint, HttpMethod method, object body)
        {
            var endpointSplits = endpoint.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var fileName = endpointSplits[0];
            var filePath = Path.Combine(FileStoreLocation, $"{fileName}.json");

            if (File.Exists(filePath))
            {
                var data = File.ReadAllText(filePath);
                var response = new List<T>();

                if (data.StartsWith("{"))
                {
                    var singleResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
                    response.Add(singleResponse);
                }
                else
                {
                    response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(data);
                }

                var methodName = Enum.GetName(typeof(HttpMethod), method)?.ToUpper();

                switch (methodName)
                {
                    case "GET":
                        return response;

                    default:
                        return null;
                }
            }

            using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine("[]");
            }
            return new List<T>();
        }

        protected override bool Validate()
        {
            if (!System.IO.Directory.Exists(FileStoreLocation))
            {
                ValidationFailMessage = $"Could not locate the JSON Store at: {FileStoreLocation}";
                var directory = Directory.CreateDirectory(FileStoreLocation);
                return directory.Exists;
            }

            return true;
        }
    }
}
