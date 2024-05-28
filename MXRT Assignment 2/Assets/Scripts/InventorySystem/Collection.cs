using UnityEngine;
using UnityEngine.InputSystem;

public class Collection : MonoBehaviour
{
    public Inventory inventory;
    private InputAction tapAction;

    private void OnEnable()
    {
        tapAction = new InputAction(binding: "<Mouse>/leftButton");
        tapAction.performed += OnTap;
        tapAction.Enable();
    }

    private void OnDisable()
    {
        tapAction.Disable();
        tapAction.performed -= OnTap;
    }

    private void OnTap(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit))
        {
            if (hit.collider != null && hit.collider.CompareTag("Crystal"))
            {
                Debug.Log("Item collected!");
                inventory.AddItem(new Item("Crystal", inventory.items.Count)); 
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
