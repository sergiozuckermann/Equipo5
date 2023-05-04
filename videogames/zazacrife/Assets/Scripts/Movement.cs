//Made by Zaza Team
// Description: This script is used to manage the main player movement and load the player's unit data.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IDataPersistance
{
     public GameObject Shaggy;
    

    //Variables for speed and horizontal/vertical axis limits are established
    [SerializeField] float speed;
    [SerializeField] float limit;
    Vector3 move;

    Animator animator;

    // Start is used to initialize the animator component, the unit data and position
    void Start()
    {
    
    animator = GetComponent<Animator>();
    
    unit ShaggyUnit=Shaggy.GetComponent<unit>(); 
    string save=PlayerPrefs.GetString("Shaggy");
    ShaggyUnit.stats = JsonUtility.FromJson<Stats>(save);


     Transform ShaggyTransform=Shaggy.GetComponent<Transform>();
     float posx=PlayerPrefs.GetFloat("x", 0f);
     float posy=PlayerPrefs.GetFloat("y", 0f);
     PlayerPrefs.Save();

     Vector3 actual;
     actual.x = posx;
     actual.y = posy;
     actual.z = 0;

    
     ShaggyTransform.position = actual;
    
    }

    // Update is called once per frame
    
    //This function gets the player input and moves them on the determined axis.
    //Also it prohibits the player from crossing the game's borders 

    //This function is used to load the player's position data.
    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    //This function is used to save the player's position data.
    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
    }
    
    //This function is used to manage the player's movement and walking animation.
    void Update()
    {
        move.x=Input.GetAxis("Horizontal");
        move.y=Input.GetAxis("Vertical");
        transform.Translate(move * speed * Time.deltaTime);

        
        animator.SetFloat("VelX", move.x);
        animator.SetFloat("VelY", move.y);

        if(move.x == 0 && move.y == 0)
        {
            animator.SetInteger("Walk", 0);
        }
        else
        {
            animator.SetInteger("Walk", 1);
        }
    }

    
}

