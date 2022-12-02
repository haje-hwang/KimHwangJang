using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardingController : MonoBehaviour
{
    GameObject playerSpawn;

    Vector3 pSpawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if(Input.GetKeyDown(KeyCode.E)){
                playerSpawn = other.transform.parent.Find("Spawnpoint").gameObject;
                Debug.Log(playerSpawn);

                pSpawnpoint = playerSpawn.transform.position;
                Debug.Log(pSpawnpoint);

                other.transform.position = pSpawnpoint;
                Debug.Log(other.transform.position);
            }
        }
    }
}
