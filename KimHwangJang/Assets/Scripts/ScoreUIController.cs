using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIController : MonoBehaviour
{
    public Text text;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }

    public void GetScore(int point){
        score = point;
        SetText();
    }

    public void SetText(){
        text.text = score.ToString();
    }
}

