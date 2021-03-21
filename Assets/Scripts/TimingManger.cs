using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManger : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>(); // 리스트로 노트저장

    int[] judgementRecord = new int[5]; // 판정기록 변수

    [SerializeField] Transform Center = null; // 센터 위치
    [SerializeField] RectTransform[] timingRect = null; // 타이밍 구간 배열
    
    Vector2[] timingBoxs = null;

    EffectManager theEffect;
    ScoreManager theScoreManager;
    ComboManger theComboManager;
    StageManager theStageManager;
    PlayerController thePlayer;
    StatusManager theStatusManger;
    AudioManager theAudioManger;

    private void Start()
    {
        theAudioManger = AudioManager.instance;
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theComboManager = FindObjectOfType<ComboManger>();
        theStageManager = FindObjectOfType<StageManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        theStatusManger = FindObjectOfType<StatusManager>();

        // 타이밍 박스 설정
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2, // 최소크기
                              Center.localPosition.x + timingRect[i].rect.width / 2); // 최대 크기
        }
    }

    public bool CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++) // 리시트에 있는 모든 노트 확인
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

                    if(CheckCanNextPlate()) // 다음 발판 확인
                    {
                        theScoreManager.IncreasecScore(x); // 점수증가
                        theStageManager.ShowNextPlate(); // 발판 생성
                        theEffect.JudgementEffect(x); // 이펙트 연출 

                        judgementRecord[x]++; // 판정기록
                        theStatusManger.CheckShield(); // 쉴드 체크
                    }
                    else
                    {
                        theEffect.JudgementEffect(5); // normal 이팩트
                    }

                   AudioManager.instance.PlaySFX("Clap");
                    
                    return true;
                }
            }
        }

        theComboManager.ResetCombo(); // 콤보 초기화
        theEffect.JudgementEffect(timingBoxs.Length); // timingBoxs의 배열 개수는 4이므로 length를 이용
        MissRecord();
        return false;
    }

    bool CheckCanNextPlate() // 다음 발판이 가능한지
    {
        if (Physics.Raycast(thePlayer.destPos, Vector3.down, out RaycastHit t_hitInfo, 1.1f)) // 레이케이트를 쏴서
        {
            if(t_hitInfo.transform.CompareTag("BasicPlate")) // 태그가 BasicPlate이면
            {
                BasicPlate t_plate = t_hitInfo.transform.GetComponent<BasicPlate>(); // t_plate에 t_hitInfo를 넣어줌
                if(t_plate.flage)// t_plateflage가 true이면
                {
                    t_plate.flage = false; // false로 바꾸고 리턴
                    return true;
                }
            }
        }

        return false;
    }

    public int[] GetJudgementRecord()
    {
        return judgementRecord;
    }

    public void MissRecord()
    {
        judgementRecord[4]++; // 판정기록
        theStatusManger.ResetShieldCombo(); // 쉴드콤보 리셋
    }

    public void Initialized()
    {
        judgementRecord[0] = 0;
        judgementRecord[1] = 0;
        judgementRecord[2] = 0;
        judgementRecord[3] = 0;
        judgementRecord[4] = 0;
    }
}
