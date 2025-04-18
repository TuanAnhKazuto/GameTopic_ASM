using UnityEngine;

public class OffscreenIndicator : MonoBehaviour
{
    public Transform target;
    public Camera mainCamera;
    public RectTransform arrowUI;           // Icon chỉ hướng
    public RectTransform targetCrosshair;   // Tâm khi thấy địch
    public Canvas Canvas;
    public float edgeBuffer = 50f;

    void Update()
    {
        if (target == null || mainCamera == null) return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

        bool isOffscreen = screenPos.z < 0 ||
            screenPos.x < 0 || screenPos.x > Screen.width ||
            screenPos.y < 0 || screenPos.y > Screen.height;

        // Hiện mũi tên nếu offscreen là nó nằm rìa màn hình á
        if (arrowUI != null)
        {
            arrowUI.gameObject.SetActive(isOffscreen);

            if (isOffscreen)
            {
                // vị trí nè
                Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                Vector2 dir = (new Vector2(screenPos.x, screenPos.y) - new Vector2(screenCenter.x, screenCenter.y)).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                arrowUI.rotation = Quaternion.Euler(0, 0, angle);

                Vector3 cappedScreenPos = screenPos;
                cappedScreenPos.x = Mathf.Clamp(cappedScreenPos.x, edgeBuffer, Screen.width - edgeBuffer);
                cappedScreenPos.y = Mathf.Clamp(cappedScreenPos.y, edgeBuffer, Screen.height - edgeBuffer);
                cappedScreenPos.z = 0;

                Vector2 canvasPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas.transform as RectTransform, cappedScreenPos, Canvas.worldCamera, out canvasPos);
                arrowUI.anchoredPosition = canvasPos;
            }
        }

        // Hiện tâm thấy địch nếu onscreen
        if (targetCrosshair != null)
        {
            targetCrosshair.gameObject.SetActive(!isOffscreen);

            if (!isOffscreen)
            {
                Vector2 canvasPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas.transform as RectTransform, screenPos, Canvas.worldCamera, out canvasPos);
                targetCrosshair.anchoredPosition = canvasPos;
            }
        }
    }
}
