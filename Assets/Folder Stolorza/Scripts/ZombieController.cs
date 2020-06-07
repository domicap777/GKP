using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

enum MoveState
{
    RotateState,
    FollowState,
    AtackState,
    DeadState,
    IdleState
}
public class ZombieController : MonoBehaviour
{
    private Transform transform;
<<<<<<< HEAD
    public Transform visionPoint;
=======
    private Rigidbody rb;
>>>>>>> parent of 10150b2... menu
    [SerializeField] private float speed;
    Vector3 moveVector;
    MoveState moveDirection;
    Transform player;
    Animator anim;
    public NavMeshAgent agent;
<<<<<<< HEAD
    private bool atacking = false, isDead = false, looking, isHurt;
    [SerializeField] Transform waypoint1, waypoint2;
    private Transform desinationWaypoint;
=======
    private bool atacking=false,isDead=false,looking ,isHurt;
>>>>>>> parent of 10150b2... menu
    [SerializeField] int health;
    [SerializeField] int demage;
    [SerializeField] float rangeOfView;
    [SerializeField] float rangeOfAtack;
    public float viewAngle;
    public bool patrolState;

    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        moveVector = new Vector3(0, 0, 0);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        moveDirection = MoveState.IdleState;
<<<<<<< HEAD
        if (patrolState)
            moveDirection = MoveState.PatrolState;
        desinationWaypoint = waypoint1;
        agent.autoBraking = false;
=======
>>>>>>> parent of 10150b2... menu
    }
    private void FixedUpdate()
    {
        CheckWhereToFace();
    }
    void CheckWhereToFace()
    {

        switch (moveDirection)
        {
            case MoveState.RotateState://stan w którym postać się obraca
                transform.Rotate(0.0f, 90 * Time.deltaTime, 0.0f);
                //TODO: idle annimation state
                break;
            case MoveState.IdleState://stan w którym postać porusza się do przodu
                anim.SetBool("isIdle", true);
                anim.SetBool("isMove", false);
                agent.speed = 0;
                break;
            case MoveState.FollowState://stan w którym postać w strone gracza
                if (looking)
                {
                    if (!atacking)
                    {
                        anim.SetBool("isMove", true);
                        anim.SetBool("isIdle", false);
                        if (agent.enabled)
                        {

                            agent.SetDestination(player.position);//cel poruszania sie obiektu
                            agent.speed = speed;
                        }
                        if (Vector3.Distance(player.position, this.transform.position) <= rangeOfAtack)
                        {
                            moveDirection = MoveState.AtackState;
                            atacking = true;
                        }
                    }
                }
                break;
            case MoveState.AtackState:
                if (!isHurt)
                {
                    agent.SetDestination(player.position);
                    anim.SetBool("isAtack", true);
                    anim.SetBool("isMove", true);
                    anim.SetBool("isIdle", false);
                    agent.speed = 0;
                    if (Vector3.Distance(player.position, this.transform.position) > rangeOfAtack)
                        moveDirection = MoveState.FollowState;
                }
                break;

            case MoveState.DeadState:
                transform.GetChild(2).GetComponent<BoxCollider>().enabled = false;//get leg colider id
                isDead = true;
                agent.speed = 0;
                //gameObject.GetComponent<CapsuleCollider>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                StartDeadAnim();
                // agent.enabled = false;
                break;
            default:
                break;
        }
        FindPlayerForward();
        CheckIfDead();
    }
<<<<<<< HEAD
    /// <summary>
    /// zmiana punktu w którym ma się poruszać zombie
    /// </summary>
    void swapDestinationPoints()
    {
        if (Vector3.Distance(this.gameObject.transform.position, waypoint1.position) < 2)
        {
            desinationWaypoint = waypoint2;
        }
        if (Vector3.Distance(this.gameObject.transform.position, waypoint2.position) < 2)
        {
            desinationWaypoint = waypoint1;
        }
    }
=======
>>>>>>> parent of 10150b2... menu
    void EndAtackAnim()
    {
        anim.SetBool("isAtack", false);
        atacking = false;
    }
    void StartDeadAnim()
    {
        anim.SetBool("isDead", true);
    }
    void CheckIfDead()
    {
        if (health <= 0)
            if (moveDirection != MoveState.DeadState)
                moveDirection = MoveState.DeadState;
    }
    void StartHurtAnim()
    {
        isHurt = true;
        anim.SetBool("isHurt", true);
        if (health <= 0)
        {
            moveDirection = MoveState.DeadState;
        }
    }
    void EndHurtAnim()
    {
        anim.SetBool("isHurt", false);
        isHurt = false;
    }
    void HurtPlaye()
    {
        if ((Vector3.Distance(player.position, this.transform.position) <= rangeOfAtack + 0.5))
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<hero>().HurtHero(demage);
    }
    public void pistolHit(int demage)
    {
        this.health -= demage;
        if (this.health <= 0)
        {
            moveDirection = MoveState.DeadState;
        }
        else
        {
            StartHurtAnim();
        }

    }

    private void FindPlayerForward()
    {
<<<<<<< HEAD

        if (moveDirection != MoveState.DeadState)
        {

            if (EnemySpotted())
            {
                //RaycastHit hit;
                //if (Physics.Raycast(transform.position, transform.forward, out hit, rangeOfView))
                //{
                //    // Debug.DrawLine(this.transform.position, transform.forward,Color.green,rangeOfView);
=======
        if (moveDirection != MoveState.DeadState)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rangeOfView))
            {
>>>>>>> parent of 10150b2... menu
                StopCoroutine("Watch");
                looking = true;
                moveDirection = MoveState.FollowState;
                if (atacking)
                    moveDirection = MoveState.AtackState;

            }
            else
            {
                StartCoroutine("Watch");
            }
        }
<<<<<<< HEAD


    }
    public bool EnemySpotted()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, visionPoint.forward);

        if (angle < viewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(visionPoint.transform.position, direction.normalized, out hit, rangeOfView))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// Sprawdz czy gracz jest w zaięgu 
    /// </summary>
    /// <returns>rozpocznij kod po czasie 1.1f</returns>
    IEnumerator Watch()
=======
    }
    IEnumerator Watch ()
>>>>>>> parent of 10150b2... menu
    {
        yield return new WaitForSeconds(1.1f);
        // go to idle state
        if (Vector3.Distance(player.position, this.transform.position) > rangeOfView / 2)
        {
            looking = false;
            moveDirection = MoveState.IdleState;
        }
    }

    void DestroyZombieObject()
    {
        StartCoroutine("CleanZombie");
    }
    IEnumerator CleanZombie()
    {
        yield return new WaitForSeconds(2.0f);//destroy object after secounds
        Destroy(gameObject);
    }

}