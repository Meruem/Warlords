namespace Warlords.Server.DomainF.Events

    open System
    open Warlords.Server.Common

    type Event = 
        interface
        end

    type LobbyCreatedEvent =
        {
            LobbyGuid : Guid
        }
        interface Event

    type PlayerJoinedEvent =
        {
            UserName : string
            ConnectionId : string
        }
        interface Event

    type GameCreatedEvent =
        {
            OwnerName : string
        }
        interface Event

