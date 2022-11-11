using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool isPickup;

    public float shootspeed;

    Rigidbody rigid;

    [SerializeField]
    Transform shootpoint;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        rigid.AddForce(shootpoint.transform.right * -shootspeed, ForceMode.Force);

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
}

