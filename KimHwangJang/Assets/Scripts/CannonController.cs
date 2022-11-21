using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    int Dirty;

    bool isReloaded;
    bool isDirty;

    float rotateSpeed;

    float xAim;
    [SerializeField]
    private float shootspeed;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform shootpoint;

    private Transform CannonBallPool;
    private CannonBallObjectPool objectPool;

    // Start is called before the first frame update
    void Awake()
    {
        try
        {
            CannonBallPool = GameObject.Find("GameObjects/CannonBallPool").transform;
            objectPool = CannonBallPool.GetComponent<CannonBallObjectPool>();
        }
        catch (System.Exception)
        {
            Debug.Log("Error in Awake(), CannonController.cs");
            throw;
        }
        isReloaded = true;
        isDirty = false;

        Dirty = 0;    
        rotateSpeed = 35.0f;
        //bullet = GetComponent<GameObject>();
    }
    public void Reload(){
        if(!isDirty && !isReloaded){
            isReloaded = true;
            Debug.Log("재장전");
        }
    }

    public void Aim(){
        xAim = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0f, xAim, 0f) * rotateSpeed * Time.deltaTime);  
    }
    
    public void Shoot(){
        if(isReloaded && !isDirty){
            GameObject Bullet_instance;
            Rigidbody Bullet_rb;
            try
            {
                //object pooling 관리
                Bullet_instance = objectPool.GetObject();
                Bullet_instance.transform.position = shootpoint.position;
                Bullet_rb = Bullet_instance.GetComponent<Rigidbody>();
                Bullet_rb.AddForce(shootpoint.transform.forward * shootspeed, ForceMode.Impulse);
            }
            catch (System.Exception)
            {
                Debug.Log("Error in CannonController.Shoot()");
                throw;
            }

            isReloaded = false;
            isDirty = true;
            Dirty = 2;

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
