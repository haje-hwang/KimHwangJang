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
            transform.LookAt(cam.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}