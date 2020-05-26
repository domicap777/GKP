using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
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
    private float directionX,directionZ;
    MoveState moveDirection;
    public bool ZRotation;
    Transform player;
    Animator anim;
    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        moveVector = new Vector3(0, 0, 0);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {

        if (ZRotation)
            moveDirection = MoveState.FollowState;
        else
            moveDirection = MoveState.WalkState;
        if (Vector3.Distance(player.position, this.transform.position) < 10)
        {
            moveDirection = MoveState.FollowState;
        }
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
              moveVector = gameObject.transform.rotation * Vector3.forward * speed;
                rb.position += moveVector;
                break;
            case MoveState.FollowState://stan w którym postać w strone gracza
                    Vector3 direcion = player.position - this.transform.position;
                    direcion.y = 0;
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                        Quaternion.LookRotation(direcion), 0.1f);
                    moveVector = gameObject.transform.rotation * Vector3.forward * speed;
                    rb.position += moveVector;
                    break;
            case MoveState.AtackState:

                break;

            case MoveState.DeadState:

                break;
            default:
                break;
       }

    }
    void EndAtackAnim()
    {
        anim.SetBool("isAtack", false);
    }
    void StartDeadAnim()
    {
        anim.SetBool("isDead", true);
    }
}
