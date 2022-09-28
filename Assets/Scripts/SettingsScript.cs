using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsScript : MonoBehaviour
{
    [Header("UI")]
    public PauseMenu PauseMenu;
    public GameObject PauseMenuUI;
    public GameObject SettingsPanel;
    public GameObject GeneralPanel;
    public GameObject ControlPanel;

    [Header("Text")]
    public Text SensivityText;
    public Text AudioText;
    public Text ResText;
    public Text GeneralText;
    public Text ControlText;
    public Text ScoupeText;
    public Text ScoupeColorText;

    [Header("Dropdown")]
    public Dropdown Dropdown;
    public Dropdown ScoupColor;
    public Dropdown ScoupSize;

    [Header("Sliders")]
    public Slider SensivityMouse;
    public Slider AudioVolume;


    [Header("Variables")]
    public int res;
    public int ResolutionWidth;
    public int ResolutionHeidth;
    private int ChangedResW = 1280;
    private int ChangedResH = 800;
    private int size;
    private int col;

    [Header("Colors")]
    public Color ActiveColor;
    public Color NoActiveColor;
    public Color Red;
    public Color Green;
    public Color Purple;
    public Color white;

    [Header("GameObjects")]
    public GameObject Player;
    public GameObject M4;
    public GameObject SaveGeneral;
    public CameraController CameraController;
    public GameObject BScoupe;
    public GameObject SScoupe;
    public GameObject MScoupe;
    public GameObject RScoupe;

    [Header("Audios")]
    public AudioSource KalashAudio;
    public AudioSource M4Audio;

    [Header("Scripts")]
    public Portal Portal;

    //Функция, запускающаяся в первый кадр
    void Start()
    {
        SensivityText.text = ((int)SensivityMouse.value).ToString();
        AudioText.text = (AudioVolume.value/10).ToString();
        ScoupColor.value = 2;
        KalashAudio = Player.GetComponent<AudioSource>();
        M4Audio = M4.GetComponent<AudioSource>();
    }


    //Функция, запускающаяся каждый кадр
    void Update()
    {
        if (SettingsPanel.gameObject.activeInHierarchy)
        {
            SensivityText.text = ((int)SensivityMouse.value).ToString();
            AudioText.text = (AudioVolume.value / 10).ToString();

            Change();

            if (res ==0)
            {
                ResText.text = ("1280x800");
                ResolutionWidth = 1280;
                ResolutionHeidth = 800;
            }
            else if (res == 1)
            {
                ResText.text = ("1280x1024");
                ResolutionWidth = 1280;
                ResolutionHeidth = 1024;
            }
            else if (res == 2)
            {
                ResText.text = ("1600x900");
                ResolutionWidth = 1600;
                ResolutionHeidth = 900;
            }
            else if (res == 3)
            {
                ResText.text = ("1920x1080");
                ResolutionWidth = 1920;
                ResolutionHeidth = 1080;
            }
        }
    }
    

    //Функция сохранения настроек
    public void SaveChanges()
    {
        CameraController.sensitivityMouse = SensivityMouse.value;
        KalashAudio.volume = AudioVolume.value/10;
        M4Audio.volume = AudioVolume.value / 10;
        Screen.SetResolution(ResolutionWidth, ResolutionHeidth, true);
        ChangedResW = ResolutionWidth;
        ChangedResH = ResolutionHeidth;
        ChangeSize();
        ChangeColor();
        Portal.GetTexture();
    }


    //Функция закрытия настроек
    public void Close()
    {
        SettingsPanel.SetActive(false);
        PauseMenuUI.SetActive(true);
        SensivityMouse.value = CameraController.sensitivityMouse;
        AudioVolume.value = (Player.GetComponent<AudioSource>().volume) * 10;

        if (ChangedResW == 1280 && ChangedResH == 800)
        {
            Dropdown.value = 0;
        }
        
        else if (ChangedResW == 1280 && ChangedResH == 1024)
        {
            Dropdown.value = 1;
        }

        else if (ChangedResW == 1600 && ChangedResH == 900)
        {
            Dropdown.value = 2;
        }

        else if (ChangedResW == 1920 && ChangedResH == 1080)
        {
            Dropdown.value = 3;
        }

        ResText.text = ChangedResW.ToString() + "x" + ChangedResH.ToString();

        if (size==0)
        {
            ScoupSize.value = 0;
            ScoupeText.text = ("Маленький");
        }

        else if (size==1)
        {
            ScoupSize.value = 1;
            ScoupeText.text = ("Средний");
        }

        else if (size==2)
        {
            ScoupSize.value = 2;
            ScoupeText.text = ("Большой");
        }


        else if (size==3)
        {
            ScoupSize.value = 3;
            ScoupeText.text = ("Точка");
        }

        if (col ==0)
        {
            ScoupeColorText.text = ("Зеленый");
            ScoupColor.value = 0;
        }

        else if (col == 1)
        {
            ScoupeColorText.text = ("Красный");
            ScoupColor.value = 1;
        }

        else if (col == 2)
        {
            ScoupeColorText.text = ("Белый");
            ScoupColor.value = 2;
        }

        else if (col == 3)
        {
            ScoupeColorText.text = ("Фиолетовый");
            ScoupColor.value = 3;
        }

        PauseMenu.SettingsActive = false;
    }


    //Функция установки значения переменной res, зависящей от выбранного значения Dropdown
    public void Change()
    {
        if (Dropdown.value == 0)
        {
            res = 0;
        }
        else if (Dropdown.value == 1)
        {
            res = 1;
        }
        else if (Dropdown.value == 2)
        {
            res = 2;
        }
        else if (Dropdown.value == 3)
        {
            res = 3;
        }
    }


    //Функция открытия настроек
    public void Open()
    {
        SettingsPanel.SetActive(true);
        PauseMenuUI.SetActive(false);
        PauseMenu.SettingsActive = true;
        OpenGeneralSettings();
    }


    //Функция открытия настроек управления
    public void OpenControlSettings()
    {
        SensivityMouse.value = CameraController.sensitivityMouse;
        AudioVolume.value = (Player.GetComponent<AudioSource>().volume) * 10;

        if (ChangedResW == 1280 && ChangedResH == 800)
        {
            Dropdown.value = 0;
        }

        else if (ChangedResW == 1280 && ChangedResH == 1024)
        {
            Dropdown.value = 1;
        }

        else if (ChangedResW == 1600 && ChangedResH == 900)
        {
            Dropdown.value = 2;
        }

        else if (ChangedResW == 1920 && ChangedResH == 1080)
        {
            Dropdown.value = 3;
        }

        ResText.text = ChangedResW.ToString() + "x" + ChangedResH.ToString();

        GeneralPanel.SetActive(false);
        ControlPanel.SetActive(true);

        GeneralText.color = NoActiveColor;
        ControlText.color = ActiveColor;

        SaveGeneral.SetActive(false);
    }


    //Функция открытия общих настроек
    public void OpenGeneralSettings()
    {
        GeneralPanel.SetActive(true);
        ControlPanel.SetActive(false);

        GeneralText.color = ActiveColor;
        ControlText.color = NoActiveColor;

        SaveGeneral.SetActive(true);
    }


    //Функция установки значени размера прицела
    public void ChangeSize()
    {
        if (ScoupSize.value == 0)
        {
            SScoupe.SetActive(true);
            MScoupe.SetActive(false);
            BScoupe.SetActive(false);
            RScoupe.SetActive(false);
            size = 0;
        }

        else if (ScoupSize.value == 1)
        {
            SScoupe.SetActive(false);
            MScoupe.SetActive(true);
            BScoupe.SetActive(false);
            RScoupe.SetActive(false);
            size = 1;
        }

        else if (ScoupSize.value == 2)
        {
            SScoupe.SetActive(false);
            MScoupe.SetActive(false);
            BScoupe.SetActive(true);
            RScoupe.SetActive(false);
            size = 2;
        }

        else if (ScoupSize.value == 3)
        {
            SScoupe.SetActive(false);
            MScoupe.SetActive(false);
            BScoupe.SetActive(false);
            RScoupe.SetActive(true);
            size = 3;
        }
    }


    //Функция установки цвета прицела
    public void ChangeColor()
    {
        if (ScoupColor.value == 0)
        {
            SScoupe.GetComponent<Image>().color = Green;
            MScoupe.GetComponent<Image>().color = Green;
            BScoupe.GetComponent<Image>().color = Green;
            RScoupe.GetComponent<Image>().color = Green;
            col = 0;
        }

        else if (ScoupColor.value == 1)
        {
            SScoupe.GetComponent<Image>().color = Red;
            MScoupe.GetComponent<Image>().color = Red;
            BScoupe.GetComponent<Image>().color = Red;
            RScoupe.GetComponent<Image>().color = Red;
            col = 1;
        }

        else if (ScoupColor.value == 2)
        {

            SScoupe.GetComponent<Image>().color = white;
            MScoupe.GetComponent<Image>().color = white;
            BScoupe.GetComponent<Image>().color = white;
            RScoupe.GetComponent<Image>().color = white;
            col = 2;
        }
        
        else if (ScoupColor.value == 3)
        {
            SScoupe.GetComponent<Image>().color = Purple;
            MScoupe.GetComponent<Image>().color = Purple;
            BScoupe.GetComponent<Image>().color = Purple;
            RScoupe.GetComponent<Image>().color = Purple;
            col = 3;
        }
    }
}