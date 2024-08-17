using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers
{
    public static int snow = 3;
    public static int sand = 6;
    public static int wall = 7;
    public static int plank = 8;
    public static int crate = 9;

    public static int snowMask = 1 << snow;
    public static int sandMask = 1 << sand;
    public static int wallMask = 1 << wall;
    public static int plankMask = 1 << plank;
    public static int crateMask = 1 << crate;
}
