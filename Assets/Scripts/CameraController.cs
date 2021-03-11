using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform thePlayer = null; // 카메라가 따라갈 변수
    [SerializeField] float followSpeed = 15; // 따라갈 스피드

    Vector3 playerDistance = new Vector3(); // 플레이어와 거리

    float hitDistance = 0; // 일반 앞으로 가려고 사용함
    [SerializeField] float zoomDistance = -1.25f; // 뒤로 땡기기

    private void Start()
    {
        playerDistance = transform.position - thePlayer.position; // 플레이어와 거리 계산
    }

    private void Update()
    {
        Vector3 t_destPos = thePlayer.position + playerDistance + (transform.forward * hitDistance); // 목적지 포지션 구하기
        transform.position = Vector3.Lerp(transform.position, t_destPos, followSpeed * Time.deltaTime); // Lerp() 실행
    }

    public IEnumerator ZoomCam() // 캠을 당기기
    {
        hitDistance = zoomDistance;

        yield return new WaitForSeconds(0.15f);

        hitDistance = 0;
    }
}
