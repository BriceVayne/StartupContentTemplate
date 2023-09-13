
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SDisplayCanvas
{
    public int Index;
    public Display Display;
    public CanvasDisplay AttachedCanvas;
}

public static class DisplayManager
{
    public static int DisplayUsed = 1;
    public static List<SDisplayCanvas> Canvas = new List<SDisplayCanvas>();
}
