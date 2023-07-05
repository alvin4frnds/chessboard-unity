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

    public static bool isUppercase(string s) {
        return s.Equals(s.ToUpper());
    }

}
