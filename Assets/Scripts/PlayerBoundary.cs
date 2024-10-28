using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float playerWidth;
    private float playerHeight;

    void Start()
    {
        mainCamera = Camera.main;

        playerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        playerHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    void Update()
    {
        KeepPlayerWithinBounds();
    }

    private void KeepPlayerWithinBounds()
    {
        Vector3 playerPos = transform.position;

        playerPos.x = Mathf.Clamp(playerPos.x, screenBounds.x * -1 + playerWidth, screenBounds.x - playerWidth);

        playerPos.y = Mathf.Clamp(playerPos.y, screenBounds.y * -1 + playerHeight, screenBounds.y - playerHeight);

        transform.position = playerPos;
    }
}
