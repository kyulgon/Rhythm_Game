using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null; // 애니메이터 통제 변수
    string hit = "Hit"; // 애니메이터의 트리거 파라미터 값

    [SerializeField] Animator judgementAnimator = null; // 판정 애니메이터
    [SerializeField] Image judgementImage = null; // 교체할 이미지
    [SerializeField] Sprite[] judgementSprite = null; // 스프라이트 담을 변수

    public void JudgementEffect(int p_num) // 스프라이트 교체
    {
        judgementImage.sprite = judgementSprite[p_num];
        judgementAnimator.SetTrigger(hit);
    }

    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(hit); // 애니메이션 트리거 실행
    }
}
