using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{
    public Rigidbody2D rbody;

    private Vector2 _mousePosition;
    private Vector2 _lookAtdirection;
    private Vector2 _moveDirection;
    private float _distanceBetween;

    void Start()
    {
        currentLife = maxLife;
    }

    void Update()
    {
        InputMovement();
    }

    void FixedUpdate()
    {
        LookAtMouse();
    }

    public void SetMousePosition()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void LookAtMouse()
    {
        SetMousePosition();

        _lookAtdirection = new Vector2(_mousePosition.x - transform.position.x, _mousePosition.y - transform.position.y);
        float _angle = Mathf.Atan2(_lookAtdirection.y, _lookAtdirection.x) * Mathf.Rad2Deg + 90;
        rbody.rotation = _angle;
    }

    public void InputMovement()
    {
        _distanceBetween = Vector2.Distance(_mousePosition, transform.position);

        if (Input.GetKey(KeyCode.W) && _distanceBetween >= 0.1f)
        {
            Movement();
        }
        else
        {
            currentSpeed -= acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        }
    }

    public void Movement()
    {
        _moveDirection = new Vector2(0f, -1f);

        if (_moveDirection.magnitude >= 0.1f)
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

            transform.Translate(_moveDirection * currentSpeed * Time.deltaTime);
        }
    }
}
