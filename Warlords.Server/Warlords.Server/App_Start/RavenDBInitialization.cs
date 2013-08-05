using System.Diagnostics.Contracts;
using Microsoft.AspNet.SignalR;
using Raven.Client;
using Raven.Client.Indexes;
using Warlords.Server.Application.ViewModels.Indexes;

namespace Warlords.Server.App_Start
{
    public class RavenDBInitialization
    {
        public static void InitializeIndexes()
        {
            var documentStore = GlobalHost.DependencyResolver.GetService(typeof(IDocumentStore)) as IDocumentStore;
            Contract.Assert(documentStore != null);

            IndexCreation.CreateIndexes(typeof (AllPlayersIndex).Assembly, documentStore);
        }
    }
}