using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    private GameController gameController;
    private float RaftSpeed;
    private void Awake()
    {

    }
    public void setRaftSpeed(float RaftSpeed){
        this.RaftSpeed = RaftSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * RaftSpeed * Time.deltaTime, Space.Self);
    }
}
