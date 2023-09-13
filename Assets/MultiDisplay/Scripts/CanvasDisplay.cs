using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class CanvasDisplay : MonoBehaviour
{
    [Header("Header")]
    [SerializeField] private Button exitButton;
    [SerializeField] private Toggle homeToggle;
    [SerializeField] private Toggle accountToggle;
    [SerializeField] private Toggle settingsToggle;
    [Space]
    [Header("Body")]
    [SerializeField] private GameObject body;

    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        exitButton.onClick.AddListener(() => Application.Quit());
        //homeToggle.onValueChanged.AddListener();
        //accountToggle.onValueChanged.AddListener();
        //settingsToggle.onValueChanged.AddListener();
    }

    private void Update()
    {
        //Vector3 mousePos = Display.RelativeMouseAt(Input.mousePosition);
        //mouseText.text = $"Mouse Position [{mousePos.x},{mousePos.y}] on Screen {mousePos.z}";
    }

    public void InitializeCanvas(int displayIndex)
    {
        canvas.targetDisplay = displayIndex;
    }
}
