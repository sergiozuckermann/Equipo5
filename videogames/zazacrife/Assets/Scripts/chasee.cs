using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasee : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] float radius;
    [SerializeField] float returnPositionX; // Serialized field for x position of return position
    [SerializeField] float returnPositionY; // Serialized field for y position of return position

    private float distance;
     [SerializeField]  Animator animator;

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (CheckRadiusFromPlayer())
        {
            ChasePlayer();
        }
        else
        {
            ReturnToPosition();
        }
    }

    bool CheckRadiusFromPlayer()
    {
        if (distance < radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        
        animator.SetFloat("VelX", direction.x);
        animator.SetFloat("VelY", direction.y);

        if(direction.x == 0 && direction.y == 0)
        {
            animator.SetInteger("Walk", 0);
        }
        else
        {
            animator.SetInteger("Walk", 1);
        }
    }

    void ReturnToPosition()
    {
        Vector2 returnPosition = new Vector2(returnPositionX, returnPositionY);
        Vector2 direction = returnPosition - (Vector2)transform.position;
        direction.Normalize();
        
        

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, returnPosition, speed * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        animator.SetFloat("VelX", direction.x);
        animator.SetFloat("VelY", direction.y);
        if(direction.x == 0 && direction.y == 0)
        {
            animator.SetInteger("Walk", 0);
        }
        else
        {
            animator.SetInteger("Walk", 1);
        }
    }

    // Method to set the return position from console or another script
    public void SetReturnPosition(float x, float y)
    {
        returnPositionX = x;
        returnPositionY = y;
    }

    

       
        

}