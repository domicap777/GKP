using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public bool canMealeeAttack;
    public bool canShoot;
    public float meleeDemage;
    public float shootDemage;

    public void pistolHit(int demage)
    {
        Debug.Log("jd"+ demage);
    }

   
}
