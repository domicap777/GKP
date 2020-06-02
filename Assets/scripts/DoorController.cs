using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class DoorController : MonoBehaviour
{

    private GameObject _player;
    [SerializeField]
    bool leftOpenDirection;//jak true to się otwierają prawo w lewo wzdłuż x
    public bool OpenDoors;
    public bool CloseDoors;
    float distance;
    public Text text;
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
                transform.position += new Vector3(0, 0, 2 * Time.deltaTime);
            }
            if (CloseDoors)
            {
                transform.position -= new Vector3(0, 0, 2 * Time.deltaTime);
            }
        }
    }
    void OnMouseOver()
    {
        distance = hero.DistanceFromTarget;
        if (distance <= 3)
        {
            if (hero.AmountOfKeys > 0)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(OpenDoor());
                    hero.AmountOfKeys--;
                }
                text.text = "aby przejśc do następnego lewela naciśnij klawisz e";
            }
            else
            {
                text.text = "aby otworzyć drzwi musisz posiadać klucz ";
            }
        }
        else
        {
            text.text = "";
        }
    }
    void OnMouseExit()
    {
        text.text = "";
    }
    IEnumerator OpenDoor()
    {
        OpenDoors = true;
        yield return new WaitForSeconds(2.0f);
        OpenDoors = false;
    }
}