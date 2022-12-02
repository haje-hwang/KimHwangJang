using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    private Transform tr;
    private Rigidbody rb;
    private Transform OriginalParent;

    public enum Food
    {
        Potato, Fish, Meat, Apple
    };

    public Food food;
    public bool isCarring;
    public float fValue;
    private void Awake()
    {
        isCarring = false;
        tr = transform;
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OriginalParent = tr.parent;
    }
    public void mount_on_head(Transform where){
        isCarring = true;
        tr.SetParent(where);
        rb.isKinematic = true;
        tr.position = where.position;
    }
    public void demount_on_head(){
        isCarring = false;
        tr.SetParent(OriginalParent);
        rb.isKinematic = false;
    }
}
