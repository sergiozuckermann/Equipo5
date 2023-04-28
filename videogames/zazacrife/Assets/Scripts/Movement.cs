//Zazacrifice of shaggy Team

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

    // Start is called before the first frame update
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

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
    }
    
    void Update()
    {
        move.x=Input.GetAxis("Horizontal");
        move.y=Input.GetAxis("Vertical");
        transform.Translate(move * speed * Time.deltaTime);
       
       
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

