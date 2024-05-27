using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using static UnityEditor.Progress;

public class Collection : MonoBehaviour
{
    [SerializeField] ArHitEvent hitEvent;
    [SerializeField] GameObject crystal; 

    public Item item = new Item("Item Name", 1); 
    

    public Inventory _inventory;

    private void Start()
    {
        hitEvent.eventRaised += OnTap;
    }

    void OnTap(object sender, ARRaycastHit hits)
    {
        print("Item collected!");
         
        Inventory.instance.AddItem(item);
        Destroy(crystal);
    }
}
