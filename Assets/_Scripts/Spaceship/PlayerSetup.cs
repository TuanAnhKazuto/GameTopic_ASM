using Fusion;
using UnityEngine;
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

    public void SetUpOffScreen()
    {
        if (!Object.HasStateAuthority) return;
        OffscreenIndicator offScreenIndicator = FindAnyObjectByType<OffscreenIndicator>();
        if (offScreenIndicator != null)
        {
            offScreenIndicator.target = target;
        }
    }
}
