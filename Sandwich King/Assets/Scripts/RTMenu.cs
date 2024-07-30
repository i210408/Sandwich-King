using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class RTMenu : MonoBehaviour
{
    public Button returnButton; 

    void Start()
    {

        if (returnButton != null)
        {
      
            returnButton.onClick.AddListener(OnReturnButtonPressed);
        }
        else
        {
            Debug.LogError("Return Button is not assigned in the Inspector.");
        }
    }

    // Method to be called when the button is pressed
    void OnReturnButtonPressed()
    {
        Debug.Log("Return button has been pressed.");
   
        SceneManager.LoadScene("Menu"); 
    }
}
