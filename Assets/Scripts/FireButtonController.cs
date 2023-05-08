using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireButtonController : MonoBehaviour
{
    public delegate void ClickFire();
    public event ClickFire OnFireClicked;

    [SerializeField]
    private GameManager ProgrammManagerScript;

    public void Fire()
    {
        OnFireClicked?.Invoke(); 
    }
}
