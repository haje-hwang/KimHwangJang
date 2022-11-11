using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool isPickup;
    // Start is called before the first frame update
    void Start()
    {
        isPickup = false;
    }
    void Pickup(){
        isPickup = true;
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Obstacle"){
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water"){
            Destroy(this.gameObject);
        }
    }
}

