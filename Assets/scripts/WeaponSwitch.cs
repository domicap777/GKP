using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> weapons;
    public int initleWeapon;
    int selectedWeapon;
    void Start()
    {
        selectedWeapon = initleWeapon % weapons.Count;
        UpdateWeapon();
    }

    // Update is called once per frame
    void Update()
    {
       //scrol zmianabroni
       if(Input.GetAxis("Mouse ScrollWheel")>0)
            selectedWeapon = (selectedWeapon + 1) % weapons.Count;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            selectedWeapon = (((selectedWeapon - 1)+ weapons.Count) % weapons.Count); //wb ? 
        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedWeapon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count>1)
            selectedWeapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 2)
            selectedWeapon = 2;
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 3)
            selectedWeapon = 3;
        UpdateWeapon();

    }

    private void UpdateWeapon()
    {
        for(int i=0;i<weapons.Count;i++)
        {
            if(selectedWeapon==i)
            {
                weapons[i].SetActive(true);
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
    }
}
