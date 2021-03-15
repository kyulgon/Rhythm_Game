using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool s_canPresskey = true; // 죽으면 이동불가능하게 만들 bool 변수

    // 이동
    [SerializeField] float moveSpeed = 3;
    Vector3 dir = new Vector3();
    public Vector3 destPos = new Vector3();
    Vector3 originPos = new Vector3();

    // 회전
    [SerializeField] float spinSpeed = 270;
    Vector3 rotDir = new Vector3();
    Quaternion destRot = new Quaternion();

    // 반동
    [SerializeField] float recoilPosY = 0.25f; // 들썩거리게 만들기 위한 변수
    [SerializeField] float recoilSpeed = 1.5f;

    bool canMove = true;  // 움직임 여부
    bool isFalling = false; // 추락 여부


    // 큐브
    [SerializeField] Transform fakeCube = null; // 가짜큐브를 먼저 돌려놓고 돌아간만큼의 값을 목표 회전값으로 삼음
    [SerializeField] Transform realCube = null; // 회전시킬 객체

    TimingManger theTimingManger;
    CameraController theCam;
    Rigidbody myRigid;

    private void Start()
    {
        theTimingManger = FindObjectOfType<TimingManger>();
        theCam = FindObjectOfType<CameraController>();
        myRigid = GetComponentInChildren<Rigidbody>();
        originPos = transform.position;
    }

    void Update()
    {
        CheckFalling();

        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            if(canMove && s_canPresskey && !isFalling) // 움직일수 있거나 누를수 있으면
            {
                Calc();

                if (theTimingManger.CheckTiming())
                {
                    startAction();
                }
            }           
        }
    }

    void Calc()
    {
        // 방향 계산
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        // 이동 목표값 계산
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        // 회전 목표값 계산
        rotDir = new Vector3(-dir.z, 0f, -dir.x);
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed); // RotateAround(공전대상, 회전축, 회전값) <편법 회전 구현>
        destRot = fakeCube.rotation;
    }

    void startAction()
    {
        StartCoroutine(MoveCoroutine());
        StartCoroutine(SpinCoroutine());
        StartCoroutine(RecoilCoroutine());
        StartCoroutine(theCam.ZoomCam());

    }

    IEnumerator MoveCoroutine() // 움직임 코루틴
    {
        canMove = false;

        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f) // 거리의 제곱근 값이 0.001값(같지 않으면)
        {
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime); // destPos로 감
            yield return null;
        }

        transform.position = destPos; // destPos값을 현재 값으로 설정
        canMove = true;
    }
    
    IEnumerator SpinCoroutine() // 스틴 코루틴
    {
        while(Quaternion.Angle(realCube.rotation, destRot) > 0.5f) // 진짜 큐브와 가짜큐브의 회전값이 차이가 있으면
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime); // 회전
            yield return null;
        }

        realCube.rotation = destRot; // 진짜큐브 회전 값 재설정
    }

    IEnumerator RecoilCoroutine() // 들썩거림 구현
    {
        while(realCube.position.y < recoilPosY) // 큐브 뛰어오름
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        while(realCube.position.y > 0) // 큐브 떨어짐
        {
            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        realCube.localPosition = new Vector3(0, 0, 0); // 위치 초기화
    }

    void CheckFalling() // 추락 구현
    {
        if(!isFalling && canMove)
        {
            if (!Physics.Raycast(transform.position, Vector3.down, 1.1f))
            {
                Falling();
            }
        }       
    }

    void Falling()
    {
        isFalling = true;
        myRigid.useGravity = true;
        myRigid.isKinematic = false;
    }

    public void ResetFalling() // 추락 전으로 돌리기
    {
        isFalling = false;
        myRigid.useGravity = false;
        myRigid.isKinematic = true;

        transform.position = originPos; // 원래 위치로 바꿈
        realCube.localPosition = new Vector3(0, 0, 0); // 자식오브젝트로 원위치
    }

}
