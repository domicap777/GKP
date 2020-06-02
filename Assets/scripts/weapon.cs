using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float demage, range, reloadingTime, timeBetweenShots, weaponSpread;
    [SerializeField]
    private AudioClip enptyGunSound;
    [SerializeField]
    private GameObject bulletHole;
    [SerializeField]
    private Text text;
    [SerializeField]
    private int ammoClipSize;
    [SerializeField]
    private bool inOwned;

    public static float spread;
    int ammoClipt;
    bool isShot=false;
    bool isRealouding=false;
    bool canShot = true;
    AudioSource source;
    Animation animator;
    [SerializeField]
    private AudioClip shotSound;
    [SerializeField]
    private AudioClip reloadSound;
    [SerializeField]
    private int ammo;

    public float Demage { get => demage; set => demage = value; }
    public float Range { get => range; set => range = value; }
    public float ReloadingTime { get => reloadingTime; set => reloadingTime = value; }
    public float TimeBetweenShots1 { get => timeBetweenShots; set => timeBetweenShots = value; }
    public float WeaponSpread { get => weaponSpread; set => weaponSpread = value; }
    public AudioClip ShotSound { get => shotSound; set => shotSound = value; }
    public AudioClip ReloadSound { get => reloadSound; set => reloadSound = value; }
    public AudioClip EnptyGunSound { get => enptyGunSound; set => enptyGunSound = value; }
    public GameObject BulletHole { get => bulletHole; set => bulletHole = value; }
    public Text Text { get => text; set => text = value; }
    public int Ammo { get => ammo; set => ammo = value; }
    public int AmmoClipSize { get => ammoClipSize; set => ammoClipSize = value; }
    public bool InOwned { get => inOwned; set => inOwned = value; }

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
                    if (bulletHole != null)
                        Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.parent=hit.collider.gameObject.transform;
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
            if (reloadSound != null)
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

