using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityStandardAssets.Vehicles.Aeroplane;

public class BossControler : MonoBehaviour
{
    Animation aniations;
    private float adlet;
    private float walkingt;
    private float runt;
    private float jumpt;
    private float throwt;
    private float begint;
    private float groundHitt;
    private float deadt;
    private float landingt;
    bool haveAction=false;
    Transform player;
    [SerializeField]
    private GameObject fireBall;
    [SerializeField]
    GameObject image;
    private int health=300;
    RectTransform healthImage;

    // Start is called before the first frame update

    void Start()
    {
        aniations = GetComponent<Animation>();
        adlet = aniations["adle"].length;
        walkingt = aniations["walking"].length;
        runt = aniations["run"].length;
        jumpt = aniations["jump"].length;
        throwt = aniations["throw"].length;
        begint = aniations["begin"].length;
        groundHitt = aniations["groundHit"].length;
        deadt = aniations["dead"].length;
        landingt = aniations["landing"].length;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        healthImage = image.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.position - transform.position);
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        if(!haveAction)
        {
            throeFireBall();
        }
        
    }

    private void throeFireBall()
    {

        aniations.Play("throw");
        StartCoroutine("actionDelay", throwt);
        StartCoroutine("Throwball");

    }

    void firstLanding(Vector3 position)
    {
        this.transform.parent.transform.position = new Vector3(87, 7, 19);
        aniations.Play("landing");
        StartCoroutine("delay", landingt-0.2f);
        aniations["begin"].layer = 1;
        aniations.Play("begin");
        aniations["begin"].weight = 0.4f;
        StartCoroutine("actionDelay",4f);
    }

    IEnumerator Throwball()
    {
        yield return new WaitForSeconds(0.6f);
        Vector3 throwPoint = transform.position + new Vector3(1, 2, 1);
        Vector3 direction = player.position - throwPoint;
        GameObject ball = (GameObject)Instantiate(fireBall, throwPoint, Quaternion.FromToRotation(-Vector3.forward, direction));
        Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.Normalize(direction) * 10, ForceMode.Impulse);
    }
    IEnumerator actionDelay(float time)
    {
        haveAction = true;
        yield return new WaitForSeconds(time);
        haveAction = false;
    }
    public void pistolHit(int demage)
    {
        this.health -= demage/10;
        if (this.health <= 0)
        {
            death();
        }
        else
        {
            healthImage.sizeDelta=new Vector2(health / 2, 5);
        }
    }

    private void death()
    {
        
    }
}
