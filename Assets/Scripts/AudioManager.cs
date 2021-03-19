using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] Sound[] sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    private void Start()
    {
        instance = this;
    }

    public void PlayBGM(string p_bgmName) // BGM 플레이
    {
        for (int i = 0; i < bgm.Length; i++) // bgm 전부 확인
        {
            if(p_bgmName == bgm[i].name) // 이름이 같으면
            {
                // i번째 클립을 bgmPlayer 클립에 넣어서 Play()
                bgmPlayer.clip = bgm[i].clip; 
                bgmPlayer.Play();
            }
        }
    }

    public void StopBGM() // BGM 멈춤
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName) // SFX 플레이
    {
        for (int i = 0; i < sfx.Length; i++) // sfx 전부 확인
        {
            if (p_sfxName == sfx[i].name) // 이름이 같으면
            {
                for (int x = 0; x < sfxPlayer.Length; x++) // 비어있는 Player 찾기
                {
                    sfxPlayer[x].clip = sfx[i].clip;
                    sfxPlayer[x].Play();

                    return;
                }

                Debug.Log("모든 오디오 플레이어가 재생중입니다.");
                return;
            }
        }

        Debug.Log(p_sfxName + "이름의 효과음이 없습니다.");
    }
}
