using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUI = null;

    [SerializeField] Text[] txtCount = null;
    [SerializeField] Text txtCoin = null;
    [SerializeField] Text txtScore = null;
    [SerializeField] Text txtMaxCombo = null;

    ScoreManager theScore;
    ComboManger theCombo;
    TimingManger theTiming;

    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theCombo = FindObjectOfType<ComboManger>();
        theTiming = FindObjectOfType<TimingManger>();
    }

    public void ShowResult()
    {
        goUI.SetActive(true);

        for (int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text = "0";
        }

        txtCoin.text = "0";
        txtScore.text = "0";
        txtMaxCombo.text = "0";

        int[] t_judgement = theTiming.GetJudgementRecord(); // 판정 기록
        int t_currentScore = theScore.GeteCurrentScore(); // 점수 기록
        int t_maxCombo = theCombo.GetMaxCombo(); // max콤보 기록
        int t_coin = (t_currentScore / 50); // 코인 기록

        for (int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text = string.Format("{0:#,##0}", t_judgement[i]);
        }

        txtScore.text = string.Format("{0:#,##0}", t_currentScore);
        txtMaxCombo.text = string.Format("{0:#,##0}", t_maxCombo);
        txtCoin.text = string.Format("{0:#,##0}", t_coin);
    }

}
