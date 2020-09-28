using System;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.Net.DevGuide
{
    public class SyncExample : ConnectionBase
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Before calling PrintDocumentAsync on thread {0}.",
              Thread.CurrentThread.ManagedThreadId);

            await new SyncExample().ExecuteAsync().ConfigureAwait(false);


            Console.WriteLine("After calling PrintDocumentAsync on thread {0}.",
                Thread.CurrentThread.ManagedThreadId);
        }

        public override async Task ExecuteAsync()
        {
            //Connect to Couchbase
            await ConnectAsync().ConfigureAwait(false);

            //call it synchronously with await
            await PrintDocumentAsync("somekey").ConfigureAwait(false);
        }

        public async Task PrintDocumentAsync(string id)
        {
            Console.WriteLine("Before awaiting GetDocumentAsync on thread {0}.",
                Thread.CurrentThread.ManagedThreadId);

            var doc = await Bucket.DefaultCollection().GetAsync(id).ConfigureAwait(false);

            Console.WriteLine("After awaiting GetDocumentAsync on thread {0}.",
                Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine(doc.ContentAs<string>());
        }
    }
}