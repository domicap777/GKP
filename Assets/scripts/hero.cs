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

    public int Health { get => health; set => health = value; }
    public int Armor { get => armor; set => armor = value; }

    void Awake()
    {
        imageHealth = healthBar.GetComponent<RectTransform>();
        imageArmor = armorBar.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        textHealth.text = health.ToString();
        textarmor.text = armor.ToString();
        imageHealth.sizeDelta = new Vector2(4 * health, 100);
        imageArmor.sizeDelta = new Vector2(4*armor,100);
    }
}
