using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneCamera : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    private float ybase;
    private float speed = 2f;
    // Update is called once per frame
    private void Start()
    {
        ybase = transform.rotation.eulerAngles.y;
    }
    void Update()
    {
     Debug.Log(transform.rotation.eulerAngles.y); 
        if(transform.rotation.eulerAngles.y < ybase - 15f){
            speed = 2f;
        }
        else if(transform.rotation.eulerAngles.y > ybase + 15f){
            speed = -2f;
        }
        transform.RotateAround(target.position, Vector3.up, Time.smoothDeltaTime * speed);
    }
}
