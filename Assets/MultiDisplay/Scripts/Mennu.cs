using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mennu : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text maxText;
    [SerializeField] private TMP_Text currentText;
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        var maxDisplay = Display.displays.Length;

        slider.value = slider.minValue = 1;
        slider.maxValue = maxDisplay;

        maxText.text = maxDisplay.ToString();
        currentText.text = slider.value.ToString();

        startButton.onClick.AddListener(() =>
        {
            DisplayManager.DisplayUsed = (int)slider.value;
            SceneManager.LoadScene(1);
        });

        exitButton.onClick.AddListener(() => Application.Quit());
    }

    private void Start()
    {
        slider.onValueChanged.AddListener((value) => currentText.text = value.ToString());
    }
}
