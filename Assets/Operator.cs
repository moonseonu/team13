using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : MonoBehaviour
{
    LineRenderer lr;
    Vector3 room1Pos, mousePos;
    Ray2D ray;
    RaycastHit2D hit;
    private GameObject target;
    private void Start()
    {
        
        lr = GetComponent<LineRenderer>();
        lr.startWidth = .05f;
        lr.endWidth = .05f;
        //라인 굵기

        room1Pos = gameObject.GetComponent<Transform>().position;
        //시작점 위치
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
            if (target == this.gameObject)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                ray = new Ray2D(mousePos, Vector2.zero);

                lr.SetPosition(0, room1Pos);
                lr.SetPosition(1, mousePos);
            }
        }

        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            lr.SetPosition(1, mousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            CastRay();
            if(target == GameObject.Find("Room5"))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                lr.SetPosition(1, GameObject.Find("Room5").GetComponent<Transform>().position);
            }
            else
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                lr.SetPosition(1, room1Pos);
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
