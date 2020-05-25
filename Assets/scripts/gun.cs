using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class gun : MonoBehaviour
{
    // Start is called before the first frame update

    public float demage;
    public float range;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip enptyGunSound;
    public GameObject bulletHole;
    public Text text;
    public int ammo;
    public int ammoClipSize;
    int ammoClipt;
    bool isShot=false;
    static public bool isShoting=false;
    bool isRealouding=false;
    AudioSource source;
    Animation animator;
    
    void Awake()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animation>(); 
        ammoClipt = ammoClipSize;
        text.text = ammoClipt + "/" + (ammo);
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isShot = true;
            isShoting = true;
        }
        if (Input.GetKeyDown(KeyCode.R)&& isRealouding==false)
        {
            reload();
        }
    }
    void FixedUpdate()
    {
        Vector3 offset = Random.insideUnitCircle*crosshairspread.spread;
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter+offset );
        RaycastHit hit;
        if(isShot&& ammoClipt > 0&& isRealouding==false)
        { 
            if (Physics.Raycast(ray, out hit, range))
            {

                isShot = false;
                Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                hit.collider.gameObject.SendMessage("pistolHit", demage, SendMessageOptions.DontRequireReceiver);
            }
            crosshairspread.spread += crosshairspread.shotSpred/2;
            ammoClipt--;
            text.text = ammoClipt+"/"+(ammo);
            source.PlayOneShot(shotSound);
            animator.Play("gun");
        } else if(isShot&&ammo>0)
        {
            reload();
            isShot = false;
        }
        else if(isShot)
        {
            source.PlayOneShot(enptyGunSound);
            isShot = false;
        }
    }

    private void reload()
    {

           ammoClipt += ammo >= ammoClipSize ? ammoClipSize : ammo;
           ammo -= ammo >= ammoClipSize ? ammoClipSize : ammo;
           source.PlayOneShot(reloadSound);
        StartCoroutine("Reload");
        text.text = ammoClipt + "/" + (ammo);

    }
 IEnumerator Reload()
    {
        isRealouding = true;
        yield return new WaitForSeconds(2);
        isRealouding = false;
    }
}
