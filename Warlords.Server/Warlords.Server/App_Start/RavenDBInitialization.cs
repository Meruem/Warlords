using System.Diagnostics.Contracts;
using Microsoft.AspNet.SignalR;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;
using AllPlayersIndex = Warlords.Server.DB.AllPlayersIndex;

namespace Warlords.Server.App_Start
{
    public class RavenDBInitialization
    {
        public static void InitializeIndexes()
        {
            var documentStore = GlobalHost.DependencyResolver.GetService(typeof(IDocumentStore)) as IDocumentStore;
            Contract.Assert(documentStore != null);

            IndexCreation.CreateIndexes(typeof (AllPlayersIndex).Assembly, documentStore);

            using (var session = documentStore.OpenSession())
            {
                session.Advanced.DocumentStore.DatabaseCommands.DeleteByIndex("AllPlayersIndex", new IndexQuery());
                session.SaveChanges();
            }
        }
    }
}