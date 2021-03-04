using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0; // 분당 비트 나올 수
    double currentTime = 0d; // 현재 시간

    [SerializeField] Transform tfNoteAppear = null; // note가 나타날 위치
    [SerializeField] GameObject goNote = null; // note를 담을 변수

    TimingManger theTimingManger;


    private void Start()
    {
        theTimingManger = GetComponent<TimingManger>();
    }

    void Update()
    {
        currentTime += Time.deltaTime; // 1초에 1씩 증가

        if(currentTime >= 60d / bpm) // 60/120 = 1beat 당 0.5초
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform); // 캠퍼스 내에 넣어야 note가 보이므로 SetParent함

            theTimingManger.boxNoteList.Add(t_note); // List에 t_note 넣어줌
            currentTime -= 60d / bpm; // currentTime = 0 안됨(0.01005~정도의 시간만큼 오차가 손실됨)
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Note"))
        {
            theTimingManger.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
