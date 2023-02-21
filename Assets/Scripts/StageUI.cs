using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Function: StageUI
// Name: Myungsub Eum
// Number: 101168160
// Last Uopdate: 20201207
// Description: stage UI can print the score

public class StageUI : MonoBehaviour
{
    public Text WinText;
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.stageClear)
        {
            WinText.enabled = true;
        }
        else
        {
            WinText.enabled = false;
        }

        scoreText.text = "Score: " + GameManager.Instance.stagePoint;
    }
}
