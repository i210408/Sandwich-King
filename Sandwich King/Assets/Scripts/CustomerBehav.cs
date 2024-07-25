using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerBehav : MonoBehaviour
{
    public List<string> Order;//To store the three order requests
    public int Score=0;
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
    }
}
