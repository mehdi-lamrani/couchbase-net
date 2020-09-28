using System;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.Net.DevGuide
{
    public class AsyncExample : ConnectionBase
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Before calling PrintDocumentAsync on thread {0}.",
               Thread.CurrentThread.ManagedThreadId);

            await new AsyncExample().ExecuteAsync().ConfigureAwait(false);

            Console.WriteLine("After calling PrintDocumentAsync on thread {0}.",
             Thread.CurrentThread.ManagedThreadId);
        }

        public override async Task ExecuteAsync()
        {
            //Connect to Couchbase
            await ConnectAsync().ConfigureAwait(false);
            var id = "somekey";

            Console.WriteLine("Before awaiting GetDocumentAsync on thread {0}.",
                Thread.CurrentThread.ManagedThreadId);

            //add a document
            await Bucket.DefaultCollection().UpsertAsync(id, new { Name ="doc"}).ConfigureAwait(false);

            var doc = await Bucket.DefaultCollection().GetAsync(id).ConfigureAwait(false);

            Console.WriteLine("After awaiting GetDocumentAsync on thread {0}.",
                Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine(doc.ContentAs<dynamic>());
        }
    }
}
