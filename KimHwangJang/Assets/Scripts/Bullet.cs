using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool isPickup;
    private Transform tr;
    private Rigidbody rb;
    private Transform OriginalParent;
    private ObjectPooling objectPool;
    private void Awake()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
    }
    private void start()
    {
        objectPool = GameObject.Find("ObjectPooling/CannonBallPool").GetComponent<ObjectPooling>();
        // GameObject.Find("CannonBallPool").GetComponent<CannonBallObjectPool>();
        isPickup = false;   
        OriginalParent =  GameObject.Find("ObjectPooling/CannonBallPool").transform;
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
    public void resetCannon(){
        rb.velocity = Vector3.zero;
        if(objectPool == null){
            objectPool = GameObject.Find("ObjectPooling/CannonBallPool").GetComponent<ObjectPooling>();
        } 
        objectPool.ReturnObject(this.gameObject);
    }

    public void mount_on_head(Transform where){
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        tr.SetParent(where);
        tr.SetPositionAndRotation(where.position, Quaternion.identity);
    }
    public void demount_on_head(){
        if(OriginalParent != null){
            tr.SetParent(OriginalParent);
        }
        else{
            tr.SetParent(null);
        }
        rb.isKinematic = false;
    }
}

