using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyControler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject player;
    [SerializeField]
    Text text;
    hero hero;
    private void Start()
    {
        hero= player.GetComponent<hero>();
    }

    private void OnTriggerEnter(Collider other)
    {
        hero.AmountOfKeys++;
        hero.SendMessage("KeyPickUp",SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }
    
}
