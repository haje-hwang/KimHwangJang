using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Silder class ����ϱ� ���� �߰��մϴ�.

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
            // �ð��� ������ ��ŭ slider Value ������ �մϴ�.
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
