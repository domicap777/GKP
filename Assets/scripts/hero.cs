using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class hero : MonoBehaviour
{
    // Start is called before the first frame update
    public  int health = 100;
    public  int armor = 0;
    public GameObject healthBar;
    public GameObject armorBar;
    private RectTransform imageHealth;
    private RectTransform imageArmor;
    public Text textHealth;
    public Text textarmor;



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
        imageHealth.localPosition = new Vector3(100 + 2 * health, 0, 0);
        imageHealth.sizeDelta = new Vector2(4 * health, 100);
        imageArmor.localPosition = new Vector3(100 + 2 * armor, 0, 0);
        imageArmor.sizeDelta = new Vector2(4*armor,100);
    }
}
