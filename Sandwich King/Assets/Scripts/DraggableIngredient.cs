using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableIngredient : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform startParent;
    private bool isOverPlate;
    private GameObject table;

    void Start()
    {
        isOverPlate = false;
        table = GameObject.FindWithTag("Table");
        if (table == null)
        {
            Debug.LogError("Table not found! Make sure the table GameObject is tagged correctly.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 position = GetCurrentDragPosition();
        position.z = 0; // Ensure the ingredient stays on the same plane
        transform.position = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (table == null)
        {
            Debug.LogError("Table not found! Cannot add ingredient.");
            transform.position = startPosition;
            return;
        }

        if (isOverPlate)
        {
            table.GetComponent<FoodPrepTableController>().AddIngredient(gameObject);
            transform.position = startPosition;
            transform.SetParent(startParent);
            // Destroy(gameObject);            // Here for mobile testing, will remove it later.
        }
        else
        {
            // Return to the start position if not dropped on the plate
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
    }

    private Vector3 GetCurrentDragPosition()
    {
        Vector3 position;
#if UNITY_EDITOR || UNITY_STANDALONE
        // For mouse input
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IOS
        // For touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            position = Camera.main.ScreenToWorldPoint(touch.position);
        }
        else
        {
            position = transform.position;
        }
#else
        // Fallback position (should not be needed, but good to have)
        position = transform.position;
#endif
        return position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plate"))
        {
            isOverPlate = true;
            Debug.Log("Entered Plate Trigger Radius.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plate"))
        {
            isOverPlate = false;
            Debug.Log("Left Plate Trigger Radius.");
        }
    }
}
