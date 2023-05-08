using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer LineRendererComponent;
    [SerializeField] private GameObject Catapult;

    // Start is called before the first frame update
    void Start()
    {
        LineRendererComponent = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Catapult = GameObject.Find("Catapult(Clone)");
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100];

        LineRendererComponent.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;
            
            points[i] = origin + speed * time + Physics.gravity * time * time / 2;

            if (points[i].y < Catapult.gameObject.transform.position.y - 1)
            {
                LineRendererComponent.positionCount = i;
                break;
            }
        }
        LineRendererComponent.SetPositions(points);
    }
}
