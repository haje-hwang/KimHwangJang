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

    public AudioClip audioCannon;
    public AudioClip Engine1;
    public AudioClip Engine2;
    public AudioClip Backword;

    AudioSource audioSource;



    int SpeedLevel = 1;
    // Update is called once per frame
    
    private void Start() {
        player = GameObject.FindWithTag("Player");

        audioSource = GetComponent<AudioSource>();

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
            if(Input.GetKeyDown(KeyCode.W) && SpeedLevel < 3){
                Debug.Log("Speed Up");
                SpeedLevel += 1;
                Debug.Log("Spd =" + SpeedLevel);
            }
            else if(Input.GetKeyDown(KeyCode.S) && SpeedLevel > 0){
                Debug.Log("Speed Down");
                SpeedLevel -= 1;
                Debug.Log("Spd =" + SpeedLevel);
            }
            switch(SpeedLevel){
                case 0:
                    RaftSpeed = -3.0f;
                    audioSource.clip = Backword;
                    break;
                case 1:
                    RaftSpeed = 0f;
                    ps.Stop();//파티클 스탑
                    break;
                case 2:
                    RaftSpeed = 3.0f;
                    audioSource.clip = Engine1;
                    ps.Play();//파티클 플레이
                    break;
                case 3:
                    RaftSpeed = 6.0f;
                    audioSource.clip = Engine2;
                    break;
            }
             /*
                SpeedLevel이 1로 초기화되는데 SpeedLevel이 1일때는
                audioSource.clip을 설정하지 않아서 실행할 audioSource가 없어서 게임이 멈춤
                그래서 audioSource가 null일 때 실행이 안되게 if문으로 감쌌다.

                라고 수정하니까 사실 Raft에 있는 AudioSource를 참조하는데 AudioSource가 없다고 오류가 난다.
                AudioSource는 MainCamera에 있으니까 MainCamera의 AudioSource를 참조하게 바꾸었다.
                45번째 줄
                this.audioSource = GetComponent<AudioSource>();
                ====>>
                audioSource = Camera.main.GetComponent<AudioSource>();

                라고 수정하니까 원래 사운드 클립이 그런건지는 몰?루겠는데 자꾸 소리가 끊기는?
                재생중인데 다시 처음부터 재생하는 느낌이 든다.
                그래서 이미 재생중일때는 재생하지 않도록 if(!audioSource.isPlaying)로 감쌌다
            */
            if(audioSource.clip != null){
                if(!audioSource.isPlaying){
                    audioSource.Play();
                }     
            }
        }
    }

    public void TurnControl(float turn){
        if(onPlayer){
            transform.Rotate(new Vector3(0, turn, 0), Space.World);
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
