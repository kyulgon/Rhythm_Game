using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400; // 노트 속도
    Image noteImage; // 노트 이미지 변수

    private void Start()
    {
        noteImage = GetComponent<Image>();
    }

    private void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime; // 오른쪽으로 이동
    }


    public void HideNote() // 첫 노트를 비활성화하여 노래를 틀어줌
    {
        noteImage.enabled = false;
    }
}
