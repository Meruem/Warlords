namespace Warlords.Server.DomainF

    open Warlords.Server.DomainF.Events

    module AggregateRoot =

        let fire o =
            [o :> Event]

        let replay = Seq.fold

        let replayAggregate empty apply (events: seq<Event>) =
            replay apply empty events


