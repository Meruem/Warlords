Feature: JoinLobby
	In order to be able to access lobby
	As a authenticated user
	I want to be able to join lobby

Scenario: Join Lobby
	Given The server is running
	When I join the lobby
	Then I'm on a list of players joined in lobby
	And Everybody receives message notifying about me joining


Scenario: Refresh users connection id
	Given The server is running
	And I'm already joined in lobby
	When I join the lobby
	Then I'm on a list of players joined in lobby
	And My connection id is refreshed to new value
