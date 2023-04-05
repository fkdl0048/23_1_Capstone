using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static void SetTimeScale(float timescale)
    {
        Time.timeScale = timescale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
