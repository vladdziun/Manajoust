using UnityEngine;
using System.Collections;

public class PlayerLookAtMousePos : MonoBehaviour
{

    public Vector3 playerToMouse;
    float camRayLength = 100f;
    PlayerMovement3D pm;
  //  Rigidbody playerRigidbody;
    void Start()
    {
        //playerRigidbody = GetComponent<Rigidbody>();
        pm= GetComponentInParent<PlayerMovement3D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Aiming();
    }

    void Aiming()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.x = pm.playerToMouse.x;
            playerToMouse.z = pm.playerToMouse.z;


            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            // Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            // playerRigidbody.MoveRotation(newRotatation);
            transform.LookAt(playerToMouse);
        }

    }
}