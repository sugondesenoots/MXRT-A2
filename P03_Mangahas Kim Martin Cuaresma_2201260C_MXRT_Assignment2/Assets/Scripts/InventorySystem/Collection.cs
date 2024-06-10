using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class Collection : MonoBehaviour
{
    public Inventory inventory;
    private InputAction tapAction;
    private Camera arCamera; 

    private void OnEnable()
    {
        //Binds for input action for tap
        tapAction = new InputAction(binding: "<Mouse>/leftButton");
        tapAction.AddBinding("<Touchscreen>/primaryTouch/press");
        tapAction.performed += OnTap;
        tapAction.Enable();

        //Gets AR Camera from XROrigin
        arCamera = Camera.main;
        if (arCamera == null)
        {
            Debug.LogError("AR Camera not found. Make sure your AR setup has a Camera component.");
        }
    }

    private void OnDisable()
    {
        tapAction.Disable();
        tapAction.performed -= OnTap;
    }

    private void OnTap(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        Vector2 inputPosition;

        //Checks if the input is from a touch screen or a mouse (Testing using PC)
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else
        {
            inputPosition = Mouse.current.position.ReadValue();
        }

        //Performs a raycast from the AR camera to the input position
        if (Physics.Raycast(arCamera.ScreenPointToRay(inputPosition), out hit))
        {
            //Checks if the raycast hits a collider with tag "Crystal"
            if (hit.collider != null && hit.collider.CompareTag("Crystal"))
            {
                //Adds the item to the inventory by name and increments the count by 1
                inventory.AddItem("Crystal");
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
