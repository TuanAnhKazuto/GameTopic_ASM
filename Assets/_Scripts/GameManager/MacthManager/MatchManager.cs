using Fusion;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchManager : NetworkBehaviour
{
    [Networked] public MatchState matchState { get; set; }
    [Networked] public int PlayerCount { get; set; }

    public Text player1Name;
    public Text player2Name;

    public float timeToStart = 15;
    public Text timeCountDown;

    public GameObject waitPanel;

    private void Start()
    {
        waitPanel.SetActive(true);
        player1Name.color = Color.red;
        player2Name.color = Color.red;
        timeCountDown.text = timeToStart.ToString();

        Debug.Log(Object);
    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            if (PlayerCount == 2)
            {
                waitPanel.SetActive(false); 
            }
        }

    }

    public void NextScene()
    {
        SceneManager.LoadScene("SelectionShip");
    }

    private void Update()
    {
        
        if (timeToStart > 0)
        {
            timeToStart -= (int)Time.deltaTime;

            if(timeToStart < 10)
            {
                timeCountDown.text = "0" + timeToStart.ToString();
            }
            else
            {
                timeCountDown.text = timeToStart.ToString();
            }
        }
    }

    public void AddPlayerExternally()
    {
        if (!Object.HasStateAuthority) return;

        Debug.Log("Player Count: " + PlayerCount);

        if (PlayerCount == 2)
        {
            matchState = new MatchState
            {
                isPlayer1Ready = false,
                isPlayer2Ready = false
            };
        }
    }

    public void ReadyBtn()
    {
        Rpc_PlayerReady(Object.InputAuthority);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.StateAuthority)]
    private void Rpc_PlayerReady(PlayerRef who)
    {
        var players = Runner.ActivePlayers.ToList();

        if (players.Count < 2)
        {
            
            return;
        }

        if (who == players[0])
            Player1IsReady();
        else if (who == players[1])
            Player2IsReady();
    }

    private void Player1IsReady()
    {
        var state = matchState;
        state.isPlayer1Ready = true;
        player1Name.color = Color.green;
        matchState = state;

        Debug.Log("Player 1 is ready: " + state.isPlayer1Ready);
    }

    private void Player2IsReady()
    {
        var state = matchState;
        state.isPlayer2Ready = true;
        player2Name.color = Color.green;
        matchState = state;

        Debug.Log("Player 2 is ready: " + state.isPlayer2Ready);
    }
}