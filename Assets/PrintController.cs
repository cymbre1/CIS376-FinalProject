using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintController : MonoBehaviour
{
    FoxController fc;
    RawImage image;
    public string myName;

    // Start is called before the first frame update
    void Start()
    {
        print(myName);
        image = GetComponent<RawImage>();
        image.enabled = false;
        GameObject fox = GameObject.Find("Fox");

        fc =  fox.GetComponent<FoxController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myName == "cake") {
            image.enabled = fc.cake;
        } else if(myName == "egg") {
            image.enabled = fc.egg;
        } else if(myName == "carrot") {
            image.enabled = fc.carrot;
        } else if(myName == "apple") {
            image.enabled = fc.apple;
        } else if(myName == "onion") {
            image.enabled = fc.onion;
        } else if(myName == "garlic") {
            image.enabled = fc.garlic;
        } else if(myName == "banana") {
            image.enabled = fc.banana;
        } else if(myName == "ham") {
            image.enabled = fc.ham;
        } else if(myName == "pumpkin") {
            image.enabled = fc.pumpkin;
        } else {
            image.enabled = fc.tomato;
        }
    }
}
