using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace WorkerRoleWithSBQueue1
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        const string QueueName = "errorlogcollectiontopic";

        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        SubscriptionClient Client;
        bool IsStopped;

        public override void Run()
        {
            while (!IsStopped)
            {
                try
                {
                    BrokeredMessage message = Client.Receive();

                    if (message != null)
                    {

                    }
                }
                catch (MessagingException e)
                {
                    if (!e.IsTransient)
                    {
                        Trace.WriteLine(e.Message);
                        throw;
                    }

                    Thread.Sleep(10000);
                }
                catch (OperationCanceledException e)
                {
                    if (!IsStopped)
                    {
                        Trace.WriteLine(e.Message);
                        throw;
                    }
                }
            }
        }

        public override bool OnStart()
        {
            // Create the queue if it does not exist already
            string connectionString = "Endpoint=sb://anubis.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=0P3oVJss/4SrHYVgr6SdsLUgjzH9wXec44ODUJtZwWo=";

            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.SubscriptionExists("errorlogcollectiontopic", "AllMessages"))
            {
                namespaceManager.CreateSubscription("errorlogcollectiontopic", "AllMessages");
            }

            // Initialize the connection to Service Bus Queue
            Client = SubscriptionClient.CreateFromConnectionString(connectionString, "errorlogcollectiontopic", "AllMessages", ReceiveMode.ReceiveAndDelete);
            Client.Receive();

            IsStopped = false;
            return base.OnStart();
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            IsStopped = true;
            Client.Close();
            base.OnStop();
        }
    }
}
