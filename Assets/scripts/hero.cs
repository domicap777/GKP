using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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


    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            DistanceFromTarget = hit.distance;
            TargetTag = hit.collider.tag;
        }
        textHealth.text = health.ToString();
        textarmor.text = armor.ToString();
        imageHealth.sizeDelta = new Vector2(4 * health, 100);
        imageArmor.sizeDelta = new Vector2(4*armor,100);
    }

    void KeyPickUp()
    {
        StartCoroutine("PickUpKey");
    }
    /// <summary>
    /// korutyna odpowiedzialna za informowanie gracza o podniesionym kluczu 
    /// </summary>
    /// <returns></returns>
    IEnumerator PickUpKey()
    {
        information.text = "znalazłeś klucz do otwarcia drzwi do następnego lewelu";
        yield return new WaitForSeconds(2.0f);
        information.text = "";
    }
    /// <summary>
    /// zadawanie obrażeń graczowi 
    /// </summary>
    /// <param name="demage"></param>
    public void HurtHero(int demage)
    {
        this.health -= demage;
        if(health<=0)
        {
            health = 0;
            SceneManager.LoadScene(0);
        }
    }
    /// <summary>
    /// leczenie bohatera
    /// </summary>
    /// <param name="number"></param>
    /// <returns>zwraca prawdę jeśli można podnieść pancerz bohaterowi</returns>
    public bool HealHero(int number)
    {
        if (this.Health == 100)
            return false;
        else
        {
            this.Health += number;
            if (this.Health > 100)
                Health = 100;
            return true;
        }
    }
    /// <summary>
    /// zwiększenie ilości pancerza bohaterowi
    /// </summary>
    /// <param name="number"></param>
    /// <returns>zwraca prawdę jeśli można uleczyć bohatera</returns>
    public bool UpgradeHeroArrmor(int number)
    {
        if (this.Armor == 100)
            return false;
        else
        {
            this.Armor += number;
            if (this.Armor > 100)
                this.Armor = 100;
            return true;
        }
    }
}
