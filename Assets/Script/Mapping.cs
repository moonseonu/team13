using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapping : MonoBehaviour
{
    public GameObject[,] room;
    public GameObject[] roomNum;
    public int[,] Map;

    // Start is called before the first frame update
    void Start()
    {
        room = new GameObject[6,3];
        Map = new int[6, 3];

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 3; j++)
            {

                int random = Random.Range(0, 6);
                room[i, j] = roomNum[random];
                Vector3 position = new Vector3(-6f + (i * 2.3f), 2.3f - (j * 2.3f), 0);
                GameObject prefab = Instantiate(room[i, j], position, Quaternion.identity, GameObject.Find("map").transform);
                prefab.transform.position = position;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
