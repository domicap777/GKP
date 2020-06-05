using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableIteam : MonoBehaviour
{
    [Header ("type_boost")]
    [SerializeField] string iteamType;
    [SerializeField] int number;
    hero player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<hero>();
    }
    public void PickUp()
    {
        // w zależności od nazwy wybieramy boost 
        switch (iteamType.ToLower())
        {
            case "health_boost":
                if (player.HealHero(number))
                    DisableIteam();
                break;
            case "armor_boost":
                if (player.UpgradeHeroArrmor(number))
                    DisableIteam();
                break;
            case "speed_boost":
                //TODO: wywołać funkcję do prędkości
                Debug.Log(iteamType);
                break;
            default:
                break;
        }

    }
    public void DisableIteam()
    {
        gameObject.GetComponent<Animator>().enabled = true;// włączamy animatora aby wywołać animacje otwrcia
        gameObject.GetComponent<BoxCollider>().enabled = false;// aby już nie wchodzić w kolizję z urzytym przedmiotem
    }

}
