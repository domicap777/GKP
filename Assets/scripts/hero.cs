using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class hero : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int health = 100;
    [SerializeField]
    int armor = 0;
    [SerializeField]
    GameObject healthBar;
    [SerializeField]
    GameObject armorBar;
    private RectTransform imageHealth;
    private RectTransform imageArmor;
    [SerializeField]
    public Text textHealth;
    [SerializeField]
    public Text textarmor;
    [SerializeField]
    public Text information;

    public static int amountOfKeys=0;
    public static float DistanceFromTarget;
    public static string TargetTag;

    public int Health { get => health; set => health = value; }
    public int Armor { get => armor; set => armor = value; }
    public static int AmountOfKeys { get => amountOfKeys; set => amountOfKeys = value; }

    void Awake()
    {
        imageHealth = healthBar.GetComponent<RectTransform>();
        imageArmor = armorBar.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            DistanceFromTarget = hit.distance;
            TargetTag = hit.collider.tag;
            //if(TargetTag=="Door"&&hit.distance)
            //{
            //    hit.collider.gameObject.SendMessage("OnOver");
            //}

        }
        //
        textHealth.text = health.ToString();
        textarmor.text = armor.ToString();
        imageHealth.sizeDelta = new Vector2(4 * health, 100);
        imageArmor.sizeDelta = new Vector2(4*armor,100);
    }

    void KeyPickUp()
    {
        StartCoroutine("PickUpKey");
    }
    IEnumerator PickUpKey()
    {
        information.text = "znalazłeś klucz do otwarcia drzwi do następnego lewelu";
        yield return new WaitForSeconds(2.0f);
        information.text = "";
    }
}
