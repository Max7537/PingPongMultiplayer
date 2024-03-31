using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _leftText;
    [SerializeField] private TMP_Text _rightText;

    void OnEnable()
    {
        ScoreBoard.ClientOnLeftScoreUpdated += HandleLeftScoreUpdate;
        ScoreBoard.ClientOnRightScoreUpdated += HandleRightScoreUpdate;
    }

    private void HandleRightScoreUpdate(int obj)
    {
        _leftText.text = obj.ToString();
    }

    private void HandleLeftScoreUpdate(int obj)
    {
        _rightText.text = obj.ToString();
    }

    void OnDestroy()
    {
        ScoreBoard.ClientOnLeftScoreUpdated -= HandleLeftScoreUpdate;
        ScoreBoard.ClientOnRightScoreUpdated -= HandleRightScoreUpdate;
    }
}
