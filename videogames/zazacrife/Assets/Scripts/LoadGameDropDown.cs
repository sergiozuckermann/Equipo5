using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LoadGameDropDown : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Dropdown myDropdown;

public void Start()
{
    GameObject dropdownGameObject = GameObject.FindGameObjectWithTag("dropDown");

    myDropdown = dropdownGameObject.GetComponent<TMP_Dropdown>();

    // Create a new list of Dropdown options
    List<string> options = new List<string>();
    options.Add("Option 1");
    options.Add("Option 2");
    options.Add("Option 3");

    // Clear existing options and add the new options to the Dropdown
    myDropdown.ClearOptions();
    myDropdown.AddOptions(options);

    // Set the initial value of the dropdown
    myDropdown.value = 0;
}


}


