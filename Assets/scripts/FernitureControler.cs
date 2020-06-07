using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// klasa odpowiedzialna za niszczenie przedmiotów 
/// </summary>
public class FernitureControler : MonoBehaviour
{
    // Start is called before the first frame update

    private int hp = 200;

    public void pistolHit(int demage)
    {
        hp -= demage;
        if(hp<=0)
        {
            Destroy(this.gameObject);
        }
    }

}
