using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// klasa odpowiedzialna za podniesienie klucza 
/// </summary>
public class KeyControler : MonoBehaviour
{
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
