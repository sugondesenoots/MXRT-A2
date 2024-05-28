using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem; // Add this namespace for the new Input System

public class Collection : MonoBehaviour
{
    [SerializeField] private ArHitEvent hitEvent; // Reference to AR Hit Event
    [SerializeField] private GameObject crystalPrefab; // Prefab of the crystal to collect
    [SerializeField] private Camera arCamera; // Reference to the AR camera

    public Item item = new Item("Item Name", 1); // Item to be added to the inventory
    public Inventory _inventory; // Reference to the inventory

    private PlayerInputActions playerInputActions; // Input Actions asset
    private InputAction tapAction; // Input action for tap

    private void Awake()
    {
        // Initialize the input actions
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        // Get the tap action from the input actions asset
        tapAction = playerInputActions.Gameplay.Tap;
        tapAction.performed += OnTapPerformed;
        tapAction.Enable();
    }

    private void OnDisable()
    {
        // Disable the tap action when the script is disabled
        tapAction.performed -= OnTapPerformed;
        tapAction.Disable();
    }

    private void Start()
    {
        // Subscribe to the AR hit event
        hitEvent.eventRaised += OnTap;
    }

    private void OnTap(object sender, ARRaycastHit hits)
    {
        // Use the ARRaycastHit position for touchPosition
        Vector2 touchPosition = new Vector2(hits.pose.position.x, hits.pose.position.y);

        // Create a ray from the touch position
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        // Perform a raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has the "Crystal" tag
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Crystal"))
            {
                Debug.Log("Item collected!");

                // Add item to the inventory and destroy the collected object
                Inventory.instance.AddItem(item);
                Destroy(hit.collider.gameObject);
            }
        }
    }

    private void OnTapPerformed(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = context.ReadValue<Vector2>();
        Ray ray = arCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Crystal"))
            {
                Debug.Log("Item collected!");

                Inventory.instance.AddItem(item);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
