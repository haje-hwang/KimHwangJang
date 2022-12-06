using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool isPickup;
    private Rigidbody rb;
    private CannonBallObjectPool objectPool;
    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
    }
    void Start()
    {
        objectPool = GameObject.Find("CannonBallPool").GetComponent<CannonBallObjectPool>();
        isPickup = false;   
    }
    void SetPickup(bool input){
        isPickup = input;
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Obstacle"){
            resetCannon();
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water"){
            resetCannon();
            // Destroy(this.gameObject);
        }
    }
    private void resetCannon(){
        rb.velocity = Vector3.zero;
        objectPool.ReturnObject(this.gameObject);
    }
}

