// https://www.cyotek.com/blog/upload-data-to-blob-storage-with-azure-functions

using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text;

namespace BlobFunction
{
    public static class BlobFunction
    {
		// fill in blob storage details below
        private static string accountName = "";
        private static string accessKey = "";
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=" + accountName + ";AccountKey=" + accessKey + ";EndpointSuffix=core.windows.net";
        private static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

        [FunctionName("BlobFunction")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            requestBody = requestBody.Replace(System.Environment.NewLine, " ") + "\n";

            string fileName = "blob-demo";

            await AppendBlob(fileName, requestBody, log);

            return new OkObjectResult("Success");
        }

        private async static Task AppendBlob(string name, string data, ILogger log)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient client;
            CloudBlobContainer container;
            CloudAppendBlob blob;

            client = storageAccount.CreateCloudBlobClient();
            container = client.GetContainerReference(accountName);
            await container.CreateIfNotExistsAsync();

            blob = container.GetAppendBlobReference(name);

            //bool blobExists = await blob.ExistsAsync();

            if (!(await blob.ExistsAsync()))
            {
                await blob.CreateOrReplaceAsync();
            }

            //blob.Properties.ContentType = "application/json";

            using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(data + "\n")))
            {
                //await blob.UploadFromStreamAsync(stream);
                await blob.AppendTextAsync(data);
                log.LogInformation("Appended Data");
            }
        }

    }
}
