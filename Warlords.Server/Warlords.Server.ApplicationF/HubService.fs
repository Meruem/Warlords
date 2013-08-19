namespace Warlords.Server.ApplicationF
    
    open System
    open Warlords.Server.Common
    open Warlords.Server.DomainF
    open Warlords.Server.DomainF.Events
    open Raven.Client
    open Raven.Abstractions.Data
    open Warlords.Server.ApplicationF.RavenDBEventStore
    open Dynamic
    open TaskHelper

    type IHubService = 
        abstract member Send : Message -> unit
        abstract member Publish : Event -> unit

    type HubService(documentStore: IDocumentStore, clientSender : IClientSender) =
        interface IHubService with

            member this.Publish message =
                match message with

                    | :? LobbyCreatedEvent as e ->
                        use session = documentStore.OpenSession()
                        do session.Advanced.DocumentStore.DatabaseCommands.DeleteByIndex("AllPlayersIndex", new IndexQuery()) |> ignore
                        do session.SaveChanges()
                        do session.Store({ CurrentLobby.LobbyId = e.LobbyGuid}, "/CurrentLobby/1")
                        do session.SaveChanges()

                    | :? PlayerJoinedEvent as e ->
                        use session = documentStore.OpenSession()
                        do session.Store({ Player.Name = e.UserName})
                        do session.SaveChanges()

                        do clientSender.SendToAllClients({PlayerJoinedMessage.Name = e.UserName})

                    | _ -> ignore()

            member this.Send message = 
                let hub = this :> IHubService
                let publish = Seq.iter(fun event -> hub.Publish event) 
                match message with
                       
                    | :? CreateLobbyCommand as c -> 
                        let newId = Guid.NewGuid()
                        Lobby.create newId |> saveEvents "Lobby" newId -1 documentStore |> publish
                       
                    | :? JoinPlayerInLobbyCommand as c -> 
                        use session = documentStore.OpenSession()
                        let currentLobby : CurrentLobby = session.Load<CurrentLobby> "/CurrentLobby/1"
                        let lobbyId = currentLobby.LobbyId
                        let lobby = getEventsForAggregate "Lobby" lobbyId documentStore |> Lobby.replayLobby
                        lobby |> Lobby.joinPlayer c.UserName c.ConnectionId |> saveEvents "Lobby" lobbyId -1 documentStore |> publish

                    | _ -> ignore()
        


