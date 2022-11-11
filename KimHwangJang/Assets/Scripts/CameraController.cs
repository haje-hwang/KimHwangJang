using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform Raft;
    private Vector3 offset;
    private void Awake()
    {
        try
        {
            Raft = GameObject.Find("GameObjects").transform.Find("Raft").GetComponent<Transform>();
        }
        catch (System.Exception)
        {
            Debug.Log("Error in Awake, CameraController");
            throw;
        }
        offset = transform.position - Raft.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Raft.transform.position + offset;
    }
}
