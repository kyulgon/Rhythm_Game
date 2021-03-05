using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    AudioSource myAudio;
    bool musicStart = false; // 노래가 틀어져있는지 확인할 변수

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // music 트리거 설정
    {
        if(!musicStart) // 노래가 안 틀어져있으면
        {
            if (collision.CompareTag("Note")) // 태그가 NOte이면
            {
                myAudio.Play(); // 노래 시작
                musicStart = true;
            }
        }
       
    }
}
