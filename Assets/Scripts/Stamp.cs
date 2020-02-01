using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 targetPos;
    public bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("test");
            SetTargetPosition();
        }
        if (isMoving)
        {
            MoveObject();
        }
    }

    void SetTargetPosition()
    {
        Plane plane = new Plane(Vector3.down, transform.position);
        Ray ray = Camera.main.ScreenPointToRay((Input.mousePosition));
        float point = 0f;

        if (plane.Raycast(ray, out point))
        {
            targetPos = ray.GetPoint(point);

            isMoving = true;
        }
    }

    void MoveObject()
    {
        transform.LookAt(targetPos);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (transform.position == targetPos)
        {
            isMoving = false;
        }
        Debug.DrawLine(transform.position, targetPos, Color.red);
    }
}
