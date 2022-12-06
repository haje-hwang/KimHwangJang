using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public void OnStartButtonClick(){
        Debug.Log("Start");
        //SceneManager.LoadScene("");
    }

    public void OnExitButtonClick(){
        Debug.Log("Exit");
        Application.Quit();
    }
}
