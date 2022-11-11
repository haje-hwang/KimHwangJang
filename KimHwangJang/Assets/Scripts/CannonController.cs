using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    int Dirty;

    bool isReload;
    bool isDirty;

    float rotateSpeed;

    float xAim;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform shootpoint;


    // Start is called before the first frame update
    void Awake()
    {
        isReload = false;
        isDirty = false;

        Dirty = 0;    
        rotateSpeed = 35.0f;
        //bullet = GetComponent<GameObject>();
    }
    public void Reload(){
        if(!isDirty && !isReload){
            isReload = true;
            Debug.Log("재장전");
        }
    }

    public void Aim(){
        xAim = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0f, xAim, 0f) * rotateSpeed * Time.deltaTime);  
    }
    
    public void Shoot(){
        if(isReload && !isDirty){
            Instantiate(bullet, shootpoint.position, Quaternion.identity);

            isReload = false;
            isDirty = true;
            Dirty = 5;

            Debug.Log("발사");
        }
    }

    public void Cleanup(){
        if(isDirty){
            if(Dirty <= 1){
                isDirty = false;
                Debug.Log("청소완료");
            }
            else{
                Dirty--;
                Debug.Log("청소중");
            }
        }
    }

}
