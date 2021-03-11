using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : MonoBehaviour
{
    AudioSource theAudio;
    NoteManager theNote;

    
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        theNote = FindObjectOfType<NoteManager>();
    }

    private void OnTriggerEnter(Collider other) // 골에 들어갔을 경우
    {
        theAudio.Play(); // 소리 재생
        PlayerController.s_canPresskey = false; // 키 안 눌러지
        theNote.RemoveNote(); // 노트제거
    }

}
