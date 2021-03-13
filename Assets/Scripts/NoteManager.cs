using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0; // 분당 비트 나올 수
    double currentTime = 0d; // 현재 시간

    bool noteActive = true;

    [SerializeField] Transform tfNoteApper = null; // 노트가 나타날 위치
    
    TimingManger theTimingManger;
    EffectManager theEffectManager;
    ComboManger theComboManger;

    private void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theComboManger = FindObjectOfType<ComboManger>();
        theTimingManger = GetComponent<TimingManger>();
    }

    private void Update()
    {
        if(noteActive)
        {
            currentTime += Time.deltaTime; // 1초에 1씩 증가

            if (currentTime >= 60d / bpm) // 60/120이므로 0.5초에 한번씩
            {
                GameObject t_note = ObjectPool.instance.noteQueue.Dequeue(); // 큐값 가져옴
                t_note.transform.position = tfNoteApper.position; // 적절한 위치값을 넣어줌
                t_note.SetActive(true); // 활성화

                theTimingManger.boxNoteList.Add(t_note); // 리시트에 노트 저장
                currentTime = 60 / bpm; // bpm다시 계산
            }
        }        
    }

    private void OnTriggerExit2D(Collider2D collision) // 지정 구간을 넘어가면 노트 파괴
    {
        if(collision.CompareTag("Note"))
        {
            if(collision.GetComponent<Note>().GetNoteFlag()) // 노트가 보여진 상태이면
            {
                theTimingManger.MissRecord();
                theEffectManager.JudgementEffect(4); // Miss 띄움
                theComboManger.ResetCombo(); // 콤보 초기화
            }

            theTimingManger.boxNoteList.Remove(collision.gameObject);


            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject); // 큐 반납
            collision.gameObject.SetActive(false);
            
        }
    }

    public void RemoveNote() // 노트를 지워주는 메서드
    {
        noteActive = false;

        for (int i = 0; i < theTimingManger.boxNoteList.Count; i++)
        {
            theTimingManger.boxNoteList[i].SetActive(false);
            ObjectPool.instance.noteQueue.Enqueue(theTimingManger.boxNoteList[i]);
        }
    }
}
