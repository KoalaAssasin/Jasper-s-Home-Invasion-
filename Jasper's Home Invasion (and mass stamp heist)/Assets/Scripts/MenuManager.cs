using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayInstructions()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayLevelOne()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayLevelOneEnd()
    {
        SceneManager.LoadScene(3);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
