using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform square;
    [SerializeField] private Transform mouseLight;

    private void Update()
    {
        Vector2 newPos = (square.position + mouseLight.position) * 0.5f;
        print(Mathf.Abs(newPos.x - square.position.x));
        print(Screen.width);
        if ((Mathf.Abs(newPos.x - square.position.x) > Screen.width * 0.9f) || (Mathf.Abs(newPos.y - square.position.y) > Screen.height * 0.9f))
            transform.position = new Vector3(square.position.x, square.position.y, transform.position.z);
        else
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
