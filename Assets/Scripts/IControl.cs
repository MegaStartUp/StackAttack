﻿using UnityEngine;
using System.Collections;

public interface IControl
{
    int Vertical_Axis();
    int Horizontal_Axis();
    bool Get_Jump();
    bool Get_Catch();
    void Get_Pause();
    bool Pause_off();
}
