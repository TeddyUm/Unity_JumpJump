using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Function: GameOverUI
// Name: Myungsub Eum
// Number: 101168160
// Last Uopdate: 20201207
// Description: Gameover scene UI. restart game and quit game

public class GameOverUI : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.Play("GameOver");
    }

    //retry and init data
    public void Retry()
    {
        SoundManager.instance.Stop("GameOver");
        GameManager.Instance.playerHP = 3;
        GameManager.Instance.stageClear = false;
        GameManager.Instance.totalPoint = 0;
        GameManager.Instance.stagePoint = 0;
        GameManager.Instance.curStage = 0;
        GameManager.Instance.SceneChange("Stage1");
    }

    // quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
