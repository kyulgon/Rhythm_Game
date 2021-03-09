using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text txtScore = null; // 텍스트 점수
    [SerializeField] int increaseScore = 10; // 증가 점수
    [SerializeField] float[] weight = null; // 가중치
    [SerializeField] int comboBonusScore = 10; // 콤보 보너스 점수

    int currentScore = 0;

    Animator myAnim;
    string animScoreUp = "ScoreUp";

    ComboManger theCombo;


    void Start()
    {
        myAnim = GetComponent<Animator>();
        theCombo = FindObjectOfType<ComboManger>();

        // 값을 0으로 초기화
        currentScore = 0;
        txtScore.text = "0";
    }

    public void IncreasecScore(int p_JudgementState) // 점수 증가 메서드
    {
        theCombo.IncreaseCombo(); // 콤보 증가

        // 콤보 보너스 점수 계산
        int t_currentCombo = theCombo.GetCurrentCombo();
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;

        // 가중치 계산
        int t_increasecScore = increaseScore + t_bonusComboScore;        
        t_increasecScore = (int)(t_increasecScore * weight[p_JudgementState]); 

        currentScore += t_increasecScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore); // 점수 포멧 설정

        myAnim.SetTrigger(animScoreUp); // 애니메이션 실행
    }

}
