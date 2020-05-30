using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
enum MoveState
{
    RotateState,
    FollowState,
    AtackState,
    DeadState,
    WalkState
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
    public bool atacking=false,isDead=false;
    [SerializeField] int health=2;
    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        moveVector = new Vector3(0, 0, 0);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        moveDirection = MoveState.FollowState;
    }
    private void FixedUpdate()
    {
            CheckWhereToFace();   
    }
    void CheckWhereToFace()
    {
       switch(moveDirection)
       {
            case MoveState.RotateState://stan w którym postać się obraca
                transform.Rotate(0.0f,90 * Time.deltaTime, 0.0f);
                //TODO: idle annimation state
                break;
            case MoveState.WalkState://stan w którym postać porusza się do przodu

                break;
            case MoveState.FollowState://stan w którym postać w strone gracza
                if (!atacking)
                {
                    agent.SetDestination(player.position);//cel poruszania sie obiektu
                    agent.speed = speed;
                    if (Vector3.Distance(player.position, this.transform.position) < 5)
                        moveDirection = MoveState.AtackState;
                }
                break;
            case MoveState.AtackState:
                agent.SetDestination(player.position);
                anim.SetBool("isAtack", true);
                agent.speed = 0;
                if (Vector3.Distance(player.position, this.transform.position) >= 5)
                    moveDirection = MoveState.FollowState;
                break;

            case MoveState.DeadState:
                isDead = true;
                agent.speed = 0;
                StartDeadAnim();
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
               // agent.enabled = false;
                break;
            default:
                break;
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
    void EndHurtAnim()
    {
        anim.SetBool("isHurt", false);
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
            anim.SetBool("isHurt", true);
        }

    }
}
