using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultReseter : MonoBehaviour
{
    public delegate void ResetCatapult();
    public event ResetCatapult OnResetCataoult;


    private void OnCollisionEnter(Collision collision)
    {
        OnResetCataoult?.Invoke();
        //_gameManager.Recharging = false;
    }
}
