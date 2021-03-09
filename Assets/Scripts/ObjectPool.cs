using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public GameObject goPrefab; 
    public int count;
    public Transform tfPoolParent;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance; // 공유자원 instance를 통해 어디서든 public변수, 함수에 접근 가능


    [SerializeField] ObjectInfo[] objectInfo = null; // 배열로 만들어줌

    public Queue<GameObject> noteQueue = new Queue<GameObject>();

    void Start()
    {
        instance = this; // instance 메모리 할당을 하기 위해 Start에 자기 자신 넣어줌
        noteQueue = InsertQueue(objectInfo[0]); // 리턴시킨 값을 noteQueue에 배열0번에 넣어줌
    }

    Queue<GameObject> InsertQueue(ObjectInfo p_objectInfo) // GameObject의 타입을 가지고 있는 큐를 return하는 InsertQueue함수를 만듦
    {
        Queue<GameObject> t_queue = new Queue<GameObject>();

        for (int i = 0; i < p_objectInfo.count; i++)
        {
            GameObject t_clone = Instantiate(p_objectInfo.goPrefab, transform.position, Quaternion.identity); // 프리팹 생성
            t_clone.SetActive(false); // 비활성

            if(p_objectInfo.tfPoolParent != null) // 부모객체가 존재한다면
            {
                t_clone.transform.SetParent(p_objectInfo.tfPoolParent); // 그 객체를 부모로 변경
            }
            else
            {
                t_clone.transform.SetParent(this.transform); // 지금 위치에 부모로 변경
            }

            t_queue.Enqueue(t_clone); // 생성한 객체를 부모에 넣어줌
        }

        return t_queue; // for문을 돌고 난 후에 count의 개수만큼에 객체를 리턴해줌
    }

}

