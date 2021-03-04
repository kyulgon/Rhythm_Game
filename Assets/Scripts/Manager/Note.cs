using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;

    Image noteImage;

    private void Start()
    {
        noteImage = GetComponent<Image>();
    }

    public void HideNote() // 처음에 음악소리 재생해주기 위함
    {
        noteImage.enabled = false;
    }

    private void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }

}
