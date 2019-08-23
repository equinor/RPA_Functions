using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;



namespace rpa_functions
{
    class CommonTable
    {
        static string tableConnectionString = Environment.GetEnvironmentVariable("STORAGE_CONNECTION");
        CloudTableClient tableClient;
        CloudTable table;

        public CommonTable(string tableName)
        {
            this.tableClient = getStorageClient();
            this.table = this.tableClient.GetTableReference(tableName);
        }


        public async Task<TableResult> InsertorReplace(ITableEntity tableEntity)
        {
            TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(tableEntity);

            try
            {
                // Execute the operation.
                return await table.ExecuteAsync(insertOrReplaceOperation);
         
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        public async Task<List<T>> RetrieveEntities<T>(string field, string queryComp, int searchValue) where T : TableEntity, new()
        {
            try
            {
                // Create the Table Query Object for Azure Table Storage  
                TableQuery<T> DataTableQuery = new TableQuery<T>();
                List<T> DataList = new List<T>();
                TableQuerySegment<T> segment;
                TableContinuationToken continuationToken = null;

                TableQuery<T> query = new TableQuery<T>()
                 .Where(TableQuery.GenerateFilterConditionForInt(field, queryComp, searchValue));
                

                do
                {
                    segment = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                    if (segment == null)
                    {
                        break;
                    }
                    DataList.AddRange(segment);

                } while (continuationToken != null);
                return DataList;

            }
            catch (Exception ExceptionObj)
            {
                throw ExceptionObj;
            }
        }

        public async Task<List<T>> RetrieveEntities<T>(string field, string queryComp, string searchValue) where T : TableEntity, new()
        {
            try
            {
                // Create the Table Query Object for Azure Table Storage  
                TableQuery<T> DataTableQuery = new TableQuery<T>();
                List<T> DataList = new List<T>();
                TableQuerySegment<T> segment;
                TableContinuationToken continuationToken = null;

                TableQuery<T> query = new TableQuery<T>()
                 .Where(TableQuery.GenerateFilterCondition(field, queryComp, searchValue));


                do
                {
                    segment = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                    if (segment == null)
                    {
                        break;
                    }
                    DataList.AddRange(segment);

                } while (continuationToken != null);
                return DataList;

            }
            catch (Exception ExceptionObj)
            {
                throw ExceptionObj;
            }
        }


        // Generic table handling
        private static CloudTableClient getStorageClient()
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(tableConnectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            return tableClient;
        }
        private static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }

    }
}
