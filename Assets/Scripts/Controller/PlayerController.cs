using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TimingManger theTimingManaer;

    private void Start()
    {
        theTimingManaer = FindObjectOfType<TimingManger>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            theTimingManaer.CheckTiming();
        }
    }
}
