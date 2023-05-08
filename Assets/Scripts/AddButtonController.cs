using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AddButtonController : MonoBehaviour
{

    [SerializeField]
    private GameObject _view;

    public void Show()
    {
        _view.SetActive(true);
    }
}
