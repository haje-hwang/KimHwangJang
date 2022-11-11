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
    private GameController gameController;

    private bool isGround;
    private void Awake()
    {
        tr = this.transform;
        rb = this.transform.GetComponent<Rigidbody>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.controlling_Obj = this.gameObject;
    }
    void Update(){
        GetInput();
        interact();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameController.controlling_Obj == this.gameObject){
            move();
        }

    }
    void GetInput(){
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        moveVector = new Vector3(Horizontal, 0, Vertical).normalized;
        Interaction_KeyPressed = Input.GetButtonDown("Interaction");
    }
    void move(){
        //빠르면 속도 감쇠, 움직임 입력이 없으면 속도 감쇠
        if(Mathf.Abs(rb.velocity.sqrMagnitude) > 400f || Horizontal + Vertical < 0.01){
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z) * 0.9f;
        }
        //addforce 물리로 player 움직이기
        rb.AddForce(moveVector * moveForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
        //가는 곳 보기
        tr.LookAt(tr.position + moveVector);
    }
    void interact(){
        if(Interaction_KeyPressed){
            Debug.Log("상호작용키 눌림");
            if(gameController.controlling_Obj == this.gameObject && nearObj != null){
                gameController.controlling_Obj = nearObj;
                Debug.Log(gameController.controlling_Obj.name);
            }
            else{
                gameController.controlling_Obj = this.gameObject;
                Debug.Log(gameController.controlling_Obj.name);
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Paddle"){
            nearObj = other.gameObject;
            Debug.Log("노와 상호작용 가능");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Paddle"){
            if(nearObj == other.gameObject){
                nearObj = null;
                Debug.Log("노 상호작용 범위 이탈");
            }
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if(other.transform.tag == "Raft"){
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if(other.transform.tag == "Raft"){
            isGround = false;
        }
    }
}
