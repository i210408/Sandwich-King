using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FoodPrepTableController : MonoBehaviour
{
    private GameObject plate;
    private List<Ingredient> plateIngredients;
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
                        if (plateIngredients[i].ingredientName == ing.ingredientName &&
                            plateIngredients[i].ingredientType == ing.ingredientType)
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    
                    // If the ingredient doesn't already exist, add it to the list
                    if (!alreadyExists)
                    {
                        plateIngredients.Add(ing);
                        Debug.Log(ing.ingredientName + " " + ing.ingredientType + " added to the plate");
                        
                        // Print updated plate contents
                        Debug.Log("Plate contents are: ");
                        for (int i = 0; i < plateIngredients.Count; i++)
                        {
                            Debug.Log(plateIngredients[i].ingredientName + " " + plateIngredients[i].ingredientType);
                        }
                    }
                    else
                    {
                        Debug.Log(ing.ingredientName + " " + ing.ingredientType + " already exists on the plate. Skipping...");
                    }
                }
                else
                {
                    // Handle other types of hits or do nothing
                    Debug.Log("Hit something but not an ingredient.");
                }
            }
        }
    }


    void Start()
    {
        plate = GameObject.FindWithTag("Plate");
        plateIngredients = new List<Ingredient>();
    }

    void Update()
    {
       SelectIngredients();
    }
}
