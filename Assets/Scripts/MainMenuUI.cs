using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Function: MainMenuUI
// Name: Myungsub Eum
// Number: 101168160
// Last Uopdate: 20201207
// Description: MainMenuUI UI script. how to play and start function

public class MainMenuUI : MonoBehaviour
{
    public GameObject howToPlayObj;

    private bool onHowToPlay;
    private void Start()
    {
        SoundManager.instance.Play("MainMenu");
    }
    public void StartGame()
    {
        SoundManager.instance.Stop("MainMenu");
        GameManager.Instance.SceneChange("Stage1");
    }
    public void HowToPlay()
    {
        if(onHowToPlay)
        {
            onHowToPlay = false;
            howToPlayObj.SetActive(false);
        }
        else
        {
            onHowToPlay = true;
            howToPlayObj.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
