namespace Warlords.Server.ApplicationF
    open System
    open Warlords.Server.DomainF.Events
    open Raven.Client
    open System.Linq

    type RavenEvent =
        {
            AggregateType : string
            AggregateId : Guid
            Event : Event
        }

    module RavenDBEventStore =
        let getEventsForAggregate aggregateType id (documentStore:IDocumentStore) =
            use session = documentStore.OpenSession()
            List.ofSeq<Event>(session.Query<RavenEvent>().Where(fun e -> e.AggregateType = aggregateType && e.AggregateId = id).Select(fun e -> e.Event).ToList())

        let saveEvents aggregateType id version (documentStore:IDocumentStore) events =
            use session = documentStore.OpenSession()
            events |> Seq.iter(fun e -> session.Store( {RavenEvent.AggregateId = id; AggregateType = aggregateType; Event = e}))
            do session.SaveChanges()
            events
