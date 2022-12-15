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
    GameObject CannonPool;

    [SerializeField]
    Transform shootpoint;
    [SerializeField]
    GameObject ReloadNeedUI;

    private Transform CannonBallPool;
    private ObjectPooling objectPool;
    [SerializeField]
    private ParticleSystem ps;

    AudioSource audioSource;

    public AudioClip cannon_fire;

    // Start is called before the first frame update
    void Awake()
    {
        isReloaded = true;
        isDirty = false;

        Dirty = 0;    
        rotateSpeed = 35.0f;
        //bullet = GetComponent<GameObject>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        CannonBallPool = GameObject.Find("ObjectPooling/CannonBallPool").transform;
        objectPool = CannonBallPool.GetComponent<ObjectPooling>();
    }
    public void Reload(){
        if(!isReloaded){
            ReloadNeedUI.SetActive(false);
            isReloaded = true;
            Debug.Log("재장전");
        }
    }

    public void Aim(){
        xAim = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0f, xAim, 0f) * rotateSpeed * Time.deltaTime);  
    }
    
    public void Shoot(){
        // if(isReloaded && !isDirty){
        if(isReloaded){
            GameObject Bullet_instance;
            Rigidbody Bullet_rb;

            //object pooling 관리
            Bullet_instance = objectPool.GetObject();
            Bullet_instance.transform.position = shootpoint.position;
            Bullet_rb = Bullet_instance.GetComponent<Rigidbody>();
            Bullet_rb.AddForce(shootpoint.transform.forward * shootspeed, ForceMode.Impulse);

            

            ReloadNeedUI.SetActive(true);
            isReloaded = false;
            // isDirty = true;
            // Dirty = 2;
            // Debug.Log("발사");
            ps.Play();
            audioSource.clip = cannon_fire;
        }
        if (audioSource.clip != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    // public void Cleanup(){
    //     if(isDirty){
    //         if(Dirty <= 1){
    //             isDirty = false;
    //             Debug.Log("청소완료");
    //         }
    //         else{
    //             Dirty--;
    //             Debug.Log("청소중");
    //         }
    //     }
    // }
}
