using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Ball_Pile : Liftable
{
    private void Start(){
        init();
    }
    public override void mount_on_head(Transform where){
        isCarring = true;
        tr.SetParent(where);
        rb.isKinematic = true;
        tr.SetPositionAndRotation(where.position, Quaternion.identity);
    }
    public override void demount_on_head(){
        isCarring = false;
        tr.SetParent(OriginalParent);
        rb.isKinematic = false;
    }
}
