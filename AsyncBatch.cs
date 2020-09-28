using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase.KeyValue;

namespace Couchbase.Net.DevGuide
{
    class AsyncBatch : ConnectionBase
    {
        static async Task Main(string[] args)
        {
            await new AsyncBatch().ExecuteAsync().ConfigureAwait(false);
            Console.Read();
        }

        public override async Task ExecuteAsync()
        {
            //Connect to Couchbase
            await ConnectAsync().ConfigureAwait(false);

            var ids = new List<string> { "doc1", "doc2", "doc4" };

            // ReSharper disable once IdentifierTypo
            var upserts = new List<Task<IMutationResult>>();
            ids.ForEach(x => upserts.Add(Bucket.DefaultCollection().UpsertAsync(x, x))); 
            await Task.WhenAll(upserts).ConfigureAwait(false);

            var gets = new List<Task<IGetResult>>();
            ids.ForEach(x => gets.Add(Bucket.DefaultCollection().GetAsync(x)));

            var results = await Task.WhenAll(gets).ConfigureAwait(false);
            results.ToList().ForEach(doc => Console.WriteLine("Removed " + doc.ContentAs<dynamic>()));
        }
    }
}
