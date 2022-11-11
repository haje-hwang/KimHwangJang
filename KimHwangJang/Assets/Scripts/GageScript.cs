using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Silder class 사용하기 위해 추가합니다.

public class GageScript : MonoBehaviour
{
    Slider slTimer;
    float fSliderBarTime;
    void Start()
    {
        slTimer = GetComponent<Slider>();
        slTimer.value = 100.0f;
    }

    void Update()
    {
        if (slTimer.value > 0.0f)
        {
            // 시간이 변경한 만큼 slider Value 변경을 합니다.
            slTimer.value -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Time is Zero.");
        }
    }

    void Addtime(float val)
    {
        slTimer.value += val;
    }

    


}
