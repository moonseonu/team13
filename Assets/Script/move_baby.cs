using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_baby : MonoBehaviour
{
    // 방 개수만큼 
    public GameObject roomPosition_1;
    public GameObject roomPosition_2;
    public GameObject roomPosition_3;

    public float roomnum = 1;
    public float special_item = 1;

    // 여기는 나중에 따로 스크립트 파거나 아니면 방식 바꿔야됨
    //roomnum = Random.Range(1.0f, 4.0f); // 최대범위 방 개수 +1
    //special_item = Random.Range(1.0f, 20.0f); // 범위를 방 개수의 5배로 해서 게임 시작할때부터 템이 나올수는 있으나 확률이 적게끔 << 범위 더늘려도 상관은없긴함

        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (roomnum < 2.0f)
            transform.position = Vector3.MoveTowards(gameObject.transform.position, roomPosition_1.transform.position, 0.1f);
        else if (roomnum >= 2.0f && roomnum < 3.0f)
            transform.position = Vector3.MoveTowards(gameObject.transform.position, roomPosition_2.transform.position, 0.1f);
        else if (roomnum >= 3.0f && roomnum <= 4.0f) // 나중에 방 더 늘리면 부등호 바꿔야됨
            transform.position = Vector3.MoveTowards(gameObject.transform.position, roomPosition_3.transform.position, 0.1f);
    }
}
