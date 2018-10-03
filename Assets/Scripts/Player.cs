using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float speed;

    private Vector3 _startMousePosition =  Vector3.zero;


	void Update ()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(1))
        {
            _startMousePosition = Input.mousePosition;
        }

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(1))
        {
            Vector3 offset = Input.mousePosition - _startMousePosition;
            _camera.transform.Rotate(new Vector3(-offset.y * Time.deltaTime * speed, 0, 0));

            Quaternion q = _camera.transform.localRotation;
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float newAngle = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
            newAngle = Mathf.Clamp(newAngle, -30, 30);
            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * newAngle);
            
            _camera.transform.localRotation = q;
            transform.Rotate(new Vector3(0, offset.x * Time.deltaTime * speed, 0));
        }
    }
}
