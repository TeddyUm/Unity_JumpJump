using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Function: GameManager
// Name: Myungsub Eum
// Number: 101168160
// Last Uopdate: 20201207
// Description: Gamemanager instance

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    #endregion

    public int totalPoint;
    public int stagePoint;
    public int playerHP;
    public int curStage;
    public bool stageClear;

    public void NextStage()
    {
        playerHP = 3;
        stageClear = true;
        totalPoint += stagePoint;
        stagePoint = 0;
        curStage++;
        if(curStage == 1)
            Invoke("Stage1End", 2.0f);
        else if (curStage == 2)
            Invoke("Stage2End", 2.0f);
        else if (curStage == 3)
            Invoke("Stage3End", 2.0f);
    }

    void Stage1End()
    {
        stageClear = false;
        SceneChange("Stage2");
    }
    void Stage2End()
    {
        stageClear = false;
        SceneChange("Stage3");
    }
    void Stage3End()
    {
        SoundManager.instance.Stop("Stage");
        stageClear = false;
        SceneChange("GameEnd");
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
