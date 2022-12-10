using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    /*code reference
    https://bloodstrawberry.tistory.com/687
    */
    private Camera MainCamera;
    public Transform lookAtMe;                 //카메라 포커스 오브젝트
    public float rotateSpeed = 300.0f;          //우클릭으로 카메라 회전하는 속도
    public float scrollSpeed = 500.0f;          //카메라 확대축소 속도
    private float xRotateMove, yRotateMove;
    public float min_CameraFar = 2f;        //카메라가 오브젝트로부터 얼마나 멀어질 수 있는지
    public float max_CameraFar = 10f;        //카메라가 오브젝트로부터 얼마나 멀어질 수 있는지
    Vector3 move_offset;
    Vector3 offset;
    Vector2 clickPoint;
    float dragSpeed = 30.0f;
    private void Awake()
    {
        MainCamera = Camera.main;
    }
    private void Start()
    {
        move_offset = lookAtMe.position;
        offset = transform.position;
    }
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0)) clickPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 pos = lookAtMe.position;
        // 플레이어 움직임 따라가기
        if(!move_offset.Equals(pos)){
            transform.position +=  pos - move_offset;
        }
        move_offset = pos;
        
        //우클릭 드래그, 카메라 회전
        if (Input.GetMouseButton(1))
        {
            xRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;

            transform.RotateAround(pos, transform.right, -yRotateMove);
            transform.RotateAround(pos, Vector3.up, xRotateMove);

            transform.LookAt(pos);  
            move_offset = pos;
            offset = transform.position;
        }
        //마우스 스크롤로 화면 확대, 축소
        else
        {
            transform.LookAt(lookAtMe.position);
            
            Vector3 cameraDirection = transform.localRotation * Vector3.back;

            float scroollWheel = Input.GetAxis("Mouse ScrollWheel");

            //스크롤 확대축소(카메라를 이동해서)
            transform.position -= cameraDirection * Time.smoothDeltaTime * scroollWheel * scrollSpeed;

            //카메라가 오브젝트에 가까워져서 반대로 뚫고 나가기 방지
            Vector3 diff = pos - transform.position;
            float gap = diff.magnitude;
            // Debug.Log(gap);
            if(gap <= min_CameraFar)
            {
                transform.position = offset;
            }   
            else if(gap >= max_CameraFar)      //카메라 너무 멀리가기 방지
            {
                transform.position = offset;
            }
            else{
                offset = transform.position;
            } 
        }
    }
}
