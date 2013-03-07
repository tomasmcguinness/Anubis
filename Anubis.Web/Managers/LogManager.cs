using Anubis.Web.Data;
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
        public int GetLogMessageCount(int ownerId, string applicationCode, int regionId)
        {
            CloudStorageAccount storageAccount = GetStorageAccountForRegion(regionId);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            string tableName = GetTableName(ownerId);

            if (tableClient.DoesTableExist(tableName))
            {
                TableServiceContext serviceContext = tableClient.GetDataServiceContext();
                return serviceContext.CreateQuery<LogEntity>(tableName).Where(a => a.PartitionKey.CompareTo(applicationCode) == 0).ToList().Count();
            }

            return 0;
        }

        private CloudStorageAccount GetStorageAccountForRegion(int regionId)
        {
            switch (regionId)
            {
                case 50:
                    return CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StoreWestEurope"]);
            }

            throw new NotImplementedException();
        }

        public void RecordLogMessage(Anubis.Data.Application app, LogModel log)
        {
            CloudStorageAccount storageAccount = GetStorageAccountForRegion(50);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            string tableName = GetTableName(app.OwnerId);

            tableClient.CreateTableIfNotExist(tableName);

            TableServiceContext serviceContext = tableClient.GetDataServiceContext();

            // Create a new customer entity
            LogEntity customer1 = new LogEntity(app.OwnerId, app.Code);
            customer1.Message = log.Message;
            customer1.Timestamp = DateTime.UtcNow;
            customer1.LogLevel = log.Level;

            // Add the new customer to the people table
            serviceContext.AddObject(tableName, customer1);

            // Submit the operation to the table service
            serviceContext.SaveChanges();
        }

        public List<LogEntity> GetLogMessages(int ownerId, string applicationCode, int regionId)
        {
            CloudStorageAccount storageAccount = GetStorageAccountForRegion(regionId);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            string tableName = GetTableName(ownerId);

            TableServiceContext serviceContext = tableClient.GetDataServiceContext();
            return serviceContext.CreateQuery<LogEntity>(tableName).Where(a => a.PartitionKey.CompareTo(applicationCode) == 0).ToList().OrderByDescending(a => a.Timestamp).ToList();
        }

        private string GetTableName(int ownerId)
        {
            string tableName = string.Format("logs{0}", ownerId);
            return tableName;
        }
    }
}