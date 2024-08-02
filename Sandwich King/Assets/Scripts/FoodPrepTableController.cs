using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class FoodPrepTableController : MonoBehaviour
{
    private GameObject plate;
    private List<string> plateIngredients;
    public List<string> customerOrder;
    private int ingredientLimit;
    private int correctGuess;
    private CustomerBehav customerBehaviour;
    public TextMeshProUGUI customerDialogue;
    public List<Sprite> finalSandwiches;
    public List<Sprite> customerEmotions;

    // Reference to the explosion prefab
    public GameObject explosionPrefab; // Drag your explosion prefab here

    // Reference to StarData
    private StarData starData;
    private string currentLevelName; // Name of the current level

    void Start()
    {
        plate = GameObject.FindWithTag("Plate");
        plateIngredients = new List<string>();
        ingredientLimit = customerOrder.Count;
        correctGuess = 0;
        customerBehaviour = GameObject.FindGameObjectWithTag("Customer").GetComponent<CustomerBehav>();
        if (customerBehaviour != null)
        {
            customerOrder = customerBehaviour.Order;
            ingredientLimit = customerOrder.Count;
        }

        // Initialize StarData
        starData = FindObjectOfType<StarData>();
        if (starData != null)
        {
            // Dynamically set the current level name
            currentLevelName = SceneManager.GetActiveScene().name;
            starData.SetCurrentLevelName(currentLevelName);
        }
        else
        {
            Debug.LogError("StarData not found.");
        }

        Debug.Log("Current level name set to: " + currentLevelName);
    }

    void Update()
    {
        SelectIngredients();
    }

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
                    StartCoroutine(Serve());
                }
                else if (hit.collider.CompareTag("Discard"))
                {
                    Discard();
                }
                else
                {
                    // Handle other types of hits or do nothing
                    Debug.Log("Hit an unknown collider.");
                }
            }
        }
    }

    IEnumerator Serve()
    {
        Debug.Log("Hit the serve button.");
        for (int i = 0; i < customerOrder.Count; i++)
        {
            if (plateIngredients.Contains(customerOrder[i]))
            {
                correctGuess++;
            }
        }

        // Trigger explosion effect on top of the plate
        if (explosionPrefab != null && plate != null)
        {
            Vector3 platePosition = plate.transform.position;
            Vector3 explosionPosition = platePosition; // Use plate's position
            explosionPosition.z = 0; // Ensure z is set to 0 for 2D

            // Instantiate the explosion prefab and make it play only once
            GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
            // Optionally, destroy the explosion after its duration if it doesn't destroy itself
            Destroy(explosion, 0.5f); // Adjust the duration to match the length of your explosion effect
        }
        else
        {
            Debug.LogWarning("Explosion Prefab or Plate is not set. Please assign the necessary references.");
        }

        // Wait for the explosion animation or effect to complete
        yield return new WaitForSeconds(0.5f); // Adjust this duration to match the length of your explosion effect

        // Continue with score calculation and updating the plate
        int score = (correctGuess * 100) / ingredientLimit;
        int stars = 0;
        if (score > 0) stars++;
        if (score > 34) stars++;
        if (score > 67) stars++;
        plateIngredients.Clear();
        RemoveCopies();
        UpdatePlateSprite(stars);

        Debug.Log("Sandwich served, you guessed " + correctGuess + " ingredients correctly, and you have " + stars + " number of stars, and " + score + "/100 points.");
        UpdateCustomer(stars);
        correctGuess = 0;

        yield return new WaitForSeconds(2f);

        // Set stars earned in StarData
        if (starData != null)
        {
            starData.SetStarsEarned(stars);
            starData.SetCurrentLevelName(currentLevelName); // Ensure current level name is set before transitioning
            Debug.Log("Current level name set to: " + currentLevelName);
        }
        else
        {
            Debug.LogError("StarData not found.");
        }

        // Load the results scene
        SceneManager.LoadScene("ResultsScene");
    }

    void Discard()
    {
        Debug.Log("Hit the discard button.");
        plateIngredients.Clear();
        RemoveCopies();
        Debug.Log("Ingredients on plate discarded. The plate is empty now.");
    }

    public bool AddIngredient(GameObject ingredient)
    {
        Ingredient ing = ingredient.GetComponent<Ingredient>();

        // Check if the ingredient is already in plateIngredients
        bool alreadyExists = plateIngredients.Contains(ing.ingredientName);

        // If the ingredient doesn't already exist, add it to the list
        if (ingredientLimit == plateIngredients.Count)
        {
            Debug.Log("Ingredient limit reached. You cannot add more ingredients.");
            return false;
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
            return true;
        }
        else
        {
            Debug.Log(ing.ingredientName + " " + ing.ingredientType + " already exists on the plate. Skipping...");
            return false;
        }
    }

    void UpdateCustomer(int stars = 0)
    {
        SpriteRenderer customer = GameObject.FindWithTag("Customer").GetComponent<SpriteRenderer>();
        if (customer == null)
        {
            Debug.LogError("The customer object not found or it does not have a sprite renderer.");
        }
        if (customerEmotions == null)
        {
            Debug.LogError("The customer emotion sprites have not been set properly.");
        }
        if (stars == 0)
        {
            customerDialogue.text = "THIS IS TERRIBLE! \nYou did not get a single ingredient right.";
            customer.sprite = customerEmotions[0];
        }
        else if (stars == 1)
        {
            customerDialogue.text = "This is not what I ordered. Most ingredients I requested are missing.";
            customer.sprite = customerEmotions[0];
        }
        else if (stars == 2)
        {
            customerDialogue.text = "I think you might have mixed up my order with someone else's but this looks alright.";
            customer.sprite = customerEmotions[1];
        }
        else if (stars == 3)
        {
            customerDialogue.text = "Thank you! \nThis is exactly what I wanted.";
            customer.sprite = customerEmotions[2];
        }
        else
        {
            Debug.LogError("Something went wrong while updating customer appearance and dialogue.");
        }
    }

    private void RemoveCopies()
    {
        GameObject[] copy = GameObject.FindGameObjectsWithTag("Copy");
        foreach (GameObject c in copy)
        {
            Destroy(c);
        }
    }

    private void UpdatePlateSprite(int stars)
    {
        SpriteRenderer plateRenderer = plate.GetComponent<SpriteRenderer>();
        if (finalSandwiches.Count != 3)
        {
            Debug.Log("The sandwich sprites have not been set properly.");
            return;
        }
        if (stars == 0 || stars == 1)
        {
            plateRenderer.sprite = finalSandwiches[0];
        }
        else if (stars == 2)
        {
            plateRenderer.sprite = finalSandwiches[1];
        }
        else if (stars == 3)
        {
            plateRenderer.sprite = finalSandwiches[2];
        }
        else
        {
            Debug.Log("Something went wrong with the stars.");
        }
    }
}
