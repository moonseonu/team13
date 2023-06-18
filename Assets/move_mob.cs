using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class move_mob : MonoBehaviour
{
    public enum CurrentState { idle, trace, attack, groggy };
    public CurrentState curState = CurrentState.idle;

    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Animator _animator;

    //trace 거리
    public float traceDist = 10.0f;
    //attack 거리
    public float attackDist = 2.0f;
    //groggy 여부
    public bool isGroggy = false;

    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").getComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _animator = this.gameObject.GetComponent<Animator>();

        // player 위치 설정시 시작
        nvAgent.destination = playerTransform.position;

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
    }

    IEnumerator CheckState()
    {
        while (!isGroggy)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTransform.position, _transform.position);

            if (dist <= attackDist)
            {
                curState = CurrentState.attack;
            }
            else if (dist <= traceDist)
            {
                curState = CurrentState.trace;
            }
            else
            {
                curState = CurrentState.idle;
            }
        }
    }

    IEnumerator CheckStateForAction()
    {
        while (!isGroggy)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    nvAgent.Stop();
                    _animator.SetBool(" ", false); //animation input
                    break;
                case CurrentState.trace:
                    nvAgent.destination = playerTransform.position;
                    nvAgent.Resume();
                    _animator.SetBool(" ", true); //animation input
                    break;
                case CurrentState.attack:
                    break;
            }
            yield return null;
        }
    }

}
