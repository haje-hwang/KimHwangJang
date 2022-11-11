using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        int rand = Random.Range(0,100);
        if(rand > 50){
            Debug.Log("아이템 드랍");
        }
        else{
            Debug.Log("아이템 미드랍");
        }
    }
}

