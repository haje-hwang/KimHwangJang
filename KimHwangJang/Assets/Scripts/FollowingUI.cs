using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingUI : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float floating_hight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position + new Vector3(0, floating_hight, 0));
    }
}
