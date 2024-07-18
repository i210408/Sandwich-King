using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FoodPrepTableController : MonoBehaviour
{
    private GameObject plate;
    private List<string> plateIngredients;
    public List<string> customerOrder;
    private int ingredientLimit;
    private int correctGuess;

    void SelectIngredients()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the ray hits an object
            if (hit.collider != null)
            {
                // Check if the object has the tag "Ingredient"
                if (hit.collider.CompareTag("Ingredient"))
                {
                    GameObject ingredient = hit.collider.gameObject;
                    Ingredient ing = ingredient.GetComponent<Ingredient>();
                    
                    // Check if the ingredient is already in plateIngredients
                    bool alreadyExists = false;
                    for (int i = 0; i < plateIngredients.Count; i++)
                    {
                        if (plateIngredients[i] == ing.ingredientName)
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    
                    // If the ingredient doesn't already exist, add it to the list

                    if (ingredientLimit == plateIngredients.Count)
                    {
                        Debug.Log("Ingredient limit reached. You cannot add more ingredients.");
                    }   
                    else if (!alreadyExists)
                    {
                        plateIngredients.Add(ing.ingredientName);
                        Debug.Log(ing.ingredientName + " " + ing.ingredientType + " added to the plate");
                        
                        // Print updated plate contents
                        Debug.Log("Plate contents are: ");
                        for (int i = 0; i < plateIngredients.Count; i++)
                        {
                            Debug.Log(plateIngredients[i]);
                        }
                    }
                    else
                    {
                        Debug.Log(ing.ingredientName + " " + ing.ingredientType + " already exists on the plate. Skipping...");
                    }
                }
                else if (hit.collider.CompareTag("Serve"))
                {
                    Debug.Log("Hit the serve button.");
                    for (int i = 0; i < customerOrder.Count; i++)
                    {
                        if (plateIngredients.Contains(customerOrder[i]))
                        {
                            correctGuess++;
                        }
                    }
                    Debug.Log("Sandwich served, you guessed " + correctGuess + " ingredients correctly.");
                }
                else if (hit.collider.CompareTag("Discard"))
                {
                    Debug.Log("Hit the discard button.");
                    plateIngredients.Clear();
                    Debug.Log("Ingredients on plate discarded. The plate is empty now.");
                }
                else
                {
                    // Handle other types of hits or do nothing
                    Debug.Log("Hit an unknown collider.");
                }
            }
        }
    }

    void Start()
    {
        plate = GameObject.FindWithTag("Plate");
        plateIngredients = new List<string>();
        ingredientLimit = customerOrder.Count;
        correctGuess = 0;
    }

    void Update()
    {
       SelectIngredients();
    }
}
