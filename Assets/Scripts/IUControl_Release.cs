using UnityEngine;
using System.Collections;
public class IUControl_Release :IControl
{

#if UNITY_STANDALONE_WIN
    private float _v;
    private float _h;
    private Control_Button C_B;

    public IUControl_Release()
    {
        C_B = GameObject.Find("Backgr_and_Menu").GetComponent<Control_Button>();
        GameObject.Find("Pause").SetActive(false);
    }
#endif

    public int Vertical_Axis()
    {
#if UNITY_STANDALONE_WIN
        _v = Input.GetAxis("Vertical");
        if(_v>0)
        return 1;
        else if (_v < 0)
            return -1;
        return 0;
#endif
    }


    public int Horizontal_Axis()
    {
#if UNITY_STANDALONE_WIN
        _h = Input.GetAxis("Horizontal");
        if (_h > 0)
            return 1;
        else if (_h < 0)
            return -1;
        return 0;
#endif
    }


    public bool Get_Jump()
    {
#if UNITY_STANDALONE_WIN
        return 0 != Input.GetAxis("Jump");
#endif
    }
    public bool Get_Catch()
    {
#if UNITY_STANDALONE_WIN
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E);
#endif
    }
    public void Get_Pause()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            C_B.Click_Pause();
        }

#endif
    }
    public bool Pause_off()
    {
        return false;
    }
}
