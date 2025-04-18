using UnityEngine;
using Fusion;

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
            if (PlayerPrefs.GetInt("GameMode", 1) == 1)
            {
                position = new Vector3(-348, 10, 1612);
                rotation = Quaternion.Euler(0, 180, 0);
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
                    }

                    var playerGun = obj.GetComponent<PlayerFlightControl>();
                    if (playerGun != null) playerGun.runner = runner;
                });
        }
    }

    public void Coop()
    {

    }
}
