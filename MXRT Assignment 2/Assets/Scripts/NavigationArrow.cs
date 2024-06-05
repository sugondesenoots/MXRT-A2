using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class NavigationArrow : MonoBehaviour
{
    public Inventory inventory; 
    public GameObject arrow; 
    public Transform destination; 
    public XROrigin xrOrigin; 

    private GameObject arrowInstance;
    private Camera arCamera;

    private bool arrowActive = false;
    public float verticalOffset = 0.5f; 

    void Start()
    {
        if (arrow != null)
        {
            arrow.SetActive(false);
        }

        if (xrOrigin != null)
        {
            arCamera = xrOrigin.Camera;
        }
    }

    void LateUpdate()
    {
        if (!arrowActive)
        {
            if (ShouldActivateArrow())
            {
                ActivateArrow();
            }
        }
        else
        {
            //Updates arrow position and rotation when active
            UpdateArrowPosition();
        }
    }

    bool ShouldActivateArrow()
    {
        //Checks if the crystal count is 3
        return GetCrystalCount() == 3;
    }

    void ActivateArrow()
    {
        if (arrow != null && arCamera != null)
        {
            //Uses the AR camera's position and forward direction
            Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * 1;
            arrowInstance = Instantiate(arrow, spawnPosition, Quaternion.identity);

            //Scales down the arrow model to fit in the scene
            arrowInstance.transform.localScale *= 0.25f;

            arrowActive = true;
            arrowInstance.SetActive(true);
        }
    }

    void UpdateArrowPosition()
    {
        if (arrowActive && arrowInstance != null && arCamera != null)
        {
            //Positions arrow in front of the AR Camera with an added vertical offset (This is so that the arrow's direction is easier to see)
            Vector3 newPosition = arCamera.transform.position + arCamera.transform.forward * 2;
            newPosition.y += verticalOffset;
            arrowInstance.transform.position = newPosition;

            //Makes the arrow point towards the destination
            Vector3 direction = destination.position - arrowInstance.transform.position; 

            direction.y = 0; //Keeps arrow horizontal
            arrowInstance.transform.rotation = Quaternion.LookRotation(direction);

            //Rotates the arrow to make it flat and point straight
            arrowInstance.transform.Rotate(90, 45, 0); 
        }
    }
     
    //Checks for the crystal count
    int GetCrystalCount()
    {
        int crystalCount = 0;

        foreach (Item item in inventory.items)
        {
            if (item.name == "Crystal")
            {
                crystalCount += item.count;
            }
        }

        return crystalCount;
    }
}
