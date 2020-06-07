using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

/// <summary>
/// Klasa do kontroli zachowań i właściwości przeciwnika zombie
/// </summary>
enum MoveState
{
    PatrolState,
    FollowState,
    AtackState,
    DeadState,
    IdleState
}

public class ZombieController : MonoBehaviour
{

    private Transform transform;
    [SerializeField] private float speed;
    MoveState moveDirection;
    Transform player;
    Animator anim;
    public NavMeshAgent agent;
    private bool atacking=false,isDead=false,looking ,isHurt;
    [SerializeField] Transform waypoint1,waypoint2;
    private Transform desinationWaypoint;
    [SerializeField] int health;
    [SerializeField] int demage;
    [SerializeField] float rangeOfView;
    [SerializeField] float rangeOfAtack;
    private float viewAngle;

    void Start()
    {
        transform = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        moveDirection = MoveState.IdleState;
        desinationWaypoint = waypoint1;
        agent.autoBraking = false;
    }
    private void FixedUpdate()
    {
        CheckWhereToFace();
    }
    /// <summary>
    /// Rozpoznanie i wykonanie stanu zachowania w którym znajduje się zombie
    /// </summary>
    void CheckWhereToFace()
    {
        
        switch (moveDirection)
        {
            case MoveState.PatrolState://stan w którym postać się obraca
                agent.SetDestination(desinationWaypoint.position);
                swapDestinationPoints();
                anim.SetBool("isIdle", false);
                anim.SetBool("isMove", true);
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
                    anim.SetBool("isMove", false);
                    anim.SetBool("isIdle", false);
                    agent.speed = 0;
                    if (Vector3.Distance(player.position, this.transform.position) > rangeOfAtack)
                        moveDirection = MoveState.FollowState;
                }
                break;

            case MoveState.DeadState:
                transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;//get leg colider id
                isDead = true;
                agent.speed = 0;
                //gameObject.GetComponent<CapsuleCollider>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                StartDeadAnim();
                break;
            default:
                break;
       }
        FindPlayerForward();
        CheckIfDead();
    }
    /// <summary>
    /// zmiana punktu w którym ma się poruszać zombie
    /// </summary>
    void swapDestinationPoints()
    {
        if(Vector3.Distance(this.gameObject.transform.position,waypoint1.position) < 2)
        {
            desinationWaypoint = waypoint2;
        }
        if (Vector3.Distance(this.gameObject.transform.position, waypoint2.position) < 2)
        {
            desinationWaypoint = waypoint1;
        }
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
    /// Kiedy obiekt otrzymuje obrażenia wykonaj animacje po czasie albo zmień stan
    /// </summary>
    /// <param name="demage">ilosc obrazen</param>
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
    /// <summary>
    /// Zrób Raycast aby sprawdzić czy nie w zasięgu nie znajduje się gracz 
    /// </summary>
    private void FindPlayerForward()
    {
        if(moveDirection !=MoveState.PatrolState)
        { 
        if (moveDirection != MoveState.DeadState)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rangeOfView))
            {
               // Debug.DrawLine(this.transform.position, transform.forward,Color.green,rangeOfView);
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
    /// <summary>
    /// Sprawdz czy gracz jest w zaięgu 
    /// </summary>
    /// <returns>rozpocznij kod po czasie 1.1f</returns>
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
    /// <summary>
    /// Zniszcz obiekt zombie
    /// </summary>
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
