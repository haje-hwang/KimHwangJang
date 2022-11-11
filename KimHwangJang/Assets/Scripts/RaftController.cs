using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    private GameController gameController;
    [SerializeField]
    private float RaftSpeed;
    private void Awake()
    {
        try
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            gameController.RaftSpeed = this.RaftSpeed;
        }
        catch (System.Exception)
        {
            Debug.Log("Cannot Find gameController");
            throw;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * RaftSpeed * Time.deltaTime, Space.Self);
    }
}
