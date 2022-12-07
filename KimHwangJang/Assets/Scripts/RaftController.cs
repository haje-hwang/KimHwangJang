using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RaftController : MonoBehaviour
{
    private float RaftSpeed;

    bool isLandable;
    bool onPlayer;

    Vector3 spawnPoint;
    Vector3 landingPoint;

    Quaternion landingRotate;

    [SerializeField]
    GameObject player;

    GameObject spawn;
    GameObject landing;

    [SerializeField]
    ParticleSystem ps;

    [SerializeField]
    private GameObject spawnpoint;


    int SpeedLevel = 1;
    // Update is called once per frame
    
    private void Start() {
        player = GameObject.FindWithTag("Player");

        isLandable = false;
        onPlayer = true;
    }

    void Update()
    {

        Move();
        Land();
    }

    public void setRaftSpeed(float RaftSpeed){
        this.RaftSpeed = RaftSpeed;
    }

    public void Move(){
        transform.Translate(Vector3.forward * RaftSpeed * Time.deltaTime, Space.Self);
    }

    //배 속도 조절. 윗키 누르면 빨라지고 아래키 누르면 느려지다가 뒤로감.
    public void SpeedControl(){
        if(onPlayer){
            if(Input.GetKeyDown(KeyCode.UpArrow) && SpeedLevel < 3){
                Debug.Log("Speed Up");
                SpeedLevel += 1;
                Debug.Log("Spd =" + SpeedLevel);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow) && SpeedLevel > 0){
                Debug.Log("Speed Down");
                SpeedLevel -= 1;
                Debug.Log("Spd =" + SpeedLevel);
            }
            switch(SpeedLevel){
                case 0:
                    RaftSpeed = -3.0f;
                    break;
                case 1:
                    RaftSpeed = 0f;
                    ps.Stop();//파티클 스탑
                    break;
                case 2:
                    RaftSpeed = 3.0f;
                    ps.Play();//파티클 플레이
                    break;
                case 3:
                    RaftSpeed = 6.0f;               
                    break;
            }
        }
    }

    //섬 상륙을 위한 충돌 체크. 선착장 태그 Port 설정 및 스폰될 위치 오브젝트 이름 "Spawnpoint" 필요.
    //플레이어 오브젝트 넣어둘 필요도 있음.
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Port") && onPlayer){
            isLandable = true;
            spawn = other.gameObject;
            spawnPoint = spawn.transform.Find("Spawnpoint").transform.position;
            landing = other.gameObject;
            landingPoint = landing.transform.Find("Landingpoint").transform.position;
            landingRotate = landing.transform.Find("Landingpoint").transform.rotation;
        }
        if (other.gameObject.CompareTag("Wall")) {
            transform.position = spawnpoint.transform.position;
        }
    }

    private void OnTriggerStay(Collider other) {
        isLandable = false;
    }

    //플레이어 상륙.
    void Land(){
        if(isLandable && onPlayer){
            if(Input.GetKeyDown(KeyCode.R)){
                SpeedLevel = 1;
                RaftSpeed = 0;
                transform.rotation = landingRotate;                
                transform.position = landingPoint;
                player.transform.position = spawnPoint;
                player.transform.Translate(Vector3.zero);
                onPlayer = false;
                Debug.Log("랜딩");
            }
        }
    }

}
