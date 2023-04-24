using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintScript : MonoBehaviour
{
    FoxController fc;
    Image image;
    public string myName;

    // Instantiates extra variables and sets the image to automatically be invisible
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
        GameObject fox = GameObject.Find("Fox");

        fc =  fox.GetComponent<FoxController>();
    }

    // Updates the image to be visible depending on name and boolean in foxController
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
