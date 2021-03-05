using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TimingManger theTimingManger;

    private void Start()
    {
        theTimingManger = FindObjectOfType<TimingManger>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) // 스페이를 누르면 CheckTiming함수 실행
        {
            theTimingManger.CheckTiming();
        }
    }
}
