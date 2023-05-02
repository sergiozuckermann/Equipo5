using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadGameDropDown : MonoBehaviour
{
    // Start is called before the first frame update
    public Dropdown myDropdown;
    
    


    public void Start()
    {
        GameObject dropdownGameObject = GameObject.FindGameObjectWithTag("dropDown");

        myDropdown = dropdownGameObject.GetComponent<Dropdown>();

        // Create a new list of Dropdown options
        List<string> options = new List<string>();
        options.Add("Option 1");
        options.Add("Option 2");
        options.Add("Option 3");



        // Add the new options to the Dropdown
        myDropdown.AddOptions(options);

    }


}


