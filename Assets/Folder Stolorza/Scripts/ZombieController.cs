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
    private Rigidbody rb;
    [SerializeField] private float speed;
    Vector3 moveVector;
    MoveState moveDirection;
    Transform player;
    Animator anim;
    public NavMeshAgent agent;
    private bool atacking=false,isDead=false,looking ,isHurt;
    [SerializeField] int health;
    [SerializeField] int demage;
    [SerializeField] float rangeOfView;
    [SerializeField] float rangeOfAtack;
    private float viewAngle;

    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        moveVector = new Vector3(0, 0, 0);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        moveDirection = MoveState.IdleState;
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
        if(health<=0)
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="demage">asd</param>
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
        if (moveDirection != MoveState.DeadState)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rangeOfView))
            {
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
    }
    //private void lookingg()
    //{
    //    Vector3 direction = player.position - transform.position;
    //    float angle = Vector3.Angle(direction, transform.forward);
    //    if(angle*2 <= viewAngle)
    //    {
    //        RaycastHit hit;
    //        if (Physics.Raycast(transform.position,)
    //    }
    //}
    IEnumerator Watch ()
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
