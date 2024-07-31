using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerBehav : MonoBehaviour
{
    public List<string> Order;//To store the three order requests
    public int Score=0;
    public List<string> Acceptable;
    public TextMeshProUGUI Textbox;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StateOrder());
    }

    IEnumerator StateOrder()
    {
        //Display hi, then upon a click, display order
        Textbox.text="Hello! I want: \n";
        for (int i = 0; i < Order.Count - 1; i++)
        {
            Textbox.text += Order[i] + ", ";
        }
        Textbox.text += Order[Order.Count - 1] + ".";

        yield return new WaitForSeconds(5f);

        Textbox.text= "Please make it quick";
    }

}
