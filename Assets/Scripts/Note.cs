using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400; // 노트 속도
    Image noteImage; // 노트 이미지 변수

    private void OnEnable() // 활성화 될때마다
    {
        if(noteImage == null) // 이미지가 없으면 
        {
            noteImage = GetComponent<Image>(); // 이미지 컴포넌트를 가져오고
        }

        // 노트이미지를 활성화 (전에 큐에 반납할때 false로 반납해서 다시 활성화되면 true해줘야함)
        noteImage.enabled = true; 
        
    }

    private void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime; // 오른쪽으로 이동
    }


    public void HideNote() // 첫 노트를 비활성화하여 노래를 틀어줌
    {
        noteImage.enabled = false;
    }

    public bool GetNoteFlag() // 노트 이미지 보여진 상태이면
    {
        return noteImage.enabled;
    }
}
