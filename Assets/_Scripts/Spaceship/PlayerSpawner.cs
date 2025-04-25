using UnityEngine;
using Fusion;
using System.Collections;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public LoadCharacter loadCharacter;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            var prefab = loadCharacter.shipPrefab;

            var position = new Vector3(0, 0, 0);
            var rotation = Quaternion.identity;

            // Fix for CS0029: Explicitly compare the integer value to determine the condition  
            if (PlayerPrefs.GetInt("GameMode") == 1)
            {
                position = new Vector3(-348, 20, 1612);
                rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (PlayerPrefs.GetInt("GameMode") == 2)
            {
                int playerIndex = 0;
                int index = 0;
                foreach (var p in Runner.ActivePlayers)
                {
                    if (p == player)
                    {
                        playerIndex = index;
                        break;
                    }
                    index++;
                }

                if (playerIndex == 0) // Player 1
                {
                    position = new Vector3(0, 0, 200);
                    rotation = Quaternion.Euler(0, 180, 0);
                }
                else if (playerIndex == 1) // Player 2
                {
                    position = new Vector3(0, 0, -200);
                    rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            // spawn character  
            Runner.Spawn(prefab,
                position,
                rotation,
                Runner.LocalPlayer,
                (runner, obj) =>
                {
                    var playerSetup = obj.GetComponent<PlayerSetup>();
                    if (playerSetup != null)
                    {
                        playerSetup.SetUpCamera();
                        playerSetup.SetUpUIHp();
                        playerSetup.SetUpOffScreen();
                    }

                    var playerGun = obj.GetComponent<PlayerFlightControl>();
                    if (playerGun != null) playerGun.runner = runner;
                });
        }
    }

    //public LoadCharacter loadCharacter;

    //private void Start()
    //{
    //    // Chờ cho đến khi NetworkRunner sẵn sàng
    //    StartCoroutine(SpawnWhenReady());
    //}

    //private IEnumerator SpawnWhenReady()
    //{
    //    // Đợi đến khi Runner đã sẵn sàng
    //    while (RunnerManager.Instance == null || RunnerManager.Instance.Runner == null)
    //    {
    //        yield return null;
    //    }

    //    var runner = RunnerManager.Instance.Runner;

    //    // Đảm bảo chỉ spawn nếu là local player
    //    if (runner.IsRunning && runner.IsServer && runner.LocalPlayer != null)
    //    {
    //        bool alreadySpawned = false;
    //        foreach (var obj in FindObjectsOfType<NetworkObject>())
    //        {
    //            if (obj.HasStateAuthority && obj.HasInputAuthority)
    //            {
    //                alreadySpawned = true;
    //                break;
    //            }
    //        }

    //        if (!alreadySpawned)
    //        {
    //            SpawnPlayer(runner, runner.LocalPlayer);
    //        }
    //    }
    //}

    //private void SpawnPlayer(NetworkRunner runner, PlayerRef player)
    //{
    //    var prefab = loadCharacter.shipPrefab;
    //    var position = Vector3.zero;
    //    var rotation = Quaternion.identity;

    //    int playerIndex = 0;
    //    int index = 0;
    //    foreach (var p in runner.ActivePlayers)
    //    {
    //        if (p == player)
    //        {
    //            playerIndex = index;
    //            break;
    //        }
    //        index++;
    //    }

    //    // Có thể tùy vào GameMode để đặt vị trí khác
    //    if (PlayerPrefs.GetInt("GameMode") == 1)
    //    {
    //        position = new Vector3(-348, 20, 1612);
    //        rotation = Quaternion.Euler(0, 180, 0);
    //    }
    //    else if (PlayerPrefs.GetInt("GameMode") == 2)
    //    {
    //        if (playerIndex == 0)
    //        {
    //            position = new Vector3(0, 0, 200);
    //            rotation = Quaternion.Euler(0, 180, 0);
    //        }
    //        else
    //        {
    //            position = new Vector3(0, 0, -200);
    //            rotation = Quaternion.Euler(0, 0, 0);
    //        }
    //    }

    //    runner.Spawn(prefab, position, rotation, player, (runner, obj) =>
    //    {
    //        var setup = obj.GetComponent<PlayerSetup>();
    //        if (setup != null)
    //        {
    //            setup.SetUpCamera();
    //            setup.SetUpUIHp();
    //        }

    //        var flight = obj.GetComponent<PlayerFlightControl>();
    //        if (flight != null)
    //        {
    //            flight.runner = runner;
    //        }
    //    });
    //}

}
