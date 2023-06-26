using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHelpers
{

    public static Color fromHex(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

}
