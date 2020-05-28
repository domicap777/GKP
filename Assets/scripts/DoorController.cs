using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private GameObject _player;
    [SerializeField] bool leftOpenDirection;//jak true to się otwierają prawo w lewo wzdłuż x
    public bool OpenDoors;
    public bool CloseDoors;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if (leftOpenDirection)
        {
            if (OpenDoors)
            {
                transform.position += new Vector3(2 * Time.deltaTime, 0, 0);
            }
            if (CloseDoors)
            {
                transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            if (OpenDoors)
            {
                transform.position += new Vector3(0,0,2 * Time.deltaTime);
            }
            if (CloseDoors)
            {
                transform.position -= new Vector3(0,0,2 * Time.deltaTime);
            }
        }
    }

    public void SetDoorAsOpen()
    {
        StartCoroutine(OpenDoor());

    }
    IEnumerator OpenDoor()
    {
        OpenDoors = true;
        yield return new WaitForSeconds(2.0f);
        OpenDoors = false;
    }
}