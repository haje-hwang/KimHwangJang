using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    private float RaftSpeed;

    int SpeedLevel = 1;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * RaftSpeed * Time.deltaTime, Space.Self);
        SpeedControl();
    }

    public void setRaftSpeed(float RaftSpeed){
        this.RaftSpeed = RaftSpeed;
    }

    void SpeedControl(){
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
                break;
            case 2:
                RaftSpeed = 3.0f;             
                break;
            case 3:
                RaftSpeed = 6.0f;               
                break;
        }
    }
}
