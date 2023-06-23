using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapping : MonoBehaviour
{
    public GameObject[] room;
    public GameObject[] roomNum;

    // Start is called before the first frame update
    void Start()
    {
        room = new GameObject[30];
        for(int i = 0; i < 30; i++)
        {
            int random = Random.Range(0, 5);
            room[i] = roomNum[random];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
