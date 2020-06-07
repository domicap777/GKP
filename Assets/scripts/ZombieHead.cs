using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    [Header ("colider type")]
    [SerializeField] private string bodyType;
    [SerializeField] GameObject _parent;

    private int dmgMultiply=3;
    public void pistolHit(int demage)
    {
        switch (bodyType.ToLower())
        {
            case "head":
                Debug.Log(bodyType+ demage * dmgMultiply);
                _parent.gameObject.GetComponent<ZombieController>().pistolHit(demage * dmgMultiply);
                break;
            case "chest":
                _parent.gameObject.GetComponent<ZombieController>().pistolHit(demage);
                Debug.Log(bodyType+ demage);
                break;
            case "leg":
                transform.parent.gameObject.GetComponent<ZombieController>().pistolHit(demage/ dmgMultiply);
                Debug.Log(bodyType + demage / dmgMultiply);
                break;
            default:
                break;

        }
    }
}
