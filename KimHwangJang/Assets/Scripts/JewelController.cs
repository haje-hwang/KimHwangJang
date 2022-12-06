using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelController : MonoBehaviour
{
    public GameObject[] jewels;

    public int jewels_Value;

    [SerializeField]
    GameController gameController;
    
    void Start()
    {
        gameController = GetComponent<GameController>();
        if(jewels_Value == 0){
            jewels_Value = 100;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")){
            Destroy(gameObject);
            gameController.score += jewels_Value;
        }
    }
}
