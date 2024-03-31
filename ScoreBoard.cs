using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class ScoreBoard : NetworkBehaviour
{
    [SyncVar(hook = nameof(Test))] private int _leftScore;
    [SyncVar] private int _rightScore;

    public static event Action<int> ClientOnLeftScoreUpdated;
    public static event Action<int> ClientOnRightScoreUpdated;

    public override void OnStartServer()
    {
        Ball.rightGoal += ServerHandlerRightGoal;
        Ball.leftGoal += ServerHandlerLeftGoal;
    }

    public override void OnStopServer()
    {
        Ball.rightGoal -= ServerHandlerRightGoal;
        Ball.leftGoal -= ServerHandlerLeftGoal;
    }

    private void ServerHandlerRightGoal()
    {
        _leftScore++;
        
    }

    private void Test(int oldScore, int newScore)
    {
        ClientOnLeftScoreUpdated.Invoke(_leftScore);
    }

    private void ServerHandlerLeftGoal()
    {
        _rightScore++;
        ClientOnRightScoreUpdated.Invoke(_rightScore);
    }

}
