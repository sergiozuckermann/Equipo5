using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasee : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] float radius;
     
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        // Get the size of the collider and set the radius to half its size
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (checkRatiusFromPlayer()){
            chasePlayer();
        }
        Debug.Log("Distance: " + distance);
        
    }
    bool checkRatiusFromPlayer(){
        if(distance < radius){
            // print in console the distance between the player and the enemy
            
            return true;
        }
        return false;
    }
    void chasePlayer(){
        
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle );  
    }
}

   