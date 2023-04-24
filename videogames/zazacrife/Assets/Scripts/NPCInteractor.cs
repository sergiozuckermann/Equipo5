using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractor : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }
    }
}
