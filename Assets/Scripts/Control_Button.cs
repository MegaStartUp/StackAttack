using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Control_Button:MonoBehaviour
{
    private CsGlobals Global;
    private GameObject Settings;
    private GameObject Menu;
    void Awake()
    {
        Global = GameObject.Find("General_Processor").transform.GetComponent<CsGlobals>();
    }
    void Start()
    {
        Settings = GameObject.Find("Settings_p");
        Menu = GameObject.Find("Menu_p");
        Menu.SetActive(true);
        Settings.SetActive(false);
        gameObject.SetActive(Global.pause);

    }
    //This function using for button
    public void Click_Pause()
    {
        gameObject.SetActive(Global.pause = !Global.pause);
    }
    public void Click_Main_Menu()
    {

        Store.Save_Score(Global.score);
        Application.LoadLevel("Menu");
    }
    public void Click_Restart()
    {

        Store.Save_Score(Global.score);
        Application.LoadLevel("Game");
    }
    public void Click_Setting()
    {
        Settings.SetActive(true);
        Menu.SetActive(false);
    }
    public void Click_Back_to_Setting()
    {
        Settings.SetActive(false);
        Menu.SetActive(true);
    }
    public void Click_Exit()
    {
        Store.Save_Score(Global.score);
        Application.Quit();
    }
    public void Slider_Sound(float value)
    {
        if(Global!=null) Global.sound = value;
        Store.Change_Value_Sound(value);
    }
    public void Toggle_Mode(bool tg)
    {
        if (tg)
        {
            Store.Save_Mode("Nigth");
            Global.mode = "Nigth";
        }
        else
        {
            Store.Save_Mode("Day");
            Global.mode = "Day";
        }
    }
    public void Click_Lang_En()
    {
        Store.Save_Lang(Global.language = "English");
    }
    public void Click_Lang_Rus()
    {
        Store.Save_Lang(Global.language = "Russian");
    }
    public void Click_Reset_Stat()
    {
        Store.Reset_Score();
        Global.Overwrite_param();
    }
}
