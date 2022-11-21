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
    private float Horizontal;
    private float Vertical;
    private Vector3 moveVector;
    private bool Interaction_KeyPressed;
    private GameObject player;
    private PlayerController playerController;
    private GameObject nearObj;
    [SerializeField]
    private GameObject controlling_Obj;
    private CannonController cannonController;
    private GameController gameController;
    private UIController UIController;
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
        player = this.gameObject;
        rb = this.transform.GetComponent<Rigidbody>();
        controlling_Obj = this.gameObject;
        Raft_RotateSpeed = 10f;
        try
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            gameController.controlling_Obj = this.gameObject;
            gameController.RaftSpeed = this.RaftSpeed;
            UIController = GameObject.Find("UIController").GetComponent<UIController>();
        }
        catch (System.Exception)
        {
            Debug.Log("Error in PlayerController.Awake()");
            throw;
        }
        Raft_tr = GameObject.Find("GameObjects/Raft").GetComponent<Transform>();
        cannonController = Raft_tr.Find("Cannon").transform.GetComponent<CannonController>();
    }
    void Update(){
        GetInput();
        interact();
        if(controlling_Obj.tag == "Food"){
            //음식 들기
            controlling_Obj.transform.position = transform.position + Vector3.up * 6f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(controlling_Obj == player || controlling_Obj.tag == "Food")
        {
            move(moveVector);
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
    public void move(Vector3 moveVector){
        //빠르면 속도 감쇠, 움직임 입력이 없으면 속도 감쇠
        if(Mathf.Abs(rb.velocity.sqrMagnitude) > 400f || moveVector.sqrMagnitude < 0.01){
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
            //음식 들고 상호작용 버튼을 눌렀다면
            else if (gameController.controlling_Obj.tag == "Food"){
                controlling_Obj.transform.position = transform.position + Vector3.up * 6f + transform.forward * 3f;
                if(nearObj != null){
                    //상호작용 대상이 테이블이라면 
                    if(nearObj.tag == "Table")
                    {
                        //테이블에 음식 놓기
                    }
                }
                else{ //음식을 그냥 놓는 경우
                    //포커스를 플레이어로
                    gameController.controlling_Obj = this.gameObject;
                    this.controlling_Obj = this.gameObject;
                    Debug.Log(gameController.controlling_Obj.name);
                }
            }
            else{
                //포커스를 플레이어로
                gameController.controlling_Obj = this.gameObject;
                this.controlling_Obj = this.gameObject;
                Debug.Log(gameController.controlling_Obj.name);
            }
            
        }
    }
     void GetInput(){
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        moveVector = new Vector3(Horizontal, 0, Vertical).normalized;
        Interaction_KeyPressed = Input.GetButtonDown("Interaction");
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Steering_Wheel":
            case "Cannon":
            case "Food":
                //Food 들고 있는 상황에서 오류 생길까봐
                if(controlling_Obj.tag == "Food") {
                   break;
                }
                else{
                    nearObj = other.gameObject;
                    Debug.Log(other.tag.ToString() + " 상호작용 준비");
                    break;
                }
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
