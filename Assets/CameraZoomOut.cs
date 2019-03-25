using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour {
    public Transform player1;
    public Transform player2;

    private const float FOV_MARGIN = 15.0f;

    private Vector3 middlePoint;
    private float distanceFromMiddlePoint;
    private float distanceBetweenPlayers;
    private float aspectRatio;

    void Start()
    {
        aspectRatio = Screen.width / Screen.height;
    }

    void Update()
    {
        // Find the middle point between players.
        middlePoint = player1.position + 0.5f * (player2.position - player1.position);

        // Position the camera in the center.
        //Vector3 newCameraPos = Camera.main.transform.position;
        //newCameraPos.x = middlePoint.x;
        //Camera.main.transform.position = newCameraPos;

        // Calculate the new FOV.
        distanceBetweenPlayers = (player2.position - player1.position).magnitude;
        distanceFromMiddlePoint = (Camera.main.transform.position - middlePoint).magnitude;
        Camera.main.fieldOfView = 2.0f * Mathf.Rad2Deg * Mathf.Atan((0.5f * distanceBetweenPlayers) / (distanceFromMiddlePoint * aspectRatio));
        if (Camera.main.fieldOfView < 52)
            Camera.main.fieldOfView = 52;

            // Add small margin so the players are not on the viewport border.
            Camera.main.fieldOfView += FOV_MARGIN;
    }
}
