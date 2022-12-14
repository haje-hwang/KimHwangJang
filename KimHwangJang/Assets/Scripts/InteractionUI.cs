using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    TextMeshProUGUI resourceText;
    private void Awake()
    {
        resourceText = GetComponent<TextMeshProUGUI>();
    }
    public void ShowMsg(string msg){
        gameObject.SetActive(true);
        resourceText.text = msg;
    }
    public void HideMsg(){
        if(gameObject.activeSelf){
            gameObject.SetActive(false);
        }
    }
}
