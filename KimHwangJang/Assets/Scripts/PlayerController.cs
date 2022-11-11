using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject controlling_Obj;
    private CannonController cannonController;
    private GameController gameController;
    private float RaftSpeed;
    private float Raft_RotateSpeed;
    private Transform Raft_tr;

    [SerializeField]
    Slider timer;

    bool hasFood;

    private bool isGround;
    private void Awake()
    {
        tr = this.transform;
        rb = this.transform.GetComponent<Rigidbody>();
        controlling_Obj = this.gameObject;
        Raft_RotateSpeed = 10f;
        try
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            gameController.controlling_Obj = this.gameObject;
            gameController.RaftSpeed = this.RaftSpeed;
        }
        catch (System.Exception)
        {
            Debug.Log("Cannot Find gameController");
            throw;
        }
        Raft_tr = GameObject.Find("GameObjects").transform.Find("Raft").GetComponent<Transform>();
        cannonController = Raft_tr.Find("Cannon").transform.GetComponent<CannonController>();
    }
    void Update(){
        GetInput();
        interact();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(controlling_Obj == this.gameObject || controlling_Obj.tag == "Food")
        {
            move();
        }
        else if(controlling_Obj.tag == "Steering_Wheel"){
            //뗏목 회전
            Raft_tr.Rotate(0, Horizontal * Time.deltaTime * Raft_RotateSpeed, 0);
        }
        else if(controlling_Obj.tag == "Cannon"){
            cannonController.Aim();
            if(Input.GetMouseButtonDown(0)){
                cannonController.Reload();
            }
            if(Input.GetButtonDown("Jump")){
                cannonController.Shoot();
            }
            if(Input.GetMouseButtonDown(1)){
                cannonController.Cleanup();
            }
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
            //조작하고 있는 오브젝트가 플레이어일때, 그리고 상호작용 가능한 오브젝트와 인접할 때
            if(gameController.controlling_Obj == this.gameObject && nearObj != null){
                //controlling_Obj에 인접 오브젝트를 넣음
                gameController.controlling_Obj = nearObj;
                this.controlling_Obj = nearObj;
                Debug.Log(gameController.controlling_Obj.name);
            }
            //음식 들고 테이블과 상호작용 버튼을 눌렀다면
            else if (gameController.controlling_Obj.tag == "Food" && nearObj.tag == "Table")
            {
                //
            }
            else{
                //포커스를 플레이어로
                gameController.controlling_Obj = this.gameObject;
                this.controlling_Obj = this.gameObject;
                Debug.Log(gameController.controlling_Obj.name);
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Paddle":
            case "Steering_Wheel":
            case "Cannon":
            case "Food":
            case "Table":
                nearObj = other.gameObject;
                Debug.Log(other.tag.ToString() + " 상호작용 준비");
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Paddle":
            case "Steering_Wheel":
            case "Cannon":
            case "Food":
            case "Table":
                if (nearObj == other.gameObject)
                {
                    nearObj = null;
                    Debug.Log(other.tag.ToString() + " 상호작용 범위 이탈");
                };
                break;
            default:
                break;
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
