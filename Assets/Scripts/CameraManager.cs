using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform square;
    [SerializeField] private Transform mouseLight;
    private float moveSpeed = 0.1f; // 0 to 1
    private float minSizeView = 2.5f; // number of block player can see at minimum
    private bool cameraFollowSquare = false;
    private Vector3 position;

    private void Start()
    {
        position = transform.position;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
            cameraFollowSquare = !cameraFollowSquare;

        if (cameraFollowSquare)
            CameraFollowSquare();
        else
            CameraFollowBothChar();
    }

    private void CameraFollowSquare()
    {
        position = Vector3.Lerp(transform.position, new Vector3(square.position.x, square.position.y, transform.position.z), moveSpeed);
    }

    private void CameraFollowBothChar()
    {
        Vector2 newPos = (square.position + mouseLight.position) / 2;
        Vector2 screenSizeInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) - Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 minNewPos = newPos - (screenSizeInWorld / 2);
        Vector2 maxNewPos = newPos + (screenSizeInWorld / 2);

        if (square.position.x < minNewPos.x + minSizeView || square.position.x > maxNewPos.x - minSizeView || square.position.y < minNewPos.y + minSizeView || square.position.y > maxNewPos.y - minSizeView)
            CameraFollowSquare();
        else
            position = Vector3.Lerp(transform.position, new Vector3(newPos.x, newPos.y, transform.position.z), moveSpeed);
    }

    private void FixedUpdate()
    {
        transform.position = position;
    }
}
