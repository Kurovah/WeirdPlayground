using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float angleInc, walkSpeed, mouseX, mouseY, mouseSen,xRot,yRot;
    public Transform cam;
    Vector3 velocity, right, forward;
    CharacterController character;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        right = -Vector3.Cross(cam.forward, Vector3.up);
        forward = Vector3.Cross(right, Vector3.up);
        velocity = forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal");
        velocity = velocity.normalized * walkSpeed*Time.deltaTime;
        character.Move(velocity);
    }

    private void LateUpdate()
    {
        if(transform.rotation.eulerAngles.x != 0 || transform.rotation.eulerAngles.z != 0)
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSen;
        mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSen;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90, 90);
        yRot += mouseX;
        cam.localRotation = Quaternion.Euler(xRot, yRot, 0);
    }
}
