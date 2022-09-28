using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SinglePlay()
    {

    }

    public void MultiPlay()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
