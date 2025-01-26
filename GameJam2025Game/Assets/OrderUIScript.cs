using System.Collections.Generic;
using UnityEngine;

public class OrderUIScript : MonoBehaviour
{
    public OrderItemUI orderPrefab; // Prefab for the order UI element
    public Transform orderParent; // Parent object to hold all orders
    public float spacing = 10f; // Horizontal spacing between orders

    private List<OrderItemUI> orders = new List<OrderItemUI>(); // List to track all orders

    void Start()
    {
            // FOR TESTING PURPOSES ONLY -__- (im watching you)
        // StartCoroutine(AddOrdersEverySecond());
    }

    public void DestroyuBaduOrderu(Item itemu)
    {
        var itemuz = orders.Find(o => o._item.CompareItem(itemu));
        orders.Remove(itemuz);
        Destroy(itemuz.gameObject);
    }

    public void CreateNewOrder(Item item)
    {
        addOrderToUI(item);
    }

    // Adds a new order to the UI and shifts existing orders to the right
    private void addOrderToUI(Item item)
    {
        // Shift all existing orders to the right
        foreach (OrderItemUI order in orders)
        {
            RectTransform rect = order.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - spacing, rect.anchoredPosition.y);
        }

        // Create a new order
        var newOrder = Instantiate(orderPrefab, orderParent);
        newOrder.SetupOrderItemUI(item);
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
        OrderItemUI firstOrder = orders[0];
        orders.RemoveAt(0);
        Destroy(firstOrder);

        // Shift remaining orders to the left
        foreach (OrderItemUI order in orders)
        {
            RectTransform rect = order.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - spacing, rect.anchoredPosition.y);
        }
    }

    //// -__-
    //private IEnumerator AddOrdersEverySecond()
    //{
    //    while (true)
    //    {
    //        addOrderToUI();
    //        yield return new WaitForSeconds(1f);
    //    }
    //}
}
