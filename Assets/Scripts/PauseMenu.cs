using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class PauseMenu : MonoBehaviour
{
    [Header("Variables")]
    public bool GameIsPaused = false;
    public bool SettingsActive = false;
    public bool blur = false;
    public int SceneId = 0;

    [Header("GameObjects")]
    public GameObject PostProcess;
    public GameObject weapon;

    [Header("UI")]
    public GameObject SettingsPanel;
    public GameObject pauseMenuUI;
    public GameObject Pricel;

    [Header("Scripts")]
    public PortalGun PortalGun;

    [Header("PostProcessing")]
    private PostProcessVolume _PostProcessVolume;
    private DepthOfField _DepthOfField;


    //Функция, запускающаяся в первой кадре
    void Start()
    {
        _PostProcessVolume = PostProcess.GetComponent<PostProcessVolume>();
        _PostProcessVolume.profile.TryGetSettings(out _DepthOfField);
        if (SceneManager.GetActiveScene().name == "Portals")
        {
            SceneId = 1;
        }
    }


    //Функция, запускающаяся каждый кад
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused == false)
            {
                Pause();
                blur = !blur;
            }   


            else
            {
                if (!SettingsActive)
                {
                    Resume();
                    blur = !blur;  
                }
                else
                {
                    SettingsActive = false;
                    SettingsPanel.SetActive(false);
                    pauseMenuUI.SetActive(true);
                }
            }
        }
    }


    //Функция, которая ставит игру на паузу
    public void Pause() 
    { 
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Pricel.SetActive(false);
        _DepthOfField.active = !_DepthOfField.active;
        if (SceneId == 1)
        {
            PortalGun.enabled = false;
        }
        /*weapon.SetActive(false);*/
    }


    //Функция, которая убирает игру с паузы
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
        Time.timeScale = 1f;
        Pricel.SetActive(true);
        _DepthOfField.active = !_DepthOfField.active;
        if (SceneId ==1)
        {
            PortalGun.enabled = true;
        }
        /* weapon.SetActive(true);*/
    }


    //Функция выхода с игры
    public void QuitGame()
    {
        Application.Quit();
    }

}