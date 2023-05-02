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

        myDropdown.options.Clear();

    }


}


