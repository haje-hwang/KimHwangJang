using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform tr;
    private Transform Raft;
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private Vector3 offset;
    private void Awake()
    {
        tr = this.gameObject.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            // Raft = GameObject.FindGameObjectWithTag("Raft").GetComponent<Transform>();
            Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
        catch (System.Exception)
        {
            Debug.Log("Error in Start, CameraController");
            throw;
        }
        // offset = transform.position - Raft.position;
        offset = tr.position - Player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // transform.position = Raft.position + offset;
        tr.position = Player.position + offset;
    }
}
