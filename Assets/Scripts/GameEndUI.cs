using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Function: EndUI
// Name: Myungsub Eum
// Number: 101168160
// Last Uopdate: 20201207
// Description: Game end scene UI. restart and quit function

public class GameEndUI : MonoBehaviour
{
    public Text scoreText;
    // Update is called once per frame
    private void Start()
    {
        SoundManager.instance.Play("Win");
    }
    void Update()
    {
        scoreText.text = "Your Score: " + GameManager.Instance.totalPoint;
    }

    public void Retry()
    {
        SoundManager.instance.Stop("Win");
        GameManager.Instance.playerHP = 3;
        GameManager.Instance.stageClear = false;
        GameManager.Instance.totalPoint = 0;
        GameManager.Instance.stagePoint = 0;
        GameManager.Instance.curStage = 0;
        GameManager.Instance.SceneChange("Stage1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
