using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;

namespace Notifications.Infrastructure
{
    public static class DocumentClientFactory
    {
        public static IDocumentClient Create()
        {
            var client = new DocumentClient(new Uri("http://localhost:8081"), "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
            var database = client.CreateDatabaseIfNotExistsAsync(new Database()
            {
                Id = "RunFabulous"
            }).Result;

            var collection = client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("RunFabulous"), new DocumentCollection()
            {
                Id = "Notifications"
            }).Result;

            return client;
        }
    }
}
