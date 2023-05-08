using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class Catapult : MonoBehaviour
{
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private Rigidbody _beamRB;
    [SerializeField]
    private CatapultReseter _reseter;
    [SerializeField]
    private float _force = 2.5f;


    private GameManager _gameManager;

    private void Start()
    {
        _reseter.OnResetCataoult += Reset;
    }

    public void Fire()
    {
        _beamRB.AddForce(_beamRB.transform.up * _force, ForceMode.Impulse);
        //_bullet.SetActive(false);
    }

    public void Set(GameManager manager)
    {
        _gameManager = manager;
    }

    public void Reset()
    {
        _gameManager.Recharging = false;
    }
}
