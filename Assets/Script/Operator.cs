using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : MonoBehaviour
{
    public enum roomType { type1, type2, type3}
    roomType type;
    Ray2D ray;
    RaycastHit2D hit;
    public GameObject miniGame;
    private GameObject target;
    public static bool isOperate;
    public float iscoolDown;
    
    private void Start()
    {
        isOperate = false;
        iscoolDown = 0;
        if(gameObject == GameObject.FindWithTag("type1"))
        {
            type = roomType.type1;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CastRay();
            if(target == this.gameObject && iscoolDown == 0)
            {
                miniGame.SetActive(true);
            }
        }
        if (isOperate)
        {
            miniGame.SetActive(false);
            isOperate = false;
            iscoolDown = 30f;
            switch (type) 
            {
                case roomType.type1:
                    //이쪽에 몹 이동에 관여시킬수 있는 함수 넣으면 될듯
                break;
            }
        }
        if(iscoolDown != 0)
        {
            iscoolDown -= Time.deltaTime;
        }
        Debug.Log(iscoolDown);
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
