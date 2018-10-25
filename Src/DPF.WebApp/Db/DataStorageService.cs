using System;
using System.Threading.Tasks;
using DPF.WebApp.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace DPF.WebApp.Db
{
    public class SlowDataStorageService
    {
        private readonly DataStorageServiceOptions _options;
        private readonly DocumentClient _client;

        public SlowDataStorageService(DataStorageServiceOptions options)
        {
            _options = options;
            _client = new DocumentClient(options.Endpoint, options.ApiKey);

            _client.CreateDatabaseIfNotExistsAsync(new Database { Id = options.DatabaseName }).GetAwaiter().GetResult();
            _client.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(options.DatabaseName),
                    new DocumentCollection { Id = options.CollectionName })
                .GetAwaiter().GetResult();
        }

        public async Task InsertFamily(TelemetryEntry telemetryEntry)
        {
            var collectionUri = UriFactory.CreateDocumentCollectionUri(_options.DatabaseName, _options.CollectionName);
            await _client.CreateDocumentAsync(collectionUri, telemetryEntry);
        }
    }

    public class DataStorageService
    {
        private readonly AsyncLazy<DocumentClient> _clientFactory;
        private readonly Uri _collectionUri;

        public DataStorageService(DataStorageServiceOptions options)
        {
            _collectionUri = UriFactory.CreateDocumentCollectionUri(options.DatabaseName, options.CollectionName);
            _clientFactory = new AsyncLazy<DocumentClient>(async () =>
            {
                var client = new DocumentClient(options.Endpoint, options.ApiKey);

                await client.CreateDatabaseIfNotExistsAsync(new Database { Id = options.DatabaseName });
                await client.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(options.DatabaseName),
                    new DocumentCollection { Id = options.CollectionName });

                return client;
            });
        }

        public async Task InsertFamily(TelemetryEntry telemetryEntry)
        {
            var client = await _clientFactory.Value;
            await client.CreateDocumentAsync(_collectionUri, telemetryEntry);
        }
    }

    public class AsyncLazy<T> : Lazy<Task<T>>
    {
        public AsyncLazy(Func<Task<T>> valueFactory) : base(valueFactory)
        {
        }
    }
}
