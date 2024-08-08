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
    GameObject serve;
    GameObject discard;
    void Start()
    {
        serve = GameObject.FindWithTag("Serve");
        discard = GameObject.FindWithTag("Discard");
        serve.SetActive(false);
        discard.SetActive(false);
        StateOrder();
    }

    void Update()
    {
        CheckTick();
    }


    void StateOrder()
    {
        Textbox.text="Hello! I want: \n";
        for (int i = 0; i < Order.Count - 1; i++)
        {
            Textbox.text += Order[i] + ", ";
        }
        Textbox.text += Order[Order.Count - 1] + ".";

    }

    void CheckTick()
    {
          if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Tick"))
                {
                    Textbox.text= "Please make it quick";
                    serve.SetActive(true);
                    discard.SetActive(true);
                    DraggableIngredient.EnableControls();
                    GameObject.FindWithTag("Tick").SetActive(false);    
                }
            }
        }
    }

}
