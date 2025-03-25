using Fusion;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public NetworkRunner runnerPrefab;
    private NetworkRunner runner;

    async void Start()
    {
        if (runner == null)
        {
            runner = Instantiate(runnerPrefab); // Tạo NetworkRunner nếu chưa có
        }

        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Host,  // Có thể đổi thành GameMode.Client nếu bạn muốn tham gia game
            SessionName = "MyGameSession",
            Scene = null,  // Chỉ định ID của Scene (hoặc để null nếu không cần load scene)
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        };

        var result = await runner.StartGame(startGameArgs);

        if (result.Ok)
        {
            Debug.Log("✅ Network Runner đã chạy thành công!");
        }
        else
        {
            Debug.LogError("❌ Lỗi khi chạy Network Runner: " + result.ErrorMessage);
        }
    }
}
