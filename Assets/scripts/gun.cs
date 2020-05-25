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
    public float reloadingTime;
    public float timeBetweenShots;
    public float weaponSpread;
    public static float spread;
    public int ammoClipSize;
    int ammoClipt;
    bool isShot=false;
    bool isRealouding=false;
    bool canShot = true;
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
        if (Input.GetButton("Fire1"))
        {
            isShot = true;
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
        if(isShot &&  isRealouding==false && canShot )
        {
            if(ammoClipt > 0)
            {
                if (Physics.Raycast(ray, out hit, range))
                {
                    Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    hit.collider.gameObject.SendMessage("pistolHit", demage, SendMessageOptions.DontRequireReceiver);
                }
                StartCoroutine("TimeBetweenShots");
                ammoClipt--;
                text.text = ammoClipt + "/" + (ammo);
                source.PlayOneShot(shotSound);
                //animator.Play("gun");
               
            }
            else if(ammo > 0)
            {
                reload(); 
            }
            else
            {
                source.PlayOneShot(enptyGunSound);
            }            
        }
        spread = isShot?weaponSpread: 0;
        isShot = false;
    }

    private void reload()
    {
        if(ammoClipt!=ammoClipSize)
        {
            if(ammo>ammoClipSize-ammoClipt)
            {
                StartCoroutine("Reload");
                ammo -= ammoClipSize - ammoClipt;
                ammoClipt = ammoClipSize;
            }
            else
            {
                StartCoroutine("Reload");
                ammoClipt += ammo;
                ammo = 0;
            }
        }
    }
 IEnumerator TimeBetweenShots()
    {
        canShot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        canShot = true;
    }
 IEnumerator Reload()
    {
        source.PlayOneShot(reloadSound);
        isRealouding = true;
        yield return new WaitForSeconds(reloadingTime);
        text.text = ammoClipt + "/" + (ammo);
        isRealouding = false;
    }
}
