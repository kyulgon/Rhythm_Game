﻿using System.Collections;
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

    int currentSong = 0;


    ScoreManager theScore;
    ComboManger theCombo;
    TimingManger theTiming;
    DatabaseManager theDatabase;

    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theCombo = FindObjectOfType<ComboManger>();
        theTiming = FindObjectOfType<TimingManger>();
        theDatabase = FindObjectOfType<DatabaseManager>();
    }

    public void SetCurrentSong(int p_songNum) // 현재 음악 셋
    {
        currentSong = p_songNum;
    }

    public void ShowResult()
    {
        FindObjectOfType<CenterFlame>().ResetMusic(); // 음악 리셋하기

        AudioManager.instance.StopBGM(); // 브금 멈춤

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

        if(t_currentScore > theDatabase.score[currentSong]) // 최고 기록이면 점수 넣어줌
        {
            theDatabase.score[currentSong] = t_currentScore;
            theDatabase.SvaeScore();
        }        
    }

    public void BtnMainMenu()
    {
        goUI.SetActive(false);
        GameManager.instance.MainMenu();
        theCombo.ResetCombo();
    }

}
