using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject stage = null; // 스테이지 변수
    Transform[] stagePlates; // 발판 배열

    // 발판 효과
    [SerializeField] float offsetY = -3;
    [SerializeField] float plateSpeed = 10;

    int stepCount = 0; // 발판 효과를 위한 카운트
    int totalPlateCount = 0; // 총 발판 수
    
    void Start()
    {
        stagePlates = stage.GetComponent<Stage>().plates;
        totalPlateCount = stagePlates.Length;

        for (int i = 0; i < totalPlateCount; i++)
        {
            stagePlates[i].position = new Vector3(stagePlates[i].position.x, stagePlates[i].position.y + offsetY, stagePlates[i].position.z);
        }
    }

    public void ShowNextPlate() // 발판 활성화 메서드
    {
        if(stepCount < totalPlateCount) // 총 발판수를 넘지 않으면
        {
            StartCoroutine(MovePlateCoroutine(stepCount++));
        }
       
    }

    IEnumerator MovePlateCoroutine(int p_num) // 발판움직임 코루틴
    {
        stagePlates[p_num].gameObject.SetActive(true);
        Vector3 t_destPos = new Vector3(stagePlates[p_num].position.x, stagePlates[p_num].position.y - offsetY, stagePlates[p_num].position.z);

        while(Vector3.SqrMagnitude(stagePlates[p_num].position - t_destPos) >= 0.001f) // 발판과 목적지까지 거리가 0이 될때까지
        {
            stagePlates[p_num].position = Vector3.Lerp(stagePlates[p_num].position, t_destPos, plateSpeed * Time.deltaTime);
            yield return null;
        }

        stagePlates[p_num].position = t_destPos;

    }


}
