//Made by Zaza Team
// Description: Script for the enemy that chases the player and manager for the enemies state

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
    public GameObject player;
    public unit EnemyUnit;
    private GameObject enemy;
    public GameObject Enemy;
    private Vector3 initialPosition;
    private bool isChasing = false; // Flag to determine if the enemy is currently chasing the player

    //Start will initialize the animator component and find the player object using its tag
    private void Start()
    {
        animator = GetComponent<Animator>();
        
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object using its tag
        initialPosition = transform.position;
        EnemyUnit=Enemy.GetComponent<unit>();
        
        

    }

    //Update will check if the enemy is chasing the player and if it is, it will chase it
    void Update()
    {
        int enemyID=PlayerPrefs.GetInt("Dead");
        int Number=PlayerPrefs.GetInt("Number");
        
         if (enemyID == 1 && EnemyUnit.stats.number == Number)
         {
             Destroy(Enemy);
         }
        
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

        unit enemy = GetComponent<unit>();
        
    }


    // This function will move the enemy towards the player and change the animation
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

    // This function will move the enemy towards the initial position and change the animation
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

    // This function will load the battle scene when the enemy collides with the player and save the player position, stats, enemy name and enemy number
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the enemy collides with the player, load the battle scene
        if (collision.CompareTag("Player"))
        {
            Transform posicion= player.GetComponent<Transform>();
            Vector3 actual = posicion.position;

                   
            PlayerPrefs.SetFloat("x", actual.x);
            PlayerPrefs.SetFloat("y", actual.y);
    
            unit Shaggy = collision.gameObject.GetComponent<unit>();
            string savedShaggy=JsonUtility.ToJson(Shaggy.stats);
            PlayerPrefs.SetString("Shaggy", savedShaggy);

            unit stats = GetComponent<unit>();
            PlayerPrefs.SetInt("Enemy", stats.stats.index);
            PlayerPrefs.SetInt("Number", stats.stats.number);
            
            animator.SetInteger("Walk", 0);

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
