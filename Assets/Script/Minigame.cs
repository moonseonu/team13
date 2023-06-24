using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Minigame : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    Vector3 startPos;
    private bool  isDraw, isClear;
    private void Awake()
    {
        isClear = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == GameObject.Find("start"))
        {
            startPos = GameObject.Find("start").GetComponent<Transform>().position;
            isDraw = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isDraw)
            gameObject.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.transform.position = startPos;
        if (isClear)
        {
            Debug.Log("successing");
            Operator.isOperate = true;
            isClear = false;
            Destroy(Operator.minigameprefab);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isDraw = true;
        if(collision.gameObject == GameObject.Find("end"))
        {
            isClear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDraw = false;
    }

}
