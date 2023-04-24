using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Changes the scene to the given name
    // Parameters:
    // String name is the name of the scene it should change to
    public void ChangeScene(string name){
        SceneManager.LoadScene(name);
    }

    // Exits the application
    public void Exit(){
        Application.Quit();
    }
}
