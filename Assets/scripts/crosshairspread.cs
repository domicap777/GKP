using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class crosshairspread : MonoBehaviour
{
    [SerializeField]
    GameObject crosshair;
    GameObject topPart;
    GameObject bottomPart;
    GameObject leftPart;
    GameObject rightPart;
    const float walkSpred = 20;
    const float runSpred = 40;
    const float jumpSpred = 60;
    static public float spread = 0;
    private float targetSpread = 0;
    float startPosition = 30;

    // Start is called before the first frame update
    void Start()
    {
        topPart = crosshair.transform.Find("topPart").gameObject;
        bottomPart = crosshair.transform.Find("bottomPart").gameObject;
        leftPart = crosshair.transform.Find("leftPart").gameObject;
        rightPart = crosshair.transform.Find("rightPart").gameObject;
        topPart.GetComponent<RectTransform>().localPosition = new Vector3(0, startPosition , 0);
        bottomPart.GetComponent<RectTransform>().localPosition = new Vector3(0, -startPosition , 0);
        leftPart.GetComponent<RectTransform>().localPosition = new Vector3(startPosition , 0, 0);
        rightPart.GetComponent<RectTransform>().localPosition = new Vector3(-startPosition , 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        targetSpread = 0;
        targetSpread += FirstPersonController.isJumping ? jumpSpred : 0;
        targetSpread += FirstPersonController.isRuning ? runSpred : 0;
        targetSpread += FirstPersonController.isWalking ? walkSpred : 0;
        targetSpread += weapon.spread;

        if ( targetSpread >spread)
        {
            spread += 6;
        }
        else if(targetSpread<spread)
        {
            spread -= 2;
            if(targetSpread - spread == 1)  
                spread = targetSpread;
        }
        if (spread!=0)
        {
            
            topPart.GetComponent<RectTransform>().localPosition =new Vector3( 0,startPosition + spread,0);
            bottomPart.GetComponent<RectTransform>().localPosition =new Vector3( 0,-startPosition - spread,0);
            leftPart.GetComponent<RectTransform>().localPosition =new Vector3(startPosition + spread,0,0);
            rightPart.GetComponent<RectTransform>().localPosition =new Vector3(-startPosition - spread, 0,0);
        }
    }
}
