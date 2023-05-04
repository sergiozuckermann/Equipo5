//Code by Zaza Team
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public bool inventoryEnabled;
    public GameObject inventory;

    // In the update function we check if the I key is pressed. If it is, we set the inventoryEnabled variable to true activating the canvas. If it is not, we set it to false. 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
        }

        if (inventoryEnabled == true)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }
}
