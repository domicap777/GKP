using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> weapons;
    int previousWeapon = 0;
    int selectedWeapon = 0;
    void Start()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeapon)
                weapons[i].SetActive(true);
            else
                weapons[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //scrol zmianabroni
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            do
            {
                selectedWeapon = (selectedWeapon + 1) % weapons.Count;
            } while (weapons[selectedWeapon].GetComponent<weapon>().inOwned == false);
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            do
            {
                selectedWeapon = (((selectedWeapon - 1) + weapons.Count) % weapons.Count);
            } while (weapons[selectedWeapon].GetComponent<weapon>().inOwned == false);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedWeapon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons[1].GetComponent<weapon>().inOwned == true)
            selectedWeapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons[2].GetComponent<weapon>().inOwned == true)
            selectedWeapon = 2;
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons[3].GetComponent<weapon>().inOwned == true)
            selectedWeapon = 3;
        if (previousWeapon != selectedWeapon)
        {
            weapons[previousWeapon].SetActive(false);
            weapons[selectedWeapon].SetActive(true);
            previousWeapon = selectedWeapon;
        }
    }
}
