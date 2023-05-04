//Code by Zaza Team
// Description: This script is used to play the button sound when the button is pressed.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonS : MonoBehaviour
{

    public AudioSource buttonSound;
    
    public void PlaySound(){
        buttonSound.Play();
    }
}
