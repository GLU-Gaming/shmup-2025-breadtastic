using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum OnButton
{
    On1Button,
    On2Button,
    On3Button
}

public class OpenControls : MonoBehaviour
{
    public bool ControlUIShow = false;

    public Vector2 move;

    public List<GameObject> ButtonList;

    [SerializeField] private GameObject ControlsImage;
    public GameObject ImageArrow;

    public OnButton state;

    public bool active = false;

    public Vector2 offset;

    public bool Button = false;

    private void Start()
    {
        state = OnButton.On1Button;
    }

    public void retry()
    {
        SceneManager.LoadScene("MainScene 2");
    }

    public void quit()
    {
        Application.Quit();
    }

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

    public void OnAButton(InputValue Value)
    {
        Button = true;
    }

    public void OnMoveUI(InputValue Value)
    {
        move = Value.Get<Vector2>();
    }
}
