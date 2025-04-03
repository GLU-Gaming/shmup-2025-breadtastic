using Unity.VisualScripting;
using UnityEngine;

public class OpenControls : MonoBehaviour
{
    [SerializeField] private GameObject ControlsImage;

    private bool active = false;

    public void ControlsActive ()
    {
        ControlsImage.SetActive(true);
        active = true;
    }
    
    public void ControlsUnActive ()
    {
        ControlsImage.SetActive(false);
        active = false;
    }

    public void OnXButton()
    {
        if (active)
        {
            ControlsUnActive();
        }
        else
        {
            ControlsActive();
        }
    }
}
