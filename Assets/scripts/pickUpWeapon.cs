using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpWeapon : MonoBehaviour
{
    public int type;
    public int ammo;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
           Transform weapons = other.gameObject.transform.GetChild(0).Find("weapons");
            WeaponSwitch weaponSwitch = weapons.GetComponent<WeaponSwitch>();
            weapon pickedWeapon = weaponSwitch.weapons[type].GetComponent<weapon>();
            if (pickedWeapon.inOwned)
                pickedWeapon.ammo += ammo;
            else
                pickedWeapon.inOwned = true;
            Destroy(this.gameObject);
            //if ()
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
