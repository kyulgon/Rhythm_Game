using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0; // 분당 비트 나올 수
    double currentTime = 0d; // 현재 시간

    [SerializeField] Transform tfNoteApper = null; // 노트가 나타날 위치
    [SerializeField] GameObject goNote = null; // 노트를 담을 오브젝트

    TimingManger theTimingManger; 

    private void Start()
    {
        theTimingManger = GetComponent<TimingManger>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime; // 1초에 1씩 증가

        if(currentTime >= 60d / bpm) // 60/120이므로 0.5초에 한번씩
        {
            GameObject t_note = Instantiate(goNote, tfNoteApper.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform); // SetPart자리에 노트 만듦
            theTimingManger.boxNoteList.Add(t_note); // 리시트에 노트 저장
            currentTime = 60 / bpm; // bpm다시 계산
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // 지정 구간을 넘어가면 노트 파괴
    {
        if(collision.CompareTag("Note"))
        {
            theTimingManger.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
