using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        Aim();
        //Reload();
        //Shoot();
        //Cleanup();

        if(Input.GetMouseButtonDown(0)){
            Reload();
        }

        if(Input.GetButtonDown("Jump")){
            Shoot();
        }

        if(Input.GetMouseButtonDown(1)){
            Cleanup();
        }
    }

    void Reload(){
        if(!isDirty && !isReload){
            isReload = true;
            Debug.Log("재장전");
        }
    }

    void Aim(){
        xAim = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0f, xAim, 0f) * rotateSpeed * Time.deltaTime);
        
    }
    
    void Shoot(){
        if(isReload && !isDirty){
            Instantiate(bullet, shootpoint.position, Quaternion.identity);

            isReload = false;
            isDirty = true;
            Dirty = 5;

            Debug.Log("발사");
        }
    }

    void Cleanup(){
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
