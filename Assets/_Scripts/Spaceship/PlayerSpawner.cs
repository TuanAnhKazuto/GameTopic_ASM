using UnityEngine;
using Fusion;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject shipPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            var position = new Vector3(0,0,0);

            // spawn character
            Runner.Spawn(shipPrefab,
                position,
                Quaternion.identity,
                Runner.LocalPlayer,
                (runner, obj) =>
                {
                    var playerSetup = obj.GetComponent<PlayerSetup>();
                    if(playerSetup != null)
                    {
                        playerSetup.SetUpCamera();
                    }

                    var playerGun = obj.GetComponent<PlayerFlightControl>();
                    if (playerGun != null) playerGun.runner = runner;

                });
        }
    }
}
