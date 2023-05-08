using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameManager _gameManager;
    private bool _killed = false;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_killed && collision.gameObject.name == "Shell(Clone)")
        {
            _gameManager.Strikes += 1;           
            _killed = true;
        }
    }

}
