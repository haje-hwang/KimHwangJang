using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    GameObject food1, food2, food3;

    [SerializeField]
    GameObject jewel1, jewel2, jewel3;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        int foodRand = Random.Range(0,100);
        int jewelRand = Random.Range(0,100);
        
        if(foodRand < 50){
            Instantiate(food1,this.transform.position + new Vector3(Random.Range(-3.0f,3.0f), 0, Random.Range(-3.0f,3.0f)),Quaternion.identity);
            Debug.Log("음식 1드랍");
        }
        else if(foodRand >= 51 && foodRand < 80) {
            Instantiate(food2,this.transform.position + new Vector3(Random.Range(-3.0f,3.0f), 0, Random.Range(-3.0f,3.0f)),Quaternion.identity);
            Debug.Log("음식 2드랍");
        }
        else{
            Instantiate(food3,this.transform.position + new Vector3(Random.Range(-3.0f,3.0f), 0, Random.Range(-3.0f,3.0f)),Quaternion.identity);
            Debug.Log("음식 3드랍");
        }

        if(jewelRand < 50){
            Instantiate(jewel1,this.transform.position + new Vector3(Random.Range(-3.0f,3.0f), 0, Random.Range(-3.0f,3.0f)),Quaternion.identity);
            Debug.Log("보석 1드랍");
        }
        else if(jewelRand >= 51 && jewelRand < 80) {
            Instantiate(jewel2,this.transform.position + new Vector3(Random.Range(-3.0f,3.0f), 0, Random.Range(-3.0f,3.0f)),Quaternion.identity);
            Debug.Log("보석 2드랍");
        }
        else{
            Instantiate(jewel3,this.transform.position + new Vector3(Random.Range(-3.0f,3.0f), 0, Random.Range(-3.0f,3.0f)),Quaternion.identity);
            Debug.Log("보석 3드랍");
        }

    }
}

