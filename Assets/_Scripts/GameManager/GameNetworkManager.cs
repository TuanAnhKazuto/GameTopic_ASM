using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;

public class GameNetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public MatchManager matchManager;

    [SerializeField] private NetworkRunner _runner;
    private NetworkSceneManagerDefault _sceneManager;

    private void Awake()
    {
        _runner = FindAnyObjectByType<NetworkRunner>();
        _runner.AddCallbacks(this);
        _sceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>();

        matchManager = FindAnyObjectByType<MatchManager>();

        Time.timeScale = 0f;

        ConnetToFusion();
    }

    async void ConnetToFusion()
    {
        _runner.ProvideInput = true; // cho phép gửi input

        string sessionName = "MySession"; // tên session

        // Cấu hình kết nối chung
        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SceneManager = _sceneManager,
            SessionName = sessionName,
            PlayerCount = 2,
            IsVisible = true,
            IsOpen = true,
        };

        var resust = await _runner.StartGame(startGameArgs);
        if (resust.Ok)
        {
            Debug.Log("Connected to Fusion");
        }
        else
        {
            Debug.LogError("Failed to connect to Fusion");
        }
    }

    private IEnumerator WaitUntilMatchManagerReady()
    {
        while (matchManager == null || matchManager.Object == null)
        {
            Debug.Log("Waiting for MatchManager to be ready...");
            yield return null; // chờ frame sau
        }

        if (matchManager.Object.HasStateAuthority)
        {
            matchManager.AddPlayerExternally();
            Debug.Log("Player Count: " + matchManager.PlayerCount);
        }
    }


    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        //Debug.Log("Player joined: " + player);
        StartCoroutine(WaitUntilMatchManagerReady());
        
        var count = runner.ActivePlayers.Count();
        Debug.Log(count);
        if (count == 2)
        {
            matchManager.waitPanel.SetActive(false);
            matchManager.isHas2Players = true;
        }
    }


    public void OnConnectedToServer(NetworkRunner runner)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }
}
