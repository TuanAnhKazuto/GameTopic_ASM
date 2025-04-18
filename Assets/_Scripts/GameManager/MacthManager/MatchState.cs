using UnityEngine;
using Fusion;

public struct MatchState : INetworkStruct
{
    public bool isPlayer1Ready;
    public bool isPlayer2Ready;
    public float startTime;
}
