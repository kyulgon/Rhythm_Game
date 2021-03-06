using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManger : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>(); // 리스트로 노트저장

    [SerializeField] Transform Center = null; // 센터 위치
    [SerializeField] RectTransform[] timingRect = null; // 타이밍 구간 배열
    
    Vector2[] timingBoxs = null;

    EffectManager theEffect;

    private void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();

        // 타이밍 박스 설정
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2, // 최소크기
                              Center.localPosition.x + timingRect[i].rect.width / 2); // 최대 크기
        }
    }

    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++) // 리시트에 있는 모든 노트 호가인
        {
            float t_tnoePosX = boxNoteList[i].transform.localPosition.x; // 모든 노트의 x좌표를 t_tonePosX에 저장

            for (int x = 0; x < timingBoxs.Length; x++) // 판정해주는 수만큼(Perfect, cool, good, bad)
            {
                if(timingBoxs[x].x <= t_tnoePosX && t_tnoePosX <= timingBoxs[x].y) // 노트가 판정범위 안에 들어왔는지 확인
                {
                    boxNoteList[i].GetComponent<Note>().HideNote(); // HideNote함수 실행
                    boxNoteList.RemoveAt(i); // 리스트에서 제거

                    if (x < timingBoxs.Length -1) // Bad를 빼고 애니메이션 실행
                    {
                        theEffect.NoteHitEffect();
                    }

                    theEffect.JudgementEffect(x); // 이펙트 연출
                    return;
                }
            }
        }

        theEffect.JudgementEffect(timingBoxs.Length); // timingBoxs의 배열 개수는 4이므로 length를 이용
    }
}
