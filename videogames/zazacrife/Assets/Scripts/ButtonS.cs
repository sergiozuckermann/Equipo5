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
