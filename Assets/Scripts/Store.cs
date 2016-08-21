using UnityEngine;
using System.Collections;

static public class Store
{
    static public void Load_Next_Start()
    {
        PlayerPrefs.SetInt("Load_Start", 1);
    }
    static public void Dnt_Load_Next_Start()
    {
        PlayerPrefs.SetInt("Load_Start", 0);
    }
    static public int Is_Load_This_Start()
    {
        return PlayerPrefs.GetInt("Load_Start", 0);
    }
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

    static public void Save_Game_Play_Poin(float n_sc, float g_sc, int[] s_lvl)
    {

        PlayerPrefs.SetFloat("Now Score", n_sc);
        PlayerPrefs.SetFloat("General Score", g_sc);
        for(int i=0; i<4;i++)
        PlayerPrefs.SetInt("Skill "+ i+ " Level" , s_lvl[i]);
        PlayerPrefs.Save();
    }

    static public float Load_Now_Score_Game_Play_Poin()
    {
        return PlayerPrefs.GetFloat("Now Score", 0);
    }

    static public float Load_General_Score_Game_Play_Poin()
    {
        return PlayerPrefs.GetFloat("General Score", 0);
    }

    static public void Load_Level_Game_Play_Poin(int[] s_lvl)
    {
        for(int i=0; i<4;i++)
        s_lvl[i] = PlayerPrefs.GetInt("Skill " + i + " Level", 0);
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
