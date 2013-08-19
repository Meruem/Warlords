namespace Warlords.Server.DomainF

    open System
    open Warlords.Server.DomainF.Events
    open AggregateRoot

    module Lobby = 

        type State =
            {
                Id : Guid
                Players : String list
            }

        let create id = fire { LobbyCreatedEvent.LobbyGuid = id }

        let isPlayerJoined name state = List.exists (fun p -> p = name) state.Players

        let joinPlayer name connectionId state = 
            if name = String.Empty then invalidArg "player name"  "is empty"
            if isPlayerJoined name state then invalidOp "Already joined"
            fire { PlayerJoinedEvent.UserName = name; ConnectionId = connectionId }

        let applyOnLobby s (e: Event) =
            match e with
            | :? LobbyCreatedEvent as e -> {Id = e.LobbyGuid; Players = [] } 
            | :? PlayerJoinedEvent as e -> {s with Players = e.UserName :: s.Players; }
            | _ -> s

        let initialState = { Id = Guid.Empty; Players = []}
        let replayLobby events = replayAggregate initialState applyOnLobby events
