using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Song
{
    public string name;
    public string composer;
    public int bpm;
    public Sprite sprite;
}

public class StageMenu : MonoBehaviour
{
    [SerializeField] Song[] songList = null;
    [SerializeField] Text txtSongName = null;
    [SerializeField] Text txtSongComposer = null;
    [SerializeField] Image imgDisk = null;
    [SerializeField] GameObject TitleMenu = null;

    int currentSong = 0;

    private void Start()
    {
        SettingSong();
    }

    public void BtnNext() // 다음 버튼(음악 바꾸기 버튼)
    {
        AudioManager.instance.PlaySFX("Touch");

        if(++currentSong > songList.Length -1)
        {
            currentSong = 0;
        }
        SettingSong();
    }

    public void BtnPrior() // 이전 버튼(음악 바꾸기 버튼)
    {
        AudioManager.instance.PlaySFX("Touch");

        if (--currentSong < 0)
        {
            currentSong = songList.Length - 1;
        }
        SettingSong();
    }

    private void SettingSong() // 음악 정보 바꾸기
    {
        txtSongName.text = songList[currentSong].name;
        txtSongComposer.text = songList[currentSong].composer;
        imgDisk.sprite = songList[currentSong].sprite;

        AudioManager.instance.PlayBGM("BGM" + currentSong);
    }

    public void BtnBack() // 뒤로 가기 버튼
    {
        TitleMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void BtnPlay() // 시작 버튼
    {
        int t_bpm = songList[currentSong].bpm;

        GameManager.instance.GameStart(currentSong, t_bpm);
        this.gameObject.SetActive(false);
    }

}
