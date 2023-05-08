using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RotateButtonController : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private Image _image;

    public delegate void RotateClick();
    public event RotateClick OnRotationClicked;

    public void Rotate()
    {
        OnRotationClicked?.Invoke();

        if (_gameManager.Rotation)
        {
            _gameManager.Rotation = false;
            _image.color = Color.red;
        }
        else
        {
            _gameManager.Rotation = true;
            _image.color = Color.green;
        }
    }

}
