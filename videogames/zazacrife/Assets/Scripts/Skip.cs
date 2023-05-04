//Done by Zaza team
// Description: This script is used to skip a cutscene.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Skip : MonoBehaviour
{


    //This function is used to skip a cutscene.
    public void SkipScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
