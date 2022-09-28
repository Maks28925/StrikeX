using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Chat : MonoBehaviour
{
    [Header("UI")]
    public GameObject InputFied_obj;
    public InputField InputField;

    [Header("GameObjects")]
    public GameObject Player;
    public GameObject MainCamera;
    public GameObject SceneManagerObj;
    public GameObject ButtonManager;
    public GameObject UIPosition;
    public GameObject HelpUI;
    public GameObject InputChat;
    public GameObject Kalash;
    public GameObject M4;
    public Transform Weapon;
    public GameObject PlayerClone;
    public GameObject CameraClone;

    [Header("Scripts")]
    public PlayerShots PlayerShots;
    public PlayerController PlayerController;
    public SceneTransition SceneTransition;

    [Header("Texts")]
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5;
    public Text PlayerPositionText;

    [Header("Variables")]
    public int Index;
    public int CommandIndex;
    public string FirstParametr;
    public string SecondParametr;
    public string ThirdParametr;
    public string SlapParametr;
    public string WeaponParametr;
    bool Wep = false;
    public float X = 0;
    public float Y = 0;
    public float Z = 0;
    public bool UIPos;
    public float XPosition;
    public float YPosition;
    public float ZPosition;
    public int SceneId = 0;

    [Header("Colors")]
    public Color Yellow;
    public Color White;


    public void Start()
    {
        do
        {
            PlayerClone = GameObject.Find("Player(Clone)");
        }

        while (PlayerClone.GetComponent<PlayerController>().isMine);
        
        Player = PlayerClone;


        do
        {
            CameraClone = GameObject.Find("Main Camera");
        }

        while (CameraClone.GetComponent<CameraController>().isMine);
        
        MainCamera = CameraClone;
        

        if (SceneManager.GetActiveScene().name == "Portals")
        {
            SceneId = 1;
        }
        else if(SceneManager.GetActiveScene().name == "Play")
        {
            SceneId = 0;  
        }


    }

    private void Update()
    {
        GetInput();
    }


    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.F6) && !InputFied_obj.gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Player.GetComponent<PlayerController>().enabled = false;
            Player.GetComponent<PlayerShots>().enabled = false;
            Player.GetComponent<PlayerRay>().enabled = false;
            SceneManagerObj.GetComponent<SceneChanger>().enabled = false;
            ButtonManager.GetComponent<PauseMenu>().enabled = false;
            MainCamera.GetComponent<CameraController>().enabled = false;
            InputFied_obj.SetActive(true);
            InputField.ActivateInputField();
            Debug.Log("f6");
        }

        else if ((Input.GetKeyDown(KeyCode.F6) || Input.GetKeyDown(KeyCode.Escape)) && InputFied_obj.gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Player.GetComponent<PlayerController>().enabled = true;
            Player.GetComponent<PlayerShots>().enabled = true;
            Player.GetComponent<PlayerRay>().enabled = true;
            SceneManagerObj.GetComponent<SceneChanger>().enabled = true;
            ButtonManager.GetComponent<PauseMenu>().enabled = true;
            MainCamera.GetComponent<CameraController>().enabled = true;
            InputFied_obj.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && HelpUI.gameObject.activeInHierarchy)
        { 
            CloseHelp();
        }

        else if (Input.GetKeyDown(KeyCode.Return) && InputFied_obj.gameObject.activeInHierarchy)
        {
            text5.color = text4.color;
            text4.color = text3.color;
            text3.color = text2.color;
            text2.color = text1.color;
            text1.color = White;

            text5.text = text4.text;
            text4.text = text3.text;
            text3.text = text2.text;
            text2.text = text1.text;
            text1.text = InputField.text;
            InputField.text = " ";
            InputField.ActivateInputField();

            CheckCommand(text1.text);
        }

        if (UIPos)
        {
            GetPosition();
        }
    }
 

    private void CheckCommand(string command)
    {
        int i;

        CommandIndex = 0;

        if (command.IndexOf("/tp") == 0 && command.Length > 8)
        {
            if (command.Length > 8)
            {

                for (i = 4; command[i] == ' '  && CommandIndex + 1 < command.Length; i++)
                {
                    CommandIndex = i + 1;
                }
                CommandIndex = i + 1;

                if (command[i] == '-')
                {
                    FirstParametr += "-";
                    i++;
                }

                CommandIndex = i;
                if (command[CommandIndex] != ' ')
                {
                    FirstParametr += command[CommandIndex].ToString();
                }
                else
                {
                    FirstParametr = null; 
                }

                if (CommandIndex + 1 < command.Length)
                {
                    if (command[CommandIndex + 1] != ' ')
                    {
                        FirstParametr += command[CommandIndex + 1];
                        CommandIndex++;
                    }

                    CommandIndex = command.IndexOf(" ", CommandIndex + 1);
                    
                    for (i = CommandIndex + 1; command[i] == ' ' && CommandIndex + 1 < command.Length; i++)
                    {
                        CommandIndex = i + 1;
                    }

                    if (command[i] == '-')
                    {
                        SecondParametr += "-";
                        i++;
                    }

                    CommandIndex = i;
                    if (command[CommandIndex] != ' ')
                    {
                        SecondParametr += command[CommandIndex].ToString();
                    }
                    else
                    {
                        SecondParametr = null;
                    }

                    if (CommandIndex + 1 < command.Length)
                    {

                        if (command[CommandIndex + 1] != ' ')
                        {
                            SecondParametr += command[CommandIndex + 1];
                            CommandIndex++;
                        }

                        if (CommandIndex + 1 < command.Length)
                        {
                            CommandIndex = command.IndexOf(" ", CommandIndex + 1);
                            for (i = CommandIndex + 1; command[i] == ' ' && CommandIndex +1 < command.Length; i++)
                            {
                                CommandIndex = i + 1;
                            }

                            if (command[i] == '-')
                            {
                                ThirdParametr += "-";
                                i++;
                            }

                            CommandIndex = i;
                            if (command[CommandIndex] != ' ' && command[CommandIndex] != command.Length)
                            {
                                ThirdParametr += command[CommandIndex].ToString();
                            }
                            else
                            {
                                ThirdParametr = null;
                            }
                            CommandIndex++;

                            if (CommandIndex != command.Length && command[CommandIndex] != ' ')
                            {
                                ThirdParametr += command[CommandIndex].ToString();
                            }
                            


                          
                        }
                    }
                }
            }


            if (FirstParametr == null || SecondParametr == null || ThirdParametr == null)
            {
                Debug.Log("null");
                Debug.Log(FirstParametr);
                Debug.Log(SecondParametr);
                Debug.Log(ThirdParametr);
            }
            else
            {
                Debug.Log(FirstParametr + "1");
                Debug.Log(SecondParametr + "1");
                Debug.Log(ThirdParametr + "1");
                TP(FirstParametr, SecondParametr, ThirdParametr);
            }
            FirstParametr = null;
            SecondParametr = null;
            ThirdParametr = null;

        }

        else if (command.IndexOf("/slap") == 0 && command.Length>6)
        {

            for (i = 6; command[i] == ' ' && CommandIndex + 1 < command.Length; i++)
            {
                CommandIndex = i + 1;
            }
            CommandIndex = i + 1;

            if ( command[i] == '-')
            {
                SlapParametr += "-";
                i++;
            }

            SlapParametr += command[i];
            CommandIndex = i+1;

            if (CommandIndex < command.Length && command[CommandIndex]!= ' ')
            {
                SlapParametr += command[CommandIndex];
            }

            Debug.Log(CommandIndex);
            Debug.Log(SlapParametr);
            if (SlapParametr == null || SlapParametr == " ")
            {
                Debug.Log("NULL");
            }
            else
            {
                Slap(SlapParametr);
            }

            SlapParametr = null;
        }

        else if (command.IndexOf("/poson") == 0)
        {
            UIPosition.SetActive(true);
            UIPos = true;
            text1.color = Yellow;
        }

        else if (command.IndexOf("/posoff") == 0)
        {
            UIPosition.SetActive(false);
            UIPos = false;
            text1.color = Yellow;
        }

        else if (command.IndexOf("/giveammokalash") == 0)
        {
            GiveAmmo("Kalash");
        }
        
        else if (command.IndexOf("/giveammom4") == 0)
        {
            GiveAmmo("M4");
        }
        
        else if (command.IndexOf("/cheaton") == 0 || command.IndexOf("/cheatoff") == 0)
        {
            if (command.IndexOf("/cheaton") == 0)
            {
                SetCheat("On");
            }
            else
            { 
                SetCheat("Off");
            }
            text1.color = Yellow;
        }

        else if (command.IndexOf("/help") == 0)
        {
            Help();
            text1.color = Yellow;
        }

        else if (command.IndexOf("/gotoweapon") == 0 && command.Length > 12)
        {

            for (i = 12; command[i] == ' ' && CommandIndex + 1 < command.Length; i++)
            {
                CommandIndex = i + 1;
            }
            CommandIndex = i + 1;


            WeaponParametr += command[i];
            CommandIndex = i + 1;

            if (CommandIndex < command.Length && command[CommandIndex] != ' ')
            {
                WeaponParametr += command[CommandIndex];
            }

            if (WeaponParametr == null || WeaponParametr == " ")
            {
                Debug.Log("NULL");
            }
            else
            {
               GotoWeapon(WeaponParametr);
               text1.color = Yellow;
            }

            WeaponParametr = null;

        }

        else if (command.IndexOf("/portals") == 0)
            {
                text1.color = Yellow;
                Portals();
            }
    }

    private void Portals()
    {
        SceneTransition.SwitchToScene("Portals");
    }

    private void TP(string FirstParam, string SecondParam, string ThirdParam)
    {
        X = float.Parse(FirstParam);
        Y = float.Parse(SecondParam);
        Z = float.Parse(ThirdParam);
        Player.transform.position = new Vector3(X,Y,Z);
        text1.color = Yellow;
    }

    private void Slap(string SlapParametr)
    {
        float SlapParam = float.Parse(SlapParametr);
        Player.transform.localPosition += new Vector3(0f, SlapParam, 0f);
        text1.color = Yellow;
    }

    private void GetPosition()
    {
        string PlayerPos;
        XPosition = Player.transform.position.x;
        YPosition = Player.transform.position.y;
        ZPosition = Player.transform.position.z;
        PlayerPos = "(" + XPosition.ToString() + "; " + YPosition.ToString() + "; " + ZPosition.ToString() + ")";
        PlayerPositionText.text = PlayerPos;
    }

    private void GiveAmmo(string Gun)
    {
        if (Gun == "Kalash")
        {
            PlayerShots.KalashallPatrons += 100;
            PlayerShots.text2.text = PlayerShots.KalashallPatrons.ToString();
            text1.color = Yellow;
        }
        else if (Gun == "M4")
        {
            PlayerShots.M4allPatrons += 100;
            PlayerShots.text4.text = PlayerShots.M4allPatrons.ToString();
            text1.color = Yellow;
        }
    }

    private void SetCheat(string Cheat)
    {
        if (Cheat == "On")
        {
            Player.GetComponent<Rigidbody>().isKinematic = true;
            PlayerController.cheatOn = true;
        }
        else
        {
            Player.GetComponent<Rigidbody>().isKinematic = false;
            PlayerController.cheatOn = false;
        }
    }

    private void Help()
    {
        HelpUI.SetActive(true);
        InputChat.SetActive(false);
    }

    public void CloseHelp()
    {
        if (SceneId == 0)
        {
            Player.GetComponent<PlayerShots>().enabled = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Player.GetComponent<PlayerController>().enabled = true;
        Player.GetComponent<PlayerRay>().enabled = true;
        SceneManagerObj.GetComponent<SceneChanger>().enabled = true;
        ButtonManager.GetComponent<PauseMenu>().enabled = true;
        MainCamera.GetComponent<CameraController>().enabled = true;
        InputFied_obj.SetActive(false);
        HelpUI.SetActive(false);
    }

    public void GotoWeapon(string WeaponParam)
    {
        if (WeaponParam == "1")
        {
            Weapon = M4.transform;
            Wep = true; 
        }
        else if (WeaponParam == "0")
        {
            Weapon = Kalash.transform;
            Wep = true;
        }

        else
        {
            Wep = false;
        }

        

        if (Wep && Weapon.gameObject.activeInHierarchy && Weapon.transform.parent == null)
        {
            Player.transform.position = Weapon.transform.position + new Vector3(0, 0.2f, 0);
            text1.text = "GotoWeapon " + WeaponParametr;
        }
        else
        {
            text1.text = "Error parametr";
        }
        Weapon = null;
    }
}
