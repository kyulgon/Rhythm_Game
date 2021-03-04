using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManger : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>(); // 판정범위에 있는지 모든 노트를 비교해야함

    [SerializeField] Transform Center = null; // 센터위치
    [SerializeField] RectTransform[] timingRect = null; //판정범위

    Vector2[] timingBoxs = null; // RectTransform의 배열을 넣어줌

    void Start()
    {
        // 타이밍 박스 설정
        timingBoxs = new Vector2[timingRect.Length]; // 배열의 수 넣어줌

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,   // 최소값 = 중심 - (이미지의 너비 /2)
                              Center.localPosition.x + timingRect[i].rect.width / 2);  // 최대값 = 중심 + (이미지의 너비 /2)
        }
    }

    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++) // 모든 노트 확인
        {
            float t_notPosX = boxNoteList[i].transform.localPosition.x; // 모든 노트의 x좌표

            for (int x = 0; x < timingBoxs.Length; x++) // 판정해주는 수만큼(4개)
            {
                if(timingBoxs[x].x <= t_notPosX && t_notPosX <= timingBoxs[x].y) // 노트가 판정범위 안에 들어왔는지 확인
                {
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i); // 리스트에서 지우기
                    Debug.Log("Hit" + x);
                    return;
                }
            }
        }

        Debug.Log("Miss");
    }
}
