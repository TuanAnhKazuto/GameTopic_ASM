using UnityEngine;
using UnityEngine.UI;

public class StoryPaper : MonoBehaviour
{
  
    public Transform playerCamera;
    public float raycastRange = 3f;
    public GameObject Panelnote; // UI Panel chứa cốt truyện
    private GameObject currentItem;
    private bool isReading = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Panelnote.activeSelf)
            {
                CloseStory();
            }
            else
            {
                CheckForItem();
            }
        }
    }

    void CheckForItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, raycastRange))
        {
            if (hit.collider.CompareTag("Note")) // Đặt tag cho vật phẩm
            {
                currentItem = hit.collider.gameObject;
                currentItem.SetActive(false); // Ẩn vật phẩm
                ShowStory();
            }
        }
    }

    void ShowStory()
    {
        Panelnote.SetActive(true);
        isReading = true;
        Time.timeScale = 0f; // Dừng thời gian
    }

    void CloseStory()
    {
        Panelnote.SetActive(false);
        if (currentItem != null)
        {
            currentItem.SetActive(true); // Hiện lại vật phẩm
            currentItem = null;
        }
        isReading = false;
        Time.timeScale = 1f; // Tiếp tục thời gian
    }
}



