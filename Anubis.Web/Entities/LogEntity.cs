using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anubis.Web.Entities
{
    public class LogEntity : TableServiceEntity
    {
        public LogEntity(int ownerId, string applicationName)
        {
            this.PartitionKey = ownerId.ToString();
            this.RowKey = applicationName;
        }

        public string Message { get; set; }
    }
}