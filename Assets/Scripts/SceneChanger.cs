using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneChanger : MonoBehaviour
{

    [Header("UI")]
	public GameObject UI;
	public GameObject Pricel;

	[Header("Scripts")]
	public PortalGun PortalGun;
	public PauseMenu PauseMenu;
	public PlayerController PlayerController;
	public CameraController CameraController;
	public PlayerShots PlayerShots;
	public PlayerRay PlayerRay;
	public DropGun DropGun;

    [Header("Variables")]
	public int SceneId = 0;



	//Функция, запускающася в первый кадр
	void Start()
    {
		SceneStart();
		if (SceneManager.GetActiveScene().name == "Portals")
		{
			SceneId = 1;
		}
		else if (SceneManager.GetActiveScene().name == "Play")
        {
			SceneId = 0;
        }
	}


	//Функция открытия пауз меню
	public void ChangeScene()
	{

		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1f;
		UI.SetActive(false);
		Pricel.SetActive(true);
		PauseMenu.enabled = true;
		PlayerController.enabled = true;
		CameraController.enabled = true;
		PlayerRay.enabled = true;
		if (SceneId == 0)
        {
			PlayerShots.enabled = true;
			DropGun.enabled = true;
        }
		if (SceneId == 1)
		{
			PortalGun.enabled = true;
		}
	}

	public void SceneStart()
    {
		Cursor.lockState = CursorLockMode.Confined;
        UI.SetActive(true);
        Pricel.SetActive(false);
		PlayerController.enabled = false;
		CameraController.enabled = false;
		PlayerShots.enabled = false;
		PlayerRay.enabled =false;
		DropGun.enabled = false;
		if (SceneId == 0)
        {
			PortalGun.enabled = false;
        }
    }
}
