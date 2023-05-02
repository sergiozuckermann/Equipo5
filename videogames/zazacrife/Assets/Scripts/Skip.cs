using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Skip : MonoBehaviour
{


    // Update is called once per frame
    public void SkipScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
