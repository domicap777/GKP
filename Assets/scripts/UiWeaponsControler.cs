using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

/// <summary>
/// klasa odpowiedzialna za wyświetlanie posiadanych broni 
/// </summary>
public class UiWeaponsControler : MonoBehaviour
{

    void Start()
    {
        update();
    }
    public void update()
    {
        int x = 0;
        List<weapon> weapons = transform.parent.parent.Find("weapons").GetComponent<WeaponSwitch>().Weapons;
        int i = 0;
        foreach(weapon weapon in weapons)
        {
            if(weapon.InOwned)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2 (0,x);
                x += 100;
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
