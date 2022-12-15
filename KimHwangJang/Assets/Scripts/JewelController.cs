using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelController : MonoBehaviour
{
    public GameObject[] jewels;

    public int jewels_Value;
    public int score = 0;


    [SerializeField]
    ScoreUIController scoreUIController;
    GameController gameController;
    
    void Start()
    {
        score = 0;
        gameController = GetComponent<GameController>();
    }

    public void GetScore()
    {
        score += jewels_Value;
        scoreUIController.GetScore(score);
    }
}
