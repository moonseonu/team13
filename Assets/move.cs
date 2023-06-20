using UnityEngine;
using System.Collections;

public class move : MonoBehaviour
{

    
    public bool on = true; 
    public bool canFly = false; 
    public float floatHeight = 0.0f;
    public bool runAway = false; 
    public bool runTo = false; 
    public float runDistance = 25.0f;
    public float runBufferDistance = 50.0f; 
    public int walkSpeed = 10;
    public int runSpeed = 15;
    public int randomSpeed = 10;
    public float rotationSpeed = 20.0f;
    public float visualRadius = 100.0f;
    public float moveableRadius = 200.0f;
    public float attackRange = 10.0f;
    public float attackTime = 0.50f;
    public bool useWaypoints = false;
    public bool reversePatrol = true;
    public Transform[] waypoints;
    public bool pauseAtWaypoints = false; 
    public float pauseMin = 1.0f;
    public float pauseMax = 3.0f;
    public float huntingTimer = 5.0f;
    public bool estimateElevation = false;
    public float estRayTimer = 1.0f;
    public bool requireTarget = true;
    public Transform target;


    private bool initialGo = false;
    private bool go = true;
    private Vector3 lastVisTargetPos;
    CharacterController characterController;
    private bool playerHasBeenSeen = false;
    private bool enemyCanAttack = false;
    private bool enemyIsAttacking = false;
    private bool executeBufferState = false;
    private bool walkInRandomDirection = false;
    private float lastShotFired;
    private float lostPlayerTimer;
    private bool targetIsOutOfSight;
    private Vector3 randomDirection;
    private float randomDirectionTimer;
    private float gravity = 20.0f;
    private float antigravity = 2.0f;
    private float estHeight = 0.0f;
    private float estGravityTimer = 0.0f;
    private int estCheckDirection = 0;
    private bool wpCountdown = false;
    private bool monitorRunTo = false;
    private int wpPatrol = 0;
    private bool pauseWpControl;
    private bool smoothAttackRangeBuffer = false;


    void Start()
    {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        if ((estimateElevation)(floatHeight > 0.0f))
        {
            estGravityTimer = Time.time;
        }
        characterController = gameObject.GetComponent<CharacterController>();
        initialGo = true;
        yield return null;
    }


    void Update()
    {
        if (!on || !initialGo)
        {
            return;
        }
        else
        {
            AIFunctionality();
        }
    }


    void AIFunctionality()
    {
        if ((!target)(requireTarget))
        {
            return;
        }

        lastVisTargetPos = target.position;
        Vector3 moveToward = lastVisTargetPos - transform.position;
        Vector3 moveAway = transform.position - lastVisTargetPos;
        float distance = Vector3.Distance(transform.position, target.position);

        if (go)
        {
            MonitorGravity();
        }

        if (!requireTarget)
        {
            Patrol();
        }
        else if (TargetIsInSight())
        {
            if (!go)
            {
                return;
            }

            if ((distance > attackRange)(!runAway)(!runTo))
            {
                enemyCanAttack = false;
                MoveTowards(moveToward);
            }
            else if ((smoothAttackRangeBuffer)(distance > attackRange + 5.0f))
            {
                smoothAttackRangeBuffer = false;
                WalkNewPath();
            }
            else if ((runAway || runTo)(distance > runDistance)(!executeBufferState))
            {
                if (monitorRunTo)
                {
                    monitorRunTo = false;
                }
                if (runAway)
                {
                    WalkNewPath();
                }
                else
                {
                    MoveTowards(moveToward);
                }
            }
            else if ((runAway || runTo)(distance < runDistance)(!executeBufferState))
            {
                enemyCanAttack = false;
                if (!monitorRunTo)
                {
                    executeBufferState = true;
                }
                walkInRandomDirection = false;
                if (runAway)
                {
                    MoveTowards(moveAway);
                }
                else
                {
                    MoveTowards(moveToward);
                }
            }
            else if (executeBufferState((runAway)(distance < runBufferDistance)) || ((runTo)(distance > runBufferDistance)))
            {
                if (runAway)
                {
                    MoveTowards(moveAway);
                }
                else
                {
                    MoveTowards(moveToward);
                }
            }
            else if ((executeBufferState)(((runAway)(distance > runBufferDistance)) || ((runTo)(distance < runBufferDistance))))
            {
                monitorRunTo = true;
                executeBufferState = false;
            }

            if ((distance < attackRange) || ((!runAway!runTo)  (distance < runDistance))) {
                if (runAway)
                {
                    smoothAttackRangeBuffer = true;
                }
                if (Time.time > lastShotFired + attackTime)
                {
                    StartCoroutine(Attack());
                }
            }

        }
        else if ((playerHasBeenSeen)(!targetIsOutOfSight)(go))
        {
            lostPlayerTimer = Time.time + huntingTimer;
            StartCoroutine(HuntDownTarget(lastVisTargetPos));
        }
        else if (useWaypoints)
        {
            Patrol();
        }
        else if (((!playerHasBeenSeen)(go))((moveableRadius == 0) || (distance < moveableRadius)))
        {
            WalkNewPath();
        }
    }


    IEnumerator Attack()
    {
        enemyCanAttack = true;

        if (!enemyIsAttacking)
        {
            enemyIsAttacking = true;
            while (enemyCanAttack)
            {
                lastShotFired = Time.time;
                yield return new WaitForSeconds(attackTime);
            }
        }
    }


    bool TargetIsInSight()
    {
        if ((moveableRadius > 0)(Vector3.Distance(transform.position, target.position) > moveableRadius))
        {
            go = false;
        }
        else
        {
            go = true;
        }

        if ((visualRadius > 0)(Vector3.Distance(transform.position, target.position) > visualRadius))
        {
            return false;
        }

        RaycastHit sight;
        if (Physics.Linecast(transform.position, target.position, out sight))
        {
            if (!playerHasBeenSeen  sight.transform == target) {
                playerHasBeenSeen = true;
            }
            return sight.transform == target;
        }
        else
        {
            return false;
        }
    }


    IEnumerator HuntDownTarget(Vector3 position)
    {
        targetIsOutOfSight = true;
        while (targetIsOutOfSight)
        {
            Vector3 moveToward = position - transform.position;
            MoveTowards(moveToward);

            if (TargetIsInSight())
            {
                targetIsOutOfSight = false;
                break;
            }

            if (Time.time > lostPlayerTimer)
            {
                targetIsOutOfSight = false;
                playerHasBeenSeen = false;
                break;
            }
            yield return null;
        }
    }


    void Patrol()
    {
        if (pauseWpControl)
        {
            return;
        }
        Vector3 destination = CurrentPath();
        Vector3 moveToward = destination - transform.position;
        float distance = Vector3.Distance(transform.position, destination);
        MoveTowards(moveToward);
        if (distance <= 1.5f + floatHeight)
        {
            if (pauseAtWaypoints)
            {
                if (!pauseWpControl)
                {
                    pauseWpControl = true;
                    StartCoroutine(WaypointPause());
                }
            }
            else
            {
                NewPath();
            }
        }
    }


    IEnumerator WaypointPause()
    {
        yield return new WaitForSeconds(Random.Range(pauseMin, pauseMax));
        NewPath();
        pauseWpControl = false;
    }


    Vector3 CurrentPath()
    {
        return waypoints[wpPatrol].position;
    }


    void NewPath()
    {
        if (!wpCountdown)
        {
            wpPatrol++;
            if (wpPatrol >= waypoints.GetLength(0))
            {
                if (reversePatrol)
                {
                    wpCountdown = true;
                    wpPatrol -= 2;
                }
                else
                {
                    wpPatrol = 0;
                }
            }
        }
        else if (reversePatrol)
        {
            wpPatrol--;
            if (wpPatrol < 0)
            {
                wpCountdown = false;
                wpPatrol = 1;
            }
        }
    }


    void WalkNewPath()
    {
        if (!walkInRandomDirection)
        {
            walkInRandomDirection = true;
            if (!playerHasBeenSeen)
            {
                randomDirection = new Vector3(Random.Range(-0.15f, 0.15f), 0, Random.Range(-0.15f, 0.15f));
            }
            else
            {
                randomDirection = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            }
            randomDirectionTimer = Time.time;
        }
        else if (walkInRandomDirection)
        {
            MoveTowards(randomDirection);
        }

        if ((Time.time - randomDirectionTimer) > 2)
        {
            walkInRandomDirection = false;
        }
    }


    void MoveTowards(Vector3 direction)
    {
        direction.y = 0;
        int speed = walkSpeed;

        if (walkInRandomDirection)
        {
            speed = randomSpeed;
        }

        if (executeBufferState)
        {
            speed = runSpeed;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float speedModifier = Vector3.Dot(forward, direction.normalized);
        speedModifier = Mathf.Clamp01(speedModifier);

        direction = forward * speed * speedModifier;
        if ((!canFly)(floatHeight <= 0.0f))
        {
            direction.y -= gravity;
        }
        characterController.Move(direction * Time.deltaTime);
    }


    void MonitorGravity()
    {
        Vector3 direction = new Vector3(0, 0, 0);

        if ((!canFly)(floatHeight > 0.0f))
        {
            if ((estimateElevation)(estRayTimer > 0.0f))
            {
                if (Time.time > estGravityTimer)
                {
                    RaycastHit floatCheck;
                    if (Physics.Raycast(transform.position, -Vector3.up, out floatCheck))
                    {
                        if (floatCheck.distance < floatHeight - 0.5f)
                        {
                            estCheckDirection = 1;
                            estHeight = floatHeight - floatCheck.distance;
                        }
                        else if (floatCheck.distance > floatHeight + 0.5f)
                        {
                            estCheckDirection = 2;
                            estHeight = floatCheck.distance - floatHeight;
                        }
                        else
                        {
                            estCheckDirection = 3;
                        }
                    }
                    else
                    {
                        estCheckDirection = 2;
                        estHeight = floatHeight * 2;
                    }
                    estGravityTimer = Time.time + estRayTimer;
                }

                switch (estCheckDirection)
                {
                    case 1:
                        direction.y += antigravity;
                        estHeight -= direction.y * Time.deltaTime;
                        break;
                    case 2:
                        direction.y -= gravity;
                        estHeight -= direction.y * Time.deltaTime;
                        break;
                    default:
                        break;
                }

            }
            else
            {
                RaycastHit floatCheck;
                if (Physics.Raycast(transform.position, -Vector3.up, out floatCheck, floatHeight + 1.0f))
                {
                    if (floatCheck.distance < floatHeight)
                    {
                        direction.y += antigravity;
                    }
                }
                else
                {
                    direction.y -= gravity;
                }
            }
        }
        else
        {
            if ((estimateElevation)(estRayTimer > 0.0f))
            {
                if (Time.time > estGravityTimer)
                {
                    RaycastHit floatCheck;
                    if (Physics.Raycast(transform.position, -Vector3.up, out floatCheck))
                    {
                        if (floatCheck.distance < floatHeight - 0.5f)
                        {
                            estCheckDirection = 1;
                            estHeight = floatHeight - floatCheck.distance;
                        }
                        else if (floatCheck.distance > floatHeight + 0.5f)
                        {
                            estCheckDirection = 2;
                            estHeight = floatCheck.distance - floatHeight;
                        }
                        else
                        {
                            estCheckDirection = 3;
                        }
                    }
                    estGravityTimer = Time.time + estRayTimer;
                }

                switch (estCheckDirection)
                {
                    case 1:
                        direction.y += antigravity;
                        estHeight -= direction.y * Time.deltaTime;
                        break;
                    case 2:
                        direction.y -= antigravity;
                        estHeight -= direction.y * Time.deltaTime;
                        break;
                    default:
                        break;
                }

            }
            else
            {
                RaycastHit floatCheck;
                if (Physics.Raycast(transform.position, -Vector3.up, out floatCheck))
                {
                    if (floatCheck.distance < floatHeight - 0.5f)
                    {
                        direction.y += antigravity;
                    }
                    else if (floatCheck.distance > floatHeight + 0.5f)
                    {
                        direction.y -= antigravity;
                    }
                }
            }
        }

        if ((!estimateElevation) || ((estimateElevation)(estHeight >= 0.0f)))
        {
            characterController.Move(direction * Time.deltaTime);
        }
    }
}