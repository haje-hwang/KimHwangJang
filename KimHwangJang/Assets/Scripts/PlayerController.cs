using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Singleton 적용
    private static PlayerController instance;  
    private PlayerController() { }  
    public static PlayerController getPlayerInstance() {  
        if (instance == null) {  
            instance = new PlayerController();  
        }  
        return instance;  
    }  
    private Transform tr;
    private Rigidbody rb;
    private GameObject player;
    private Transform PlaceHere;
    private PlayerController playerController;
    private GameObject nearObj;
    private GameObject controlling_Obj;
    private GameController gameController;
    private ObjectPooling CannonPool;

    [SerializeField]
    private GameObject spawnpoint;
     [SerializeField]
    private InteractionUI interactionUI;


    // private UIController UIController;
    // [SerializeField]
    // Slider timer;
    private float moveForce, Turn_speed, maxSpeed, jumpForce;
    bool hasFood;

    Animator animator;

    // private bool isGround;
    private void Awake()
    {
        tr = this.transform;
        player = this.gameObject;   
        rb = this.transform.GetComponent<Rigidbody>();
        controlling_Obj = this.gameObject;
        PlaceHere = transform.Find("PlaceHere");
        
    }

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.controlling_Obj = this.gameObject;
        // UIController = GameObject.Find("UIController").GetComponent<UIController>();
        animator = GetComponentInChildren<Animator>();

        CannonPool = GameObject.Find("ObjectPooling/CannonBallPool").GetComponent<ObjectPooling>();
    }

    private void Update() {
        Downfall();
    }

    public void RunStop(){
        animator.SetBool("isRun", false);
    }

    public void Jump(){
        if(rb.velocity.y < 0.8f){
            animator.SetTrigger("isJump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
        else{
            animator.SetBool("isFalling", true);
        }
    }
    public void move(Vector3 moveVector){
        try
        {
            //addforce 물리로 player 움직이기
            rb.AddForce(moveVector * moveForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
            animator.SetBool("isRun", moveVector != Vector3.zero);
            //최대속도 제한
            if (Mathf.Abs(rb.velocity.sqrMagnitude) > Mathf.Pow(maxSpeed, 2)){
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            //이동 입력이 없으면 즉시 정지
            if(moveVector.Equals(Vector3.zero)){
                rb.velocity = Vector3.zero;
            }
            else
            {
                //가는 곳 보기
                tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.LookRotation(moveVector), Time.fixedDeltaTime * Turn_speed);
                // tr.LookAt(tr.position + moveVector);
            }  
        }
        catch (System.Exception)
        {
            Debug.Log("Error in PlayerController.move()");
            throw;
        }
        
    }
    public void AddOn(Transform where){
        tr.SetPositionAndRotation(where.position - (where.forward * 2f) + (where.up * 0.4f),
        where.rotation);
    }
    public void interact(){
        interactionUI.HideMsg();
        switch (gameController.controlling_Obj.tag)
        {
            case "Player":
             //조작하고 있는 오브젝트가 플레이어일때, 그리고 상호작용 가능한 오브젝트와 인접할 때
                if( nearObj != null){
                    switch (nearObj.tag)
                    {
                        case "Food":
                            //음식 들기
                            nearObj.GetComponent<FoodScript>().mount_on_head(PlaceHere);
                             //controlling_Obj에 인접 오브젝트를 넣음
                            gameController.controlling_Obj = nearObj;
                            this.controlling_Obj = nearObj;
                            break;
                        case "Fish_Rod":
                            Camera.main.GetComponent<CameraController>().SetisFishing(true);
                            //controlling_Obj에 인접 오브젝트를 넣음
                            gameController.controlling_Obj = nearObj;
                            this.controlling_Obj = nearObj;
                            break;
                        case "Cannon":
                            animator.SetBool("isRun", false);
                            rb.isKinematic = true;
                            //controlling_Obj에 인접 오브젝트를 넣음
                            gameController.controlling_Obj = nearObj;
                            this.controlling_Obj = nearObj;
                            break;
                        case "Cannon_Ball":
                            nearObj.GetComponent<Bullet>().mount_on_head(PlaceHere);
                            //controlling_Obj에 인접 오브젝트를 넣음
                            gameController.controlling_Obj = nearObj;
                            this.controlling_Obj = nearObj;
                            break;
                        case "Cannon_Ball_Pile":
                            GameObject var =  CannonPool.GetObject();
                            var.GetComponent<Bullet>().mount_on_head(PlaceHere);
                            gameController.controlling_Obj = var;
                            this.controlling_Obj = var;
                            break;
                        default:
                             //controlling_Obj에 인접 오브젝트를 넣음
                            gameController.controlling_Obj = nearObj;
                            this.controlling_Obj = nearObj;
                            break;
                    }
                }
                break;
            case "Food":
            //음식 들고 상호작용 버튼을 눌렀다면
                if(nearObj != null){
                    //상호작용 대상이 테이블이라면 
                    if(nearObj.CompareTag("Table"))
                    {
                        Transform where = nearObj.transform.Find("PlaceHere");
                        //테이블에 음식 놓기
                        controlling_Obj.GetComponent<FoodScript>().mount_on_head(where);
                        nearObj.GetComponent<KitchenScript>().Plate(controlling_Obj);
                        //controlling_Obj 테이블로
                        gameController.controlling_Obj = nearObj;
                        this.controlling_Obj = nearObj;
                    }
                }
                else { //음식을 그냥 놓는 경우
                    //음식 놓기
                    controlling_Obj.GetComponent<FoodScript>().demount_on_head();
                    focusToPlayer();
                }
                break;
            case "Cannon_Ball":
                if(nearObj != null){
                    //상호작용 대상이 대포라면 
                    if(nearObj.CompareTag("Cannon"))
                    {
                        controlling_Obj.GetComponent<Rigidbody>().isKinematic = false;
                        // 대포 재장전
                        nearObj.GetComponent<CannonController>().Reload();
                        CannonPool.ReturnObject(controlling_Obj);
                        focusToPlayer();
                    }
                    else {
                        //내려 놓기
                        controlling_Obj.GetComponent<Bullet>().demount_on_head();
                        focusToPlayer();
                    }
                }
                else {
                    //내려 놓기
                    controlling_Obj.GetComponent<Bullet>().demount_on_head();
                    focusToPlayer();
                }
                break;
            case "Table":
                if(controlling_Obj.Equals(nearObj))
                {
                    GameObject CookedFood = controlling_Obj.GetComponent<KitchenScript>().Cooking();
                    if(CookedFood != null){
                        CookedFood.GetComponent<FoodScript>().mount_on_head(PlaceHere);
                    }
                }
                else if(nearObj.CompareTag("Food"))
                {
                    focusToPlayer();
                }
                else
                {
                    focusToPlayer();
                }
                break;
            case "Fish_Rod":
                Camera.main.GetComponent<CameraController>().SetisFishing(false);
                focusToPlayer();
                break;
            case "Cannon":
                rb.isKinematic = false;
                focusToPlayer();
                break;
            default:
                focusToPlayer();
                break;
        }
    }
    private void focusToPlayer(){
        //포커스를 플레이어로
        gameController.controlling_Obj = this.gameObject;
        this.controlling_Obj = this.gameObject;

        // Debug.Log(gameController.controlling_Obj.name);
    }
    public void SetmoveForce(float moveForce){
        this.moveForce = moveForce;
    }
    public void SetTurn_speed(float Turn_speed){
        this.Turn_speed = Turn_speed;
    }
    public void SetmaxSpeed(float maxSpeed){
        this.maxSpeed = maxSpeed;
    }
     public void SetjumpForce(float jumpForce){
        this.jumpForce = jumpForce;
    }
    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isFalling", false);
        switch (other.tag)
        {
            case "Food":
                //Food 들고 있는 상황에서 오류 생길까봐
                if(controlling_Obj.CompareTag("Food")) {
                   break;
                }
                else{
                    nearObj = other.gameObject;
                    interactionUI.ShowMsg("Press [E] to interact with " + other.tag.ToString());
                    // Debug.Log(other.tag.ToString() + " 상호작용 준비");
                    break;
                }
            case "Steering_Wheel":
            case "Cannon":
            case "Cannon_Ball":
            case "Table":
            case "Fish_Rod":
            case "Cannon_Ball_Pile":
                nearObj = other.gameObject;
                interactionUI.ShowMsg("Press [E] to interact with " + other.tag.ToString());
                // Debug.Log(other.tag.ToString() + " 상호작용 준비");
                break;
            default:
                // Debug.Log(other.name + "가 범위에 있음");
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(nearObj != null)
        {
            if (nearObj.Equals(other.gameObject))
            {
                nearObj = null;
                interactionUI.HideMsg();
                // Debug.Log(other.tag.ToString() + " 상호작용 범위 이탈"); 
            }
        }
        
        // switch (other.tag)
        // {
        //     case "Steering_Wheel":
        //     case "Cannon":
        //     case "Food":
        //     case "Table": 
        //         if(nearObj != null)
        //         {
        //             if (nearObj.Equals(other.gameObject))
        //             {
        //                 nearObj = null;
        //                 Debug.Log(other.tag.ToString() + " 상호작용 범위 이탈"); 
        //             }
        //         }
        //         break;
        //     default:
        //         break;
        // }
    }
    private void OnCollisionEnter(Collision collision)
    {
        animator.SetBool("isFalling", false);
    }
    // private void OnCollisionStay(Collision other)
    // {
    //     if(other.transform.CompareTag("Raft")){
    //         isGround = true;
    //     }
    // }
    // private void OnCollisionExit(Collision other)
    // {
    //     if(other.transform.CompareTag("Raft")){
    //         isGround = false;
    //     }
    // }

    void Downfall(){
        if(transform.position.y <= -1){
            transform.position = spawnpoint.transform.position;
        } 
    }
} 