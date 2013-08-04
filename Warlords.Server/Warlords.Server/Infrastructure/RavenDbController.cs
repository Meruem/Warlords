using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Ninject;
using Raven.Client;
using Raven.Client.Document;
using Warlords.Server.Application.Infrastructure;

namespace Warlords.Server.Infrastructure
{
    public class RavenDbController : ApiController
    {
        [Inject]
        public IDocumentStore Store { get; set; }

        [Inject]
        public IHubService  Service { get; set; }

        public IAsyncDocumentSession Session { get; set; }

        public async override Task<HttpResponseMessage> ExecuteAsync(
            HttpControllerContext controllerContext,
            CancellationToken cancellationToken)
        {
            using (Session = Store.OpenAsyncSession())
            {
                var result = await base.ExecuteAsync(controllerContext, cancellationToken);
                await Session.SaveChangesAsync();

                return result;
            }
        }
    }
}