using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    [SerializeField] float blickSpeed = 0.1f;
    [SerializeField] int blinkCount = 10;
    int currentBlinkCount = 0;
    bool isBlink = false;

    bool isDead = false;

    int maxHp = 3;
    int currentHp = 3;

    int maxShield = 3;
    int currentShield = 0;

    [SerializeField] Image[] hpImage = null;
    [SerializeField] Image[] shieldImage = null;
    [SerializeField] MeshRenderer playerMesh = null;

    [SerializeField] int shieldIncreaseCombo = 5;
    [SerializeField] Image shieldGauge = null;
    int currentShieldCombo = 0;

    Result theResult;
    NoteManager theNote;


    private void Start()
    {
        theResult = FindObjectOfType<Result>();
        theNote = FindObjectOfType<NoteManager>();
    }

    public void Initialized()
    {
        currentHp = maxHp;
        currentShield = 0;
        currentShieldCombo = 0;
        shieldGauge.fillAmount = 0;
        isDead = false;
        SettingHPImage();
        SettingShieldImage();
    }


    public void CheckShield() // 쉴드 체크
    {
        currentShieldCombo++; // 쉴드 콤보 증가

        if(currentShieldCombo >= shieldIncreaseCombo) // 콤보 5회당 쉴드 1개 획득
        {
            currentShieldCombo = 0;
            IncreaseShield();
        }

        shieldGauge.fillAmount = (float)currentShieldCombo / shieldIncreaseCombo; // 쉴드 게이지 계산
    }

    public void ResetShieldCombo() // 쉴드 콤보 리셋
    {
        currentShieldCombo = 0;
        shieldGauge.fillAmount = (float)currentShieldCombo / shieldIncreaseCombo;

    }

    public void IncreaseShield() // 쉴드 증가
    {
        currentShield++;

        if(currentShield >= maxShield)
        {
            currentShield = maxShield;
        }

        SettingShieldImage(); // 쉴드 이미지 세팅
    }

    public void DecreasShield(int p_num) // 쉴드 감소
    {
        currentShield -= p_num;

        if(currentShield <=0)
        {
            currentShield = 0;
        }

        SettingShieldImage();
    }

    public void IncreaseHP(int p_num) // 체력 증가
    {
        currentHp += p_num;

        if(currentHp >= maxHp)
        {
            currentHp = maxHp;
        }

        SettingHPImage();
    }

    public void DecreaseHP(int p_num) // 체력 감소
    {
        if(!isBlink) // 블링크가 true이면
        {
            if(currentShield > 0) // 쉴드가 있으면 쉴드 감소
            {
                DecreasShield(p_num);
            }
            else // 쉴드가 없으면 체력 감소
            {
                currentHp -= p_num;

                if (currentHp <= 0) // 체력이 0이면 
                {
                    isDead = true;
                    theResult.ShowResult(); // 결과창 보여주기
                    theNote.RemoveNote(); // 노트 제거
                }
                else
                {
                    StartCoroutine(BlinkCoroutine()); // 체력이 0보다 크면
                }

                SettingHPImage();
            }            
        }       
    }

    private void SettingHPImage()  // 체력 이미지 효과
    {
        for (int i = 0; i < hpImage.Length; i++)
        {
            if(i < currentHp)
            {
                hpImage[i].gameObject.SetActive(true);
            }
            else
            {
                hpImage[i].gameObject.SetActive(false);
            }

        }
    }

    private void SettingShieldImage()  // 쉴드 이미지 효과
    {
        for (int i = 0; i < shieldImage.Length; i++)
        {
            if (i < currentShield)
            {
                shieldImage[i].gameObject.SetActive(true);
            }
            else
            {
                shieldImage[i].gameObject.SetActive(false);
            }

        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    IEnumerator BlinkCoroutine() // 블링크 효과
    {
        isBlink = true;

        while(currentBlinkCount <= blinkCount)
        {
            playerMesh.enabled = !playerMesh.enabled;
            yield return new WaitForSeconds(blickSpeed);
            currentBlinkCount++;
        }

        playerMesh.enabled = true;
        currentBlinkCount = 0;
        isBlink = false;
    }

}
