using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    //사용할때는 주석처리 빼고 씬 이름 넣어주세요.
    public void OnStartButtonClick(){
        Debug.Log("Start");
        SceneManager.LoadScene("JangSampleScene");
    }

    public void OnExitButtonClick(){
        Debug.Log("Exit");
        Application.Quit();
    }
}
