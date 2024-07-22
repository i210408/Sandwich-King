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
                if (hit.collider.CompareTag("Serve"))
                {
                    Serve();
                    Destroy(hit.collider.gameObject);                   // Here for mobile testing, remove later.
                }
                else if (hit.collider.CompareTag("Discard"))
                {
                    Discard();
                    Destroy(hit.collider.gameObject);                   // Here for mobile testing, remove later.
                }
                else
                {
                    // Handle other types of hits or do nothing
                    Debug.Log("Hit an unknown collider.");
                }
            }
        }
    }

    void Serve()
    {
        Debug.Log("Hit the serve button.");
        for (int i = 0; i < customerOrder.Count; i++)
        {
            if (plateIngredients.Contains(customerOrder[i]))
            {
                correctGuess++;
            }
        }
        //Calculating score based on correct ingredients out of 100
        int score = (correctGuess * 100) / ingredientLimit;
        int stars = 0;
        //calculating stars
        if (score > 0)
        {
            stars++;
        }
        if (score > 34)
        {
            stars++;
        }
        if (score > 67)
        {
            stars++;
        }
        Debug.Log("Sandwich served, you guessed " + correctGuess + " ingredients correctly, and you have " + stars + " number of stars, and " + score + "/100 points.");
    }

    void Discard()
    {
        Debug.Log("Hit the discard button.");
        plateIngredients.Clear();
        Debug.Log("Ingredients on plate discarded. The plate is empty now.");
    }

    public void AddIngredient(GameObject ingredient)
    {
        Ingredient ing = ingredient.GetComponent<Ingredient>();

        // Check if the ingredient is already in plateIngredients
        bool alreadyExists = plateIngredients.Contains(ing.ingredientName);

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
            foreach (var item in plateIngredients)
            {
                Debug.Log(item);
            }
        }
        else
        {
            Debug.Log(ing.ingredientName + " " + ing.ingredientType + " already exists on the plate. Skipping...");
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
