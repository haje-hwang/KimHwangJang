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
    
    void Start()
    {
        //scoreUIController = GetComponent<ScoreUIController>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            GetScore(jewels_Value);
            Destroy(gameObject);
        }
    }

    public void GetScore(int point)
    {
        score += point;
        scoreUIController.GetScore(score);
    }
}
