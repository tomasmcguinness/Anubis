using Anubis.Web.Entities;
using Anubis.Web.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Anubis.Web.Managers
{
    public class LogManager
    {
        public int GetLogMessageCount(int ownerId, string applicationCode, string dataCentre)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["TableStorage"]);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            string tableName = GetTableName(ownerId);

            if (tableClient.DoesTableExist(tableName))
            {
                TableServiceContext serviceContext = tableClient.GetDataServiceContext();
            }

            return 0;
        }


        public void RecordLogMessage(int ownerId, LogModel log)
        {
            RecordLogMessage(ownerId, null, log);
        }

        public void RecordLogMessage(int ownerId, string applicationCode, LogModel log)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["TableStorage"]);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            string tableName = GetTableName(ownerId);

            tableClient.CreateTableIfNotExist(tableName);

            TableServiceContext serviceContext = tableClient.GetDataServiceContext();

            // Create a new customer entity
            LogEntity customer1 = new LogEntity(ownerId, applicationCode);
            customer1.Message = log.Message;
            customer1.Timestamp = DateTime.UtcNow;
            customer1.LogLevel = log.Level;

            // Add the new customer to the people table
            serviceContext.AddObject(tableName, customer1);

            // Submit the operation to the table service
            serviceContext.SaveChanges();
        }

        private string GetTableName(int ownerId)
        {
            string tableName = string.Format("logs{0}", ownerId);
            return tableName;
        }
    }
}