using System;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public struct STypeToCanvas
{
    public string Name;
    public EScreenDisplay EScreenDisplay;
    public CanvasDisplay CanvasDisplay;
}

public class MultiDisplayService : MonoBehaviour
{
    [SerializeField] private List<STypeToCanvas> typeToCanvas;
    [SerializeField] private CanvasDisplay canvasPrefab;

    private void Start()
    {
        MultiDisplayFunctions();
    }

    private void MultiDisplayFunctions()
    {
        var displays = Display.displays;

        for (int i = 0; i < DisplayManager.DisplayUsed; i++)
        {
            displays[i].Activate();

            var refPrefab = Instantiate(canvasPrefab);
            refPrefab.InitializeCanvas(i);

            SDisplayCanvas data = new SDisplayCanvas()
            {
                Index = i,
                Display = displays[i],
                AttachedCanvas = refPrefab
            };

            DisplayManager.Canvas.Add(data);

            //string infos = $"displays connected: {displays.Length}\n\n";
            //infos += DisplayInfoToString(displays[i], i);
            //var refPrefab = Instantiate(prefab);
            //refPrefab.InitializeCanvas(i, true, infos);

            //canvas.Add(refPrefab);

            //if(i == 0)
            //    Instantiate(dragPrefab, refPrefab.transform);
        }
    }

    private string DisplayInfoToString(Display display, int index)
    {
        Vector2 systRes = new Vector2(display.systemWidth, display.systemHeight);
        Vector2 renderRes = new Vector2(display.renderingWidth, display.renderingHeight);

        return $"Screen n°{index} \n System Resolution : {systRes} \n Render Resolution : {renderRes}\n";
    }
}
