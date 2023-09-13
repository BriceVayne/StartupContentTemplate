using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform m_RectTransform;
    private Image m_Image;
    private int lastCanvasIndex;

    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Image = GetComponent<Image>();
        lastCanvasIndex = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_Image.color = new Color(255, 255, 255, 170);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var mousePos = Display.RelativeMouseAt(Input.mousePosition);

        m_RectTransform.anchoredPosition += eventData.delta;
        transform.position = mousePos;

        if (lastCanvasIndex != mousePos.z)
        {
            //var displayService = FindAnyObjectByType<MultiDisplayService>();
            //if (displayService != null)
            //{
            //    transform.SetParent(displayService.Canvas[(int)mousePos.z].transform);
            //    lastCanvasIndex = (int)mousePos.z;
            //}
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_Image.color = new Color(255, 255, 255, 255);
    }
}
