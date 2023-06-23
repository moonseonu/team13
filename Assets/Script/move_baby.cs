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
    public bool isspecial_time = false;

    public float speed;
    public float startWaitTime;
    private float waitTime;

    public float startitemTime;
    private float itemwaitTime;

    
        
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        itemwaitTime = startitemTime;
        //roomnum = Random.Range(1.0f, 4.0f);   // 일단은 주석처리 해놨는데 나중에 풀면 됨
    }

    // Update is called once per frame
    void Update()
    {
        // 이동 제어문부분 나중에 함수 하나로 파서 update함수 깔끔하게 해도 됨
        if (roomnum < 2.0f && !isspecial_time ) // bool로 아이템 없을때 적용
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, roomPosition_1.transform.position, speed + Time.deltaTime); // 2D공간에 맞게 벡터3 에서 벡터 2로 변경

            if (Vector2.Distance(transform.position, roomPosition_1.position) < 0.2)
            {
                if(waitTime <= 0)
                {
                    //roomnum = Random.Range(1.0f, 4.0f); // 최대범위 방 개수 +1  새로운 방 좌표 생성
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        else if ((roomnum >= 2.0f && roomnum < 3.0f) && !isspecial_time )
        //transform.position = Vector2.MoveTowards(gameObject.transform.position, roomPosition_2.transform.position, speed + Time.deltaTime);
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, roomPosition_2.transform.position, speed + Time.deltaTime);

            if (Vector2.Distance(transform.position, roomPosition_2.position) < 0.2)
            {
                if (waitTime <= 0)
                {
                    //roomnum = Random.Range(1.0f, 4.0f); // 최대범위 방 개수 +1
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        else if ((roomnum >= 3.0f && roomnum <= 4.0f) && !isspecial_time ) // 나중에 방 더 늘리면 부등호 바꿔야됨
        //transform.position = Vector2.MoveTowards(gameObject.transform.position, roomPosition_3.transform.position, speed + Time.deltaTime);
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, roomPosition_3.transform.position, speed + Time.deltaTime);

            if (Vector2.Distance(transform.position, roomPosition_3.position) < 0.2)
            {
                if (waitTime <= 0)
                {
                    //roomnum = Random.Range(1.0f, 4.0f); // 최대범위 방 개수 +1
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        // 아이템 생성시 방 이동 부분      여기도 함수로 빼버리고 깔끔하게 해도 됨
        // 아이템 동시 생성은 배제하고 생각했음
        // 사실 위쪽에 제어문 조건에 추가해도 되는부분인데 시간없어서 일단 이렇게 해둠

        if (special_item < 2.0f)
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, roomPosition_1.transform.position, speed + Time.deltaTime); // 2D공간에 맞게 벡터3 에서 벡터 2로 변경

            if (Vector2.Distance(transform.position, roomPosition_1.position) < 0.2)
            {
                if (waitTime <= 0)
                {
                    //roomnum = Random.Range(1.0f, 4.0f); // 최대범위 방 개수 +1  새로운 방 좌표 생성
                    isspecial_time = false;
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        else if ( special_item >= 2.0f && special_item < 3.0f )
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, roomPosition_2.transform.position, speed + Time.deltaTime);

            if (Vector2.Distance(transform.position, roomPosition_2.position) < 0.2)
            {
                if (waitTime <= 0)
                {
                    //roomnum = Random.Range(1.0f, 4.0f); // 최대범위 방 개수 +1
                    isspecial_time = false;
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        else if (special_item >= 3.0f && special_item <= 4.0f)
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, roomPosition_3.transform.position, speed + Time.deltaTime);

            if (Vector2.Distance(transform.position, roomPosition_3.position) < 0.2)
            {
                if (waitTime <= 0)
                {
                    //roomnum = Random.Range(1.0f, 4.0f); // 최대범위 방 개수 +1
                    isspecial_time = false;
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        makeitem();
    }

    // 시간 지날때마다 템 생성 시도        생성 안되면 그대로 진행       생성 된다면 그 아이템 사라질때까지(isspecial_time이 false) 다음 아이템 생성 안함
    void makeitem()
    {
        if ( itemwaitTime <= 0 )
        {
            if (!isspecial_time)
            {
                special_item = Random.Range(1.0f, 20.0f); // 범위를 방 개수의 5배로 해서 게임 시작할때부터 템이 나올수는 있으나 확률이 적게끔 << 범위 더늘려도 상관은없긴함

                if (special_item >= 1.0f %% special_item <= 4.0f) // 최대 범위는 방 개수만큼 늘리면 됨
                {
                    isspecial_time = true;
                }
            }
            itemwaitTime = startitemTime;
        }
        else
        {
            itemwaitTime -= Time.deltaTime;           
        }
        
        
    }
}