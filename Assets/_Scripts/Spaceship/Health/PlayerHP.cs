using UnityEngine;
using Fusion;
using UnityEngine.UI;

public class PlayerHP : NetworkBehaviour
{
    [Networked] public int Health { get; set; } = 100;

    public Slider hpSlider;

    public override void Render()
    {
        if(hpSlider == null) return;
        hpSlider.value = Health;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RpcTakeDamage(int damage)
    {
        if (Object.HasStateAuthority)
        {
            Health = Mathf.Clamp(Health - damage, 0, 100);
            Debug.Log($"[{Object.InputAuthority}] bị trúng đạn, còn {Health} máu");

            if (Health <= 0)
            {
                Health = 0;
                GameObject gameOver = GameObject.Find("LosePanel");
                gameObject.SetActive(true);
                Time.timeScale = 0f;
                Runner.Despawn(Object);
            }
        }
    }
}
