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

    // ����� ���߿� ���� ��ũ��Ʈ �İų� �ƴϸ� ��� �ٲ�ߵ�
    //roomnum = Random.Range(1.0f, 4.0f); // �ִ���� �� ���� +1
    //special_item = Random.Range(1.0f, 20.0f); // ������ �� ������ 5��� �ؼ� ���� �����Ҷ����� ���� ���ü��� ������ Ȯ���� ���Բ� << ���� ���÷��� �����������

        
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
        else if (roomnum >= 3.0f && roomnum <= 4.0f) // ���߿� �� �� �ø��� �ε�ȣ �ٲ�ߵ�
            transform.position = Vector3.MoveTowards(gameObject.transform.position, roomPosition_3.transform.position, 0.1f);
    }
}
