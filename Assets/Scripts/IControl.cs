using UnityEngine;
using System.Collections;

public interface IControl
{
    int Vertical_Axis();
    int Horizontal_Axis();
    bool Get_Jump();
    bool Get_Catch();
    bool Get_Skill();
    void Get_Pause();
    void Get_Skill_Sets();
    bool Pause_off();
}
