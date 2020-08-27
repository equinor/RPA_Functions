﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

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

        public async Stream downloadFileFromBlob(string fileName)
        {

        }

        private async Task getBlobContainer(string containerName)
        {

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
