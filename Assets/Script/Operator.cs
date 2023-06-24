using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : Mapping
{
    public enum roomType { type1, type2, type3}
    roomType type;
    Ray2D ray;
    RaycastHit2D hit;
    public GameObject miniGame;
    private GameObject target;
    public static bool isOperate;
    public float iscoolDown;
    public static GameObject prefab;
    private bool isminigameStart;


    private void Start()
    {
        isOperate = false;
        iscoolDown = 0;
        isminigameStart = false;
        if(gameObject == GameObject.FindWithTag("type1"))
        {
            type = roomType.type1;
        }
    }
    void Update()
    {
        if(iscoolDown > 0)
            Debug.Log(iscoolDown);
        if (Input.GetMouseButtonUp(0))
        {
            CastRay();
            if (target == this.gameObject && iscoolDown <= 0 && !isminigameStart)
            {
                prefab = Instantiate(miniGame, GameObject.Find("map").transform);
                isminigameStart = true;
            }
        }
        if (isOperate)
        {
            isminigameStart = false;
            iscoolDown = 10f;
            isOperate = false;
            switch (type)
            {
                case roomType.type1:
                    //이쪽에 몹 이동에 관여시킬수 있는 함수 넣으면 될듯
                    break;
            }
            Debug.Log(isOperate);
        }
        if (iscoolDown != 0)
        {
            iscoolDown -= Time.deltaTime;
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
