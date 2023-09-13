using UnityEngine;

public class ToggleType : MonoBehaviour
{
    [SerializeField] private EScreenDisplay eScreenDisplay;
    public EScreenDisplay ScreenDisplay { get { return eScreenDisplay; } }
}
