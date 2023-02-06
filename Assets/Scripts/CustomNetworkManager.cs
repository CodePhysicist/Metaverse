using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
    public GameObject playerPrefab2;
    public GameObject playerPrefab3;

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<CharacterCreatorMessage>(OnCreateCharacter);
        NetworkServer.RegisterHandler<ChatMessage>(OnChat);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        CharacterCreatorMessage characterCreatorMessage = new CharacterCreatorMessage
        {
            name = PlayerPrefs.GetString("Name"),
            character = PlayerPrefs.GetString("Character")
        };

        NetworkClient.Send(characterCreatorMessage);
    }


    void OnCreateCharacter(NetworkConnectionToClient conn, CharacterCreatorMessage message)
    {

        // playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example
        GameObject gameobject;
        if (message.character == "Man")
            gameobject = Instantiate(playerPrefab);
        else if(message.character == "Girl")
            gameobject = Instantiate(playerPrefab2);
        else
            gameobject = Instantiate(playerPrefab3);

        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        PlayerController player = gameobject.GetComponent<PlayerController>();
        player.nameInGame = message.name;

        // call this to use this gameobject as the primary controller
        NetworkServer.AddPlayerForConnection(conn, gameobject);
    }

    void OnChat(NetworkConnectionToClient conn, ChatMessage chatMessageObj)
    {
        NetworkServer.SendToAll<ChatMessage>(chatMessageObj);
    }
}
