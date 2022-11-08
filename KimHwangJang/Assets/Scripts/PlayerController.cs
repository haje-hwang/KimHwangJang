using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform tr;
    private Rigidbody rb;
    public float moveForce;
    //GetInput()
    private float Horizontal;
    private float Vertical;
    private Vector3 moveVector;
    private bool Interaction_KeyPressed;
    private GameObject nearObj;
    private void Awake()
    {
        tr = this.transform;
        rb = this.transform.GetComponent<Rigidbody>();
    }
    void Update(){
        GetInput();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }
    void GetInput(){
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        moveVector = new Vector3(Horizontal, 0, Vertical).normalized;
        Interaction_KeyPressed = Input.GetButton("Interaction");
    }
    void move(){
        //빠르면 속도 감쇠, 움직임 입력이 없으면 속도 감쇠
        if(Mathf.Abs(rb.velocity.sqrMagnitude) > 400f || Horizontal + Vertical < 0.01){
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z) * 0.9f;
        }
        //addforce 물리로 움직이기
        rb.AddForce(moveVector * moveForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
        //가는 곳 보기
        tr.LookAt(tr.position + moveVector);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Paddle"){
            Debug.Log("노와 상호작용 가능");
        }
    }
}
