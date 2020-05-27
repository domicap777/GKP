using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class weapon : MonoBehaviour
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
    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            isShot = true;
        }
        if (Input.GetKeyDown(KeyCode.R)&& isRealouding==false)
        {
            StartCoroutine("Reload");
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
                    if (enptyGunSound != null)
                        Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    hit.collider.gameObject.SendMessage("pistolHit", demage, SendMessageOptions.DontRequireReceiver);
                }
                StartCoroutine("TimeBetweenShots");
                ammoClipt--;
                UpdateTextAmmo();
                if (shotSound!=null)
                source.PlayOneShot(shotSound);
                animator.Play(animator.clip.name);
            }
            else if(ammo > 0)
            {
                StartCoroutine("Reload");
            }
            else
            {
                StartCoroutine("TimeBetweenShots");
                if (enptyGunSound != null)
                    source.PlayOneShot(enptyGunSound);
            }            
        }
        spread = isShot?weaponSpread: 0;
        isShot = false;
    }

    void OnEnable()
    {
        canShot = true;
        isRealouding = false;
        UpdateTextAmmo();
    }

 IEnumerator TimeBetweenShots()
    {
        canShot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        canShot = true;
    }
 IEnumerator Reload()
    {
        if (ammoClipt != ammoClipSize)
        {
            source.PlayOneShot(reloadSound);
            isRealouding = true;
            yield return new WaitForSeconds(reloadingTime);
            if (ammo > ammoClipSize - ammoClipt)
            {
                ammo -= ammoClipSize - ammoClipt;
                ammoClipt = ammoClipSize;
            }
            else
            {
                ammoClipt += ammo;
                ammo = 0;
            }
            UpdateTextAmmo();
            isRealouding = false;
        }
    }
    void UpdateTextAmmo()
    {
        if (this.gameObject.name == "tanto")
            text.text = "";
        else
            text.text = ammoClipt + "/" + (ammo);
    }
}

