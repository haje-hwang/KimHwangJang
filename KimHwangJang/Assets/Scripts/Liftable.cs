using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liftable : MonoBehaviour
{
    protected Transform tr;
    protected Rigidbody rb;
    //원래 이 옵젝의 부모였던 오브젝트 풀
    protected Transform OriginalParent;
    public bool isCarring;
    protected void init(){
        isCarring = false;
        tr = transform;
        OriginalParent = tr.parent;
        rb = GetComponent<Rigidbody>();
    }
    public virtual void mount_on_head(Transform where){
        isCarring = true;
        tr.SetParent(where);
        rb.isKinematic = true;
        tr.SetPositionAndRotation(where.position, Quaternion.identity);
    }
    public virtual void demount_on_head(){
        isCarring = false;
        tr.SetParent(OriginalParent);
        rb.isKinematic = false;
    }
}
