using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class PlayerSetup : NetworkBehaviour
{
    public GameObject ship;
    public Transform target;

    public void SetUpCamera()
    {
        if (!Object.HasStateAuthority) return;

        CameraFlightFollow camera = FindAnyObjectByType<CameraFlightFollow>();
        if (camera != null)
        {
            camera.control = ship.GetComponent<PlayerFlightControl>();
            camera.target = target;
            camera.customPointer.enabled = true;
            camera.demoUI.enabled = true;
        }        
    }
}
