using FishNet.Managing;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkGameManager : NetworkBehaviour
{
    NetworkManager networkManager;
    MatchmakingSystem matchmakingSystem;

    private void Awake()
    {
        networkManager = FindObjectOfType<NetworkManager>();
        matchmakingSystem = FindObjectOfType<MatchmakingSystem>();
        matchmakingSystem.OnStatusUpdate += (string msg, bool inBool) => SystemMsg(msg);
    }

    private void Start()
    {
        Debug.Log("Game Manager Start");
        //Server auto start on ServerManager startOnHeadless toggle
    }

    public void ClientConnect()
    {
        matchmakingSystem.FindDeployedServers();
    }
    void SystemMsg(string msg)
    {
        Debug.Log(msg);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("StartClient");
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("StartServer");
    }
}
