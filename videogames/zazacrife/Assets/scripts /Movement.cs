//Zazacrifice of shaggy Team

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Variables for speed and horizontal/vertical axis limits are established

    [SerializeField] float speed;

    Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    //This function gets the player input and moves them on the determined axis.
    //Also it prohibits the player from crossing the game's borders 

    void Update()
    {
        move.x=Input.GetAxis("Horizontal");
        move.y=Input.GetAxis("Vertical");

       // Debug.Log("H motion: " +move.x);

        //en caso de querer poner limites en el mapa desactivar comentarios de esta seccion y escribir limites en unity. 
        //Limit the movement to a specific range of coordinates 
        /*
        if(transform.position.x < -limitX && move.x < 0){
            move.x = 0;
        } else if (transform.position.x > limitX && move.x > 0){
            move.x = 0;
        }

         if(transform.position.y < -limitY && move.y < 0){
            move.y = 0;
        } else if (transform.position.y > limitY && move.y > 0){
            move.y = 0;
        }
        */ 


        transform.Translate(move * speed * Time.deltaTime);


    }
}