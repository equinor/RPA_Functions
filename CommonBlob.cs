using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace rpa_functions
{
    class CommonBlob
    {
        string blobConnectionString = Environment.GetEnvironmentVariable("STORAGE_CONNECTION");
        
        CloudStorageAccount blobStorageAccount;
        CloudBlobClient blobClient;
        CloudBlobContainer cloudBlobContainer;

        public CommonBlob(string containerName)
        {
            blobClient = getBlobConnection();
            // Get and interpreter return value
            getBlobContainer(containerName);
            // Only set if created
            setBlobContainerPermissions();


        }

        public async Task<Uri> uploadFileToBlob(Stream inFile, string destFileName)
        {
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(destFileName);
            await cloudBlockBlob.UploadFromStreamAsync(inFile);
            return cloudBlockBlob.Uri;
      
        }

        public async Task<Stream> downloadStreamFromBlob(string fileName)
        {
            Stream returnStream = new MemoryStream();

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);

            Console.WriteLine(cloudBlockBlob.StorageUri.ToString());
            await cloudBlockBlob.DownloadToStreamAsync(returnStream);

            return returnStream;
        }

        public string convertToBase64(Stream inStream)
        {


            var bytes = new Byte[(int)inStream.Length];

            inStream.Seek(0, SeekOrigin.Begin);
            inStream.Read(bytes, 0, (int)inStream.Length);

            return (Convert.ToBase64String(bytes));
            
        }


        public async Task<List<IListBlobItem>> listFilesInContainer()
        {
            BlobContinuationToken continuationToken = null;
            List<IListBlobItem> results = new List<IListBlobItem>();

            do
            {
                var response = await cloudBlobContainer.ListBlobsSegmentedAsync(continuationToken);
                continuationToken = response.ContinuationToken;
                results.AddRange(response.Results);
            }
            while (continuationToken != null);
            return results;
        }

        private async Task getBlobContainer(string containerName)
        {
            Console.WriteLine(containerName);
            cloudBlobContainer = blobClient.GetContainerReference(containerName);
            
            await cloudBlobContainer.CreateIfNotExistsAsync();

        }

        private CloudBlobClient getBlobConnection()
        {
            if (CloudStorageAccount.TryParse(blobConnectionString, out blobStorageAccount))
            {
                return blobStorageAccount.CreateCloudBlobClient();
            }
            else
            {
                return null;
            }
        }

        private async Task setBlobContainerPermissions()
        {
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Off
            };

            await cloudBlobContainer.SetPermissionsAsync(permissions);        
        }  

    }
}
