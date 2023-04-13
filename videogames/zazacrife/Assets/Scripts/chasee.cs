using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chasee : MonoBehaviour
{
    [SerializeField] string Bosque_Combate; // Serialized field for the name of the battle scene to load
    [SerializeField] float speed;
    [SerializeField] float returnPositionX;
    [SerializeField] float returnPositionY;
    [SerializeField] float chaseRadius; // Serialized field for the radius within which the enemy will chase the player
    private Animator animator;
    private GameObject player;
    private Vector3 initialPosition;
    private bool isChasing = false; // Flag to determine if the enemy is currently chasing the player

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object using its tag
        initialPosition = transform.position;
    }

    void Update()
    {
        // If the enemy is not chasing the player and is within the chase radius, start chasing
        if (!isChasing && player != null && Vector2.Distance(transform.position, player.transform.position) <= chaseRadius)
        {
            isChasing = true;
        }

        // If the enemy is not chasing the player, return to the initial position
        if (!isChasing)
        {
            ReturnToPosition();
            return;
        }

        // Chase the player
        ChasePlayer();
    }

    void ChasePlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        animator.SetFloat("VelX", direction.x);
        animator.SetFloat("VelY", direction.y);

        if (direction.x == 0 && direction.y == 0)
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
        transform.position = Vector2.MoveTowards(transform.position, returnPosition, speed * Time.deltaTime);
        animator.SetFloat("VelX", direction.x);
        animator.SetFloat("VelY", direction.y);
        if (direction.x == 0 && direction.y == 0)
        {
            animator.SetInteger("Walk", 0);
        }
        else
        {
            animator.SetInteger("Walk", 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the enemy collides with the player, load the battle scene
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(Bosque_Combate);
        }
    }

    // Method to set the return position from console or another script
    public void SetReturnPosition(float x, float y)
    {
        returnPositionX = x;
        returnPositionY = y;
    }
}
