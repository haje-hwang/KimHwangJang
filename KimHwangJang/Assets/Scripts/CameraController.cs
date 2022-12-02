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
    private Vector3 fishing_offset;
    private bool isFishing;
    private void Awake()
    {
        tr = this.gameObject.transform;
        fishing_offset = new Vector3(5, 4, -5);
        isFishing = false;
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
        if(isFishing)
        {
            tr.SetPositionAndRotation(Player.position + fishing_offset, 
            Quaternion.Euler(new Vector3(30f, -45f, 0)));
        }
        else
        {
            // transform.position = Raft.position + offset;
            tr.SetPositionAndRotation(Player.position + offset, 
            Quaternion.Euler(new Vector3(30f, 0, 0)));
        }
    }

    public void SetisFishing(bool isFishing)
    {
        this.isFishing = isFishing;
    }
}
