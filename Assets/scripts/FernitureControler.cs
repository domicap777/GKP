using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
