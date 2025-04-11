using UnityEngine;

public class OffscreenIndicator : MonoBehaviour
{
    public Transform target;            // Mục tiêu cần theo dõi
    public Camera mainCamera;          // Camera chính
    public RectTransform arrowUI;      // UI mũi tên
    public Canvas Canvas;              // Canvas gốc
    public float edgeBuffer = 50f;     // Khoảng cách từ rìa màn hình

    void Update()
    {
        if (target == null || arrowUI == null || mainCamera == null) return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

        bool isOffscreen = screenPos.z < 0 ||
            screenPos.x < 0 || screenPos.x > Screen.width ||
            screenPos.y < 0 || screenPos.y > Screen.height;

        arrowUI.gameObject.SetActive(isOffscreen);

        if (isOffscreen)
        {
            Vector3 dir = (target.position - mainCamera.transform.position).normalized;
            dir.y = 0; // Chỉ cần xoay theo hướng ngang
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            arrowUI.rotation = Quaternion.Euler(0, 0, -angle);

            Vector3 cappedScreenPos = screenPos;

            cappedScreenPos.x = Mathf.Clamp(cappedScreenPos.x, edgeBuffer, Screen.width - edgeBuffer);
            cappedScreenPos.y = Mathf.Clamp(cappedScreenPos.y, edgeBuffer, Screen.height - edgeBuffer);
            cappedScreenPos.z = 0;

            Vector2 canvasPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas.transform as RectTransform, cappedScreenPos, Canvas.worldCamera, out canvasPos);
            arrowUI.anchoredPosition = canvasPos;
        }
    }
}
