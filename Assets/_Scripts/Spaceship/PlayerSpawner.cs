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

            var position = new Vector3(0,0,0);

            // spawn character
            Runner.Spawn(prefab,
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

    public void Coop()
    {

    }    

}
