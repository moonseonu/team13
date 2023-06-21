using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : MonoBehaviour
{
    public enum roomType { type1, type2, type3}
    roomType type;
    Vector3 room1Pos, mousePos;
    Ray2D ray;
    RaycastHit2D hit;
    public GameObject miniGame;
    private GameObject target;
    public static bool isOperate;
    
    private void Start()
    {
        isOperate = false;
        if(gameObject == GameObject.Find("Room1"))
        {
            type = roomType.type1;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CastRay();
            if(target == GameObject.Find("Room1"))
            {
                Debug.Log("Operating room1");
                miniGame.SetActive(true);
            }
        }
        if (isOperate)
        {
            miniGame.SetActive(false);
            isOperate = false;
            switch (type) 
            {
                case roomType.type1:
                    //이쪽에 몹 이동에 관여시킬수 있는 함수 넣으면 될듯
                break;
            }
        }
    }

    void CastRay()
    {
        target = null;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject; 
        }
    }
}
