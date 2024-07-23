using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerBehav : MonoBehaviour
{
    public List<string> Order;//To store the three order requests
    public int Score=0;//The total obtainable score is out of 5 for each compoent the player gets correct
    //In the case that the, say, sauces, dont matter, all possible sauces will be in the AcceptableList,
    //so the player gets an fair score count for the ones that the hints dont necessarily hint at
    //Eg: Hints are "Spicy + No Meat + Healthy Bread", doesn't tell you anything abt the sauces or cheeses to use
    //therefore, any choice for those is fine
    public List<string> Acceptable;
public TextMeshPro Textbox;
    // Start is called before the first frame update
    void Start()
    {
        //Display hi, then upon a click, display order
        Textbox.text="Hello! I want: \n";
        for (int i = 0; i < Order.Count; i++)
        {
            Textbox.text += Order[i] + ", ";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Textbox.text= "Please make it quick";
        }
        //For now, if tap, delete, then spawn next, until Total is done. or maybe that code should be in the spawner instead? 
        //Maybe. Yes, i think so
    }
}
