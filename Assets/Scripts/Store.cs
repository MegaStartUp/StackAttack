using UnityEngine;
using System.Collections;

static public class Store 
{
    static public void Save_Score(int sc)
    {
        if (sc > PlayerPrefs.GetInt("Top score", 0))
            PlayerPrefs.SetInt("Top score", sc);
        PlayerPrefs.SetInt("Last score", sc);
        PlayerPrefs.Save();
    }

    static public int Last_result()
    {
        return PlayerPrefs.GetInt("Last score", 0);
    }

    static public int Top_result()
    {
        return PlayerPrefs.GetInt("Top score", 0);
    }

    static public void Reset_Score()
    {

        PlayerPrefs.SetInt("Top score", 0);
        PlayerPrefs.SetInt("Last score", 0);
        PlayerPrefs.Save();
    }

    static public void Change_Value_Sound(float sound)
    {

        PlayerPrefs.SetFloat("Value Sound", sound);
        PlayerPrefs.Save();
    }

    static public float Value_Sound()
    {
        return PlayerPrefs.GetFloat("Value Sound", 0);
    }

    static public void Save_Mode(string md)
    {
        PlayerPrefs.SetString("Mode", md);
        PlayerPrefs.Save();
    }

    static public string Mode()
    {
        return PlayerPrefs.GetString("Mode", "Day");
    }

    static public void Save_Lang(string lg)
    {
        PlayerPrefs.SetString("Language", lg);
        PlayerPrefs.Save();
    }

    static public string Lang()
    {
        return PlayerPrefs.GetString("Language", "English");
    }
}
