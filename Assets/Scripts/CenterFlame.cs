using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    AudioSource myAudio;
    bool musicStart = false; // 처음 노트에만 노래가 시작되게 하려고

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!musicStart)
        {
            if(collision.CompareTag("Note"))
            {
                myAudio.Play();
                musicStart = true;
            }
        }
    }
}
