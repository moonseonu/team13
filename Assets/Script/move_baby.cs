using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_baby : MonoBehaviour
{
    // �� ������ŭ 
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
        //roomnum = Random.Range(1.0f, 4.0f);   // �ϴ��� �ּ�ó�� �س��µ� ���߿� Ǯ�� ��
    }

    // Update is called once per frame
    void Update()
    {
        // �̵� ����κ� ���߿� �Լ� �ϳ��� �ļ� update�Լ� ����ϰ� �ص� ��
        if (roomnum < 2.0f && !isspecial_time ) // bool�� ������ ������ ����
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, roomPosition_1.transform.position, speed + Time.deltaTime); // 2D������ �°� ����3 ���� ���� 2�� ����

            if (Vector2.Distance(transform.position, roomPosition_1.position) < 0.2)
            {
                if(waitTime <= 0)
                {
                    //roomnum = Random.Range(1.0f, 4.0f); // �ִ���� �� ���� +1  ���ο� �� ��ǥ ����
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
                    //roomnum = Random.Range(1.0f, 4.0f); // �ִ���� �� ���� +1
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        else if ((roomnum >= 3.0f && roomnum <= 4.0f) && !isspecial_time ) // ���߿� �� �� �ø��� �ε�ȣ �ٲ�ߵ�
        //transform.position = Vector2.MoveTowards(gameObject.transform.position, roomPosition_3.transform.position, speed + Time.deltaTime);
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, roomPosition_3.transform.position, speed + Time.deltaTime);

            if (Vector2.Distance(transform.position, roomPosition_3.position) < 0.2)
            {
                if (waitTime <= 0)
                {
                    //roomnum = Random.Range(1.0f, 4.0f); // �ִ���� �� ���� +1
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        // ������ ������ �� �̵� �κ�      ���⵵ �Լ��� �������� ����ϰ� �ص� ��
        // ������ ���� ������ �����ϰ� ��������
        // ��� ���ʿ� ��� ���ǿ� �߰��ص� �Ǵºκ��ε� �ð���� �ϴ� �̷��� �ص�

        if (special_item < 2.0f)
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, roomPosition_1.transform.position, speed + Time.deltaTime); // 2D������ �°� ����3 ���� ���� 2�� ����

            if (Vector2.Distance(transform.position, roomPosition_1.position) < 0.2)
            {
                if (waitTime <= 0)
                {
                    //roomnum = Random.Range(1.0f, 4.0f); // �ִ���� �� ���� +1  ���ο� �� ��ǥ ����
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
                    //roomnum = Random.Range(1.0f, 4.0f); // �ִ���� �� ���� +1
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
                    //roomnum = Random.Range(1.0f, 4.0f); // �ִ���� �� ���� +1
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

    // �ð� ���������� �� ���� �õ�        ���� �ȵǸ� �״�� ����       ���� �ȴٸ� �� ������ �����������(isspecial_time�� false) ���� ������ ���� ����
    void makeitem()
    {
        if ( itemwaitTime <= 0 )
        {
            if (!isspecial_time)
            {
                special_item = Random.Range(1.0f, 20.0f); // ������ �� ������ 5��� �ؼ� ���� �����Ҷ����� ���� ���ü��� ������ Ȯ���� ���Բ� << ���� ���÷��� �����������

                if (special_item >= 1.0f %% special_item <= 4.0f) // �ִ� ������ �� ������ŭ �ø��� ��
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