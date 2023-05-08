using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviour
{
    private const string SEL_TAG_NAME = "Selected", UNSEL_TAG_NAME = "Unselected";

    [SerializeField]
    private FireButtonController _fireButtonController;

    [SerializeField]
    private ARRaycastManager _ARRaycastManagerScript;
    List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    [SerializeField]
    private GameObject _markerPrefab;


    [SerializeField]
    private GameObject _winText;

    [SerializeField]
    private Camera ARCamera;
    [SerializeField]
    private Vector2 _raycastStart = new Vector2(Screen.width / 2, Screen.height / 2);

    private GameObject _spawnObject;
    private GameObject _selectedObj;

    private Vector2 _touchPos;

    private Quaternion _rotation;

    public bool ChooseObject = false;
    public bool Rotation;
    public bool Recharging;

    [SerializeField]
    private int _strikesToWin = 2;
    public int Strikes { get; set; }

    [SerializeField]
    private float _rotationSpeed = 0.1f;

    private List<GameObject> _objects = new List<GameObject>();

    void Start()
    {
        _winText.SetActive(false);
        _markerPrefab.SetActive(false);

        _fireButtonController.OnFireClicked += Fire;
    }

    private void Fire()
    {
        if (!Recharging)
        {
            foreach (var obj in _objects)
            { 
                var catapult = obj.GetComponent<Catapult>();
                catapult?.Fire();
            }


            Recharging = true;
        }
    }


    private void CheckGameOver()
    {
        if (Strikes > _strikesToWin)
        {
            _winText.SetActive(true);
        }
    }


    public void SetSpawnObject(GameObject obj)
    {
        _spawnObject = obj;
    }

    void Update()
    {
        if (ChooseObject)
        {
            ShowMarkerAndSetObject();
        }

        MoveObjectAndRotation();

        CheckGameOver();
    }

    private void ShowMarkerAndSetObject()
    {

        _ARRaycastManagerScript.Raycast(_raycastStart, _hits, TrackableType.Planes);

        // show marker
        if (_hits.Count > 0)
        {
            _markerPrefab.transform.position = _hits[0].pose.position;
            _markerPrefab.SetActive(true);
        }
   
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Instantiate(_spawnObject, _hits[0].pose.position, _spawnObject.transform.rotation);
            _objects.Add(_spawnObject);
            var catapult = _spawnObject.GetComponent<Catapult>();
            catapult?.Set(this);

            ChooseObject = false;
            _markerPrefab.SetActive(false);
        }
    }

    private void MoveObjectAndRotation()
    {
        if (Input.touchCount == 0)
            return;
        
        Touch touch = Input.GetTouch(0);
        _touchPos = touch.position;
            
        // Select object
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = ARCamera.ScreenPointToRay(touch.position);
            RaycastHit hitObject;

            if (Physics.Raycast(ray, out hitObject))
            {
                if (hitObject.collider.CompareTag(UNSEL_TAG_NAME))
                {
                    hitObject.collider.gameObject.tag = SEL_TAG_NAME;
                    _selectedObj = hitObject.collider.gameObject;
                }
            }
        }

        if (touch.phase == TouchPhase.Moved && Input.touchCount == 1 )
        {
            // Rotate object with one finger
            if (Rotation)
            {
                _rotation = Quaternion.Euler(0f, -touch.deltaPosition.x * _rotationSpeed, 0f);
                _selectedObj.transform.rotation = _rotation * _selectedObj.transform.rotation;
            }
            // Move Object
            else
            {
                _ARRaycastManagerScript.Raycast(_touchPos, _hits, TrackableType.Planes);
                _selectedObj.transform.position = _hits[0].pose.position;
            }
        }
        // Rotate object with 2 fingers
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.touches[0];
            Touch touch2 = Input.touches[1];

            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float DistanceBetweenTouches = Vector2.Distance(touch1.position, touch2.position);
                float prevDistanceBetweenTouches = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
                float Delta = DistanceBetweenTouches - prevDistanceBetweenTouches;

                if (Mathf.Abs(Delta) > 0)
                {
                    Delta *= 0.1f;
                }
                else
                {
                    DistanceBetweenTouches = Delta = 0;
                }
                _rotation = Quaternion.Euler(0f, -touch1.deltaPosition.x * Delta, 0f);
                _selectedObj.transform.rotation = _rotation * _selectedObj.transform.rotation;
            }

        }
        // Deselect object
        if (touch.phase == TouchPhase.Ended)
        {
            if (_selectedObj.CompareTag(SEL_TAG_NAME))
            {
                _selectedObj.tag = UNSEL_TAG_NAME;
            }
        }
        
    }
}
