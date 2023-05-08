using UnityEngine;

public class ChooseButtonController : MonoBehaviour
{

    [SerializeField]
    private GameObject _scrollView;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _choosedObject;


    public void ChooseObject()
    {
        _gameManager.SetSpawnObject(_choosedObject);
        _gameManager.ChooseObject = true;
        _scrollView.SetActive(false);
    }
}
