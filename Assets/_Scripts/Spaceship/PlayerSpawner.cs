using UnityEngine;
using Fusion;
using System.Collections;

public class PlayerSpawner : NetworkBehaviour
{
    //public LoadCharacter loadCharacter;

    //public void PlayerJoined(PlayerRef player)
    //{
    //    if (player == Runner.LocalPlayer)
    //    {
    //        var prefab = loadCharacter.shipPrefab;

    //        var position = new Vector3(0, 0, 0);
    //        var rotation = Quaternion.identity;

    //        // Fix for CS0029: Explicitly compare the integer value to determine the condition  
    //        if (PlayerPrefs.GetInt("GameMode") == 1)
    //        {
    //            position = new Vector3(-348, 20, 1612);
    //            rotation = Quaternion.Euler(0, 180, 0);
    //        }
    //        else if(PlayerPrefs.GetInt("GameMode") == 2)
    //        {
    //            int playerIndex = 0;
    //            int index = 0;
    //            foreach (var p in Runner.ActivePlayers)
    //            {
    //                if (p == player)
    //                {
    //                    playerIndex = index;
    //                    break;
    //                }
    //                index++;
    //            }

    //            if (playerIndex == 0) // Player 1
    //            {
    //                position = new Vector3(0, 0, 200);
    //                rotation = Quaternion.Euler(0, 180, 0);
    //            }
    //            else if (playerIndex == 1) // Player 2
    //            {
    //                position = new Vector3(0, 0, -200);
    //                rotation = Quaternion.Euler(0, 0, 0);
    //            }
    //        }
    //        // spawn character  
    //        Runner.Spawn(prefab,
    //            position,
    //            rotation,
    //            Runner.LocalPlayer,
    //            (runner, obj) =>
    //            {
    //                var playerSetup = obj.GetComponent<PlayerSetup>();
    //                if (playerSetup != null)
    //                {
    //                    playerSetup.SetUpCamera();
    //                    playerSetup.SetUpUIHp();
    //                    playerSetup.SetUpOffScreen();
    //                }

    //                var playerGun = obj.GetComponent<PlayerFlightControl>();
    //                if (playerGun != null) playerGun.runner = runner;
    //            });
    //    }
    //}

    public LoadCharacter loadCharacter;
    NetworkRunner _runner;

    //private void Awake()
    //{
    //    if (prefab == null)
    //    {
    //        prefab = loadCharacter.shipPrefab;
    //    }
    //    else
    //    {
    //        Debug.Log("Prefab already assigned in inspector");
    //    }
    //        Debug.Log("prefab: " + prefab.name);
    //}

    private void Start()
    {
        // Chờ cho đến khi NetworkRunner sẵn sàng
        //StartCoroutine(SpawnWhenReady());
        SpawnWhenReady();

    }

    private void SpawnWhenReady()
    {
        // Đợi đến khi Runner đã sẵn sàng
        //while (RunnerManager.Instance == null || RunnerManager.Instance.Runner == null)
        //{
        //    Debug.Log("===Runner is not ready yet, waiting...===");
        //    yield return null;
        //}

        var runner = RunnerManager.Instance.Runner;
        Debug.Log($"Runner is ready: {runner.IsRunning}\n Runner Sever: {runner.IsServer}\n Runner Local: {runner.LocalPlayer}");

        // Đảm bảo chỉ spawn nếu là local player
        if (runner.IsRunning && runner.LocalPlayer != null)
        {
            bool alreadySpawned = false;
            foreach (var obj in FindObjectsOfType<NetworkObject>())
            {
                if (obj.HasStateAuthority && obj.HasInputAuthority)
                {
                    alreadySpawned = true;
                    break;
                }
            }

            if (!alreadySpawned)
            {
                SpawnPlayer(runner, runner.LocalPlayer);
            }
        }
        else
        {
            Debug.Log("===Runner is not running or not server, waiting...===");
        }
    }

    private void SpawnPlayer(NetworkRunner runner, PlayerRef player)
    {
        runner = _runner;
        if(!Object.HasStateAuthority) return;
        var prefab = loadCharacter.shipPrefab;
        var position = Vector3.zero;
        var rotation = Quaternion.identity;

        int playerIndex = 0;
        int index = 0;
        foreach (var p in runner.ActivePlayers)
        {
            if (p == player)
            {
                playerIndex = index;
                break;
            }
            index++;
        }

        // Có thể tùy vào GameMode để đặt vị trí khác
        if (PlayerPrefs.GetInt("GameMode") == 1)
        {
            position = new Vector3(-348, 20, 1612);
            rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (PlayerPrefs.GetInt("GameMode") == 2)
        {
            if (playerIndex == 0)
            {
                position = new Vector3(0, 0, 200);
                rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (playerIndex == 1)
            {
                position = new Vector3(0, 0, -200);
                rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        runner.Spawn(prefab, position, rotation, player, (runner, obj) =>
        {
            var setup = obj.GetComponent<PlayerSetup>();
            if (setup != null)
            {
                setup.SetUpCamera();
                setup.SetUpUIHp();
            }

            var flight = obj.GetComponent<PlayerFlightControl>();
            if (flight != null)
            {
                flight.runner = runner;
            }
        });
    }

}
