using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class DoorController : MonoBehaviour
{

    private GameObject player;
    private CharacterController characterController;
    private NavMeshAgent agent;
    [SerializeField]
    bool leftOpenDirection;//jak true to się otwierają prawo w lewo wzdłuż x
    public bool OpenDoors;
    public bool CloseDoors;
    float distance;
    public Text text;
    [SerializeField]
    private bool opened = false;
    [SerializeField]
    private string textToDisplay;
    [SerializeField]
    bool closeAfterOpening = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterController = player.GetComponent<CharacterController>();
        agent = player.GetComponent<NavMeshAgent>();
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
        if (distance <= 3 && opened==false)
        {
            if (hero.AmountOfKeys > 0)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(OpenDoor());
                  /// hero.AmountOfKeys--;
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
            if(opened == false)
                text.text = "";
        }
    }
    void OnMouseExit()
    {
        if (opened == false)
            text.text = "";
    }
    IEnumerator OpenDoor()
    {
        opened = true;
        OpenDoors = true;
        characterController.enabled = false;
        yield return new WaitForSeconds(0.01f);
        text.text = textToDisplay; 
        yield return new WaitForSeconds(2.0f);
        agent.enabled = true;
        agent.SetDestination(this.transform.position + new Vector3(this.transform.rotation.y == 0 ? -2 : -6, 1, this.transform.rotation.y==0?2:-2));
        OpenDoors = false;
        yield return new WaitForSeconds(2.0f);
        if (closeAfterOpening)
        {
            CloseDoors = true;
            yield return new WaitForSeconds(2.0f);
            CloseDoors = false;
        }
        text.text = "";
        characterController.enabled = true;
        agent.enabled = false;
    }
}