using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCameraCanvas : MonoBehaviour
{
    //https://jacobjea.tistory.com/7
    GameObject cam;
    private void Start()
    {
        cam = Camera.main.gameObject;
    }

    private void LateUpdate()
    {
        if(cam != null)
        {
            Debug.Log("작동중이에요!");
            transform.LookAt(cam.transform);
            transform.Rotate(0, 90, 0);
        }
    }
}