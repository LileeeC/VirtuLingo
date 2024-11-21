﻿// ******------------------------------------------------------******
// BasicCameraControl.cs
// Really bad and basic camera movement
// meant to be used just for the demo purposes.
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
// ******------------------------------------------------------******
using UnityEngine;
using System.Collections;
namespace PW{
    [RequireComponent(typeof(Camera))]
    public class BasicCameraControl : MonoBehaviour{

        [Range(0.2f, 6f)]
        public float rotateSpeed = 2f;
        public float scrollSmooth = 2f;

        private void Update(){
            //Gets scroll wheel delta and zoom in out based on the cursor position
            float delta = Input.GetAxis("Mouse ScrollWheel");

            if (Mathf.Abs(delta) > Mathf.Epsilon){
                RaycastHit hit;
                Ray ray = this.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                Vector3 desiredPosition;

                if (Physics.Raycast(ray, out hit)){
                    desiredPosition = hit.point;
                }else{
                    desiredPosition = transform.localPosition + transform.forward*5f;
                }

                float curDir = Vector3.Distance(desiredPosition, transform.localPosition);
                Vector3 direction = Vector3.Normalize(desiredPosition - transform.localPosition) * (delta);
                transform.localPosition += direction.normalized * scrollSmooth * Time.deltaTime;
            }
        }
        private void LateUpdate(){
            //Gets right mouseButton rotates around the pivot
            Vector3 eulerRotation = transform.localRotation.eulerAngles;
            eulerRotation.z = 0f;

            if (Input.GetMouseButton(1)){
                float rot_x = Input.GetAxis("Mouse X");
                float rot_y = -Input.GetAxis("Mouse Y");

                eulerRotation.x += rot_y * rotateSpeed;
                eulerRotation.y += rot_x * rotateSpeed;
            }
            transform.localRotation = Quaternion.Euler(eulerRotation);
        }
    }

}
/*using UnityEngine;
using UnityEngine.InputSystem;

namespace PW{
    [RequireComponent(typeof(Camera))]
    public class BasicCameraControl : MonoBehaviour
    {
        [Range(0.2f, 6f)]
        public float rotateSpeed = 2f;

        public float scrollSmooth = 2f;

        // Input Actions for mouse movement and scroll wheel
        private InputAction scrollAction;
        private InputAction rotateAction;

        private void OnEnable(){
            // Create the input actions (bind them to the mouse)
            var playerControls = new InputActionMap("PlayerControls");

            scrollAction = playerControls.AddAction("MouseScroll", binding: "<Mouse>/scroll");
            rotateAction = playerControls.AddAction("MouseRotate", binding: "<Mouse>/position");

            // Enable the input actions
            scrollAction.Enable();
            rotateAction.Enable();
        }
        private void OnDisable(){
            // Disable the input actions when the script is disabled
            scrollAction.Disable();
            rotateAction.Disable();
        }

        private void Update(){
            // Handle mouse scroll input
            float delta = scrollAction.ReadValue<Vector2>().y;

            if (Mathf.Abs(delta) > Mathf.Epsilon){
                RaycastHit hit;
                Ray ray = this.transform.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
                Vector3 desiredPosition;

                if (Physics.Raycast(ray, out hit)){
                    desiredPosition = hit.point;
                }else{
                    desiredPosition = transform.localPosition + transform.forward * 5f;
                }

                float curDir = Vector3.Distance(desiredPosition, transform.localPosition);
                Vector3 direction = Vector3.Normalize(desiredPosition - transform.localPosition) * delta;

                transform.localPosition += direction.normalized * scrollSmooth * Time.deltaTime;
            }
        }

        private void LateUpdate(){
            // Handle mouse rotation (right mouse button)
            if (Mouse.current.rightButton.isPressed)
            {
                Vector2 mouseDelta = rotateAction.ReadValue<Vector2>();
                float rot_x = mouseDelta.x;
                float rot_y = -mouseDelta.y;

                Vector3 eulerRotation = transform.localRotation.eulerAngles;
                eulerRotation.z = 0f;

                eulerRotation.x += rot_y * rotateSpeed;
                eulerRotation.y += rot_x * rotateSpeed;

                transform.localRotation = Quaternion.Euler(eulerRotation);
            }
        }
    }
}*/
