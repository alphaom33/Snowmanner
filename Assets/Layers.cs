using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers
{
    public static int floor = 3;
    public static int wall = 7;
    public static int crate = 9;

    public static int floorMask = 1 << floor;
    public static int wallMask = 1 << wall;
    public static int crateMask = 1 << crate;
}
