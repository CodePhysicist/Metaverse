using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoConnect : MonoBehaviour
{
    public NetworkManager networkManager;
    // Start is called before the first frame update
    void Start()
    {
        networkManager.networkAddress = "localhost";
        string conn = PlayerPrefs.GetString("Connection");
        if (conn == "Host")
            networkManager.StartHost();
        else if (conn == "Client")
            networkManager.StartClient();
    }

}
