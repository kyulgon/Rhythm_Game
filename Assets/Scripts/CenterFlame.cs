using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    bool musicStart = false; // 노래가 틀어져있는지 확인할 변수


    private void OnTriggerEnter2D(Collider2D collision) // music 트리거 설정
    {
        if(!musicStart) // 노래가 안 틀어져있으면
        {
            if (collision.CompareTag("Note")) // 태그가 NOte이면
            {
                AudioManager.instance.PlayBGM("BGM0");
                musicStart = true;
            }
        }
       
    }
}
