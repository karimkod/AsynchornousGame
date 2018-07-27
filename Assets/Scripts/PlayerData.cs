using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
#region /GameData
    [SerializeField]
    private int bestScore;
    public int BestScore { get { return bestScore; } }
    #endregion /GameData

    #region /PlayerPrefs
    [SerializeField]
    private bool isSoundPlaying; 
    public bool IsSoundPlaying { set { isSoundPlaying = value; } get { return isSoundPlaying; } }
#endregion /PlayerPrefs

    public PlayerData()
    {
        bestScore = 0;
        isSoundPlaying = true;
    }

    public bool IsNewBest(int newScore)
    {
        if (newScore > bestScore)
        {
            bestScore = newScore;
            return true; 
        }else
        {
            return false;
        }
    }

}
