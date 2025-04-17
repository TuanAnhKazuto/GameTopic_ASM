using Fusion;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : NetworkBehaviour
{
    [Networked] public MatchState matchState { get; set; }
    [Networked] public int PlayerCount { get; set; }

    public Text player1Name;
    public Text player2Name;

    public GameObject waitPanel;

    void Start()
    {
        waitPanel.SetActive(true);
    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            if (PlayerCount == 2)
            {
                waitPanel.SetActive(false); // ẩn panel chờ khi có đủ 2 người chơi
            }
        }
            Debug.Log("Player Count: " + PlayerCount);

    }

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            PlayerCount++;

            if (PlayerCount == 2)
            {
                matchState = new MatchState
                {
                    isPlayer1Ready = false,
                    isPlayer2Ready = false,
                    
                };
            }

            Debug.Log("Player Count: " + PlayerCount);
        }
    }

    public void ReadyBtn()
    {
        Rpc_PlayerReady(Object.InputAuthority); // gọi hàm RPC để thông báo cho server
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.StateAuthority)]
    private void Rpc_PlayerReady(PlayerRef who)
    {
        var players = Runner.ActivePlayers.ToList();

        if (players.Count < 2)
        {
            Debug.LogWarning("Chưa đủ 2 người chơi để xác nhận Ready.");
            return;
        }

        if (who == players[0])
            Player1IsReady();
        else if (who == players[1])
            Player2IsReady();

        
    }

    private void Player1IsReady()
    {
        var state = matchState; // tạo bản sao để chỉnh sửa
        state.isPlayer1Ready = true;

        player1Name.color = Color.green; // đổi màu chữ thành xanh lá cây

        matchState = state; // gán lại vào Networked struct
        Debug.Log("Player 1 is ready: state = " + state.isPlayer1Ready);
    }

    private void Player2IsReady()
    {
        var state = matchState; // tạo bản sao để chỉnh sửa
        state.isPlayer2Ready = true;

        player2Name.color = Color.green; // đổi màu chữ thành xanh lá cây

        matchState = state; // gán lại vào Networked struct
        Debug.Log("Player 2 is ready: state = " + state.isPlayer2Ready);
    }
}