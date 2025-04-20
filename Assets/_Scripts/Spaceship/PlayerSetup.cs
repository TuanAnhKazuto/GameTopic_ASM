using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSetup : NetworkBehaviour
{
    public GameObject ship;
    public Transform target;
    public PlayerHP playerHp;    

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
    public void SetUpUIHp()
    {
        if (!Object.HasStateAuthority) return;
        Slider _hpSlider = GameObject.Find("Slider Health").GetComponent<Slider>();
        if (_hpSlider != null)
        {
            playerHp = ship.GetComponent<PlayerHP>();
            playerHp.hpSlider = _hpSlider;
        }
    }
}
