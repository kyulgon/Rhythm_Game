using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManger : MonoBehaviour
{
    [SerializeField] GameObject goCombooImage = null;
    [SerializeField] Text txtCombo = null;

    int currentCombo = 0;
    int maxCombo = 0;

    Animator myAnimator;
    string animComboUp = "ComboUp";

    private void Start()
    {
        // 시작 시 콤보 비활성화
        txtCombo.gameObject.SetActive(false);
        goCombooImage.SetActive(false);

        myAnimator = GetComponent<Animator>();
    }

    public void IncreaseCombo(int p_num = 1) // 콤포 증가 메서드
    {
        currentCombo += p_num;
        txtCombo.text = string.Format("{0:#,##0}", currentCombo); // 텍스트 포멧 설정

        if(maxCombo < currentCombo) // max콤보 바꾸기
        {
            maxCombo = currentCombo;
        }

        if(currentCombo > 2) // 3콤보부터 보이기
        {
            txtCombo.gameObject.SetActive(true);
            goCombooImage.SetActive(true);

            myAnimator.SetTrigger(animComboUp); // 콤보 애니메이션
        }
    }

    public int GetCurrentCombo()
    {
        return currentCombo;
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        txtCombo.text = "0";
        txtCombo.gameObject.SetActive(false);
        goCombooImage.SetActive(false);        
    }

    public int GetMaxCombo()
    {
        return maxCombo;
    }
}
