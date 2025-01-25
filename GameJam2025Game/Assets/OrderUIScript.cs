using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Assuming you're using UI elements

public class OrderUIScript : MonoBehaviour
{
    public GameObject orderPrefab; // Prefab for the order UI element
    public Transform orderParent; // Parent object to hold all orders
    public float spacing = 10f; // Horizontal spacing between orders

    private List<GameObject> orders = new List<GameObject>(); // List to track all orders

    void Start()
    {
            // FOR TESTING PURPOSES ONLY -__- (im watching you)
        // StartCoroutine(AddOrdersEverySecond());
    }

    // Adds a new order to the UI and shifts existing orders to the right
    public void addOrderToUI()
    {
        // Shift all existing orders to the right
        foreach (GameObject order in orders)
        {
            RectTransform rect = order.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - spacing, rect.anchoredPosition.y);
        }

        // Create a new order
        GameObject newOrder = Instantiate(orderPrefab, orderParent);
        newOrder.SetActive(true);
        RectTransform newOrderRect = newOrder.GetComponent<RectTransform>();

        // Position the new order at the start
        newOrderRect.anchoredPosition = new Vector2(0, 0);

        // Add it to the list of orders
        orders.Add(newOrder);
    }

    // Removes the first order from the UI and shifts remaining orders to the left
    public void removeOrderFromUI()
    {
        if (orders.Count == 0) return;

        // Remove the first order
        GameObject firstOrder = orders[0];
        orders.RemoveAt(0);
        Destroy(firstOrder);

        // Shift remaining orders to the left
        foreach (GameObject order in orders)
        {
            RectTransform rect = order.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - spacing, rect.anchoredPosition.y);
        }
    }

    // -__-
    private IEnumerator AddOrdersEverySecond()
    {
        while (true)
        {
            addOrderToUI();
            yield return new WaitForSeconds(1f);
        }
    }
}
