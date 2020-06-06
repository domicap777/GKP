using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// klasa odpowiedzialna za podniesienie broni jeśli dana broń jest już w posiadanu zwiększana jest ilość amunicji 
/// </summary>
public class pickUpWeapon : MonoBehaviour
{
    [SerializeField]
    int type, ammo;

    public int Type { get => type; set => type = value; }
    public int Ammo { get => ammo; set => ammo = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           Transform weapons = other.gameObject.transform.GetChild(0).Find("weapons");
            WeaponSwitch weaponSwitch = weapons.GetComponent<WeaponSwitch>();
            weapon pickedWeapon = weaponSwitch.Weapons[type];
            if (pickedWeapon.InOwned)
            {
                pickedWeapon.Ammo += ammo;
                if (type==weaponSwitch.SelectedWeapon)
                {
                    weaponSwitch.switchWepon(type);
                }
            }
            else
            {
                weaponSwitch.setWeaponActive(type);
            }
            Destroy(this.gameObject);
        }
    }
}
