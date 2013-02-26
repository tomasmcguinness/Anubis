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
        public void RecordLogMessage(int ownerId, LogModel log)
        {
            RecordLogMessage(ownerId, null, log);
        }

        public void RecordLogMessage(int ownerId, string applicationName, LogModel log)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["TableStorage"]);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            tableClient.CreateTableIfNotExist("1");

            TableServiceContext serviceContext = tableClient.GetDataServiceContext();

            // Create a new customer entity
            LogEntity customer1 = new LogEntity(ownerId, applicationName);
            customer1.Message = log.Message;

            // Add the new customer to the people table
            serviceContext.AddObject("1", customer1);

            // Submit the operation to the table service
            serviceContext.SaveChangesWithRetries();
        }
    }
}