using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{
    [Header("Rigidbody variable")]
    public Rigidbody2D rBodyPlayer;

    [Header("Shot variables")]
    public Transform[] shotPoints;
    public GameObject bulletPrefab;
    public float timeBtwShots;
    public float startTimeBtwShots;

    [Header("Movement variables")]
    public float currentSpeed;
    public float maxSpeed;
    public float acceleration;

    private Vector2 _mousePosition;
    private Vector2 _lookAtdirection;
    private Vector2 _moveDirection;
    private float _distanceBetween;

    void Start()
    {
        SetMaxLife();
    }

    void Update()
    {
        SetShipGraphics();
        Dead();
        InputMovement();
        Shooting();
        LookAtMouse();
    }

    public void SetMousePosition()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void LookAtMouse()
    {
        if (state != ShipState.DISABLED)
        {
            SetMousePosition();
            _lookAtdirection = new Vector2(_mousePosition.x - transform.position.x, _mousePosition.y - transform.position.y);
            float _angle = Mathf.Atan2(_lookAtdirection.y, _lookAtdirection.x) * Mathf.Rad2Deg + 90;
            rBodyPlayer.rotation = _angle;
        }
    }

    public void InputMovement()
    {
        _distanceBetween = Vector2.Distance(_mousePosition, transform.position);

        if (Input.GetKey(KeyCode.W) && _distanceBetween >= 0.5f && state != ShipState.DISABLED)
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

    public void Shooting()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetButtonDown("Fire1") && state != ShipState.DISABLED)
            {
                FrontalShoot();
                timeBtwShots = startTimeBtwShots;
            }
            else if (Input.GetButtonDown("Fire2") && state != ShipState.DISABLED)
            {
                SideShoot();
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void FrontalShoot()
    {
        Bullet _bullet = GameInstances.instance.poolSystemInstance.TryToGetBullet();
        _bullet.transform.position = shotPoints[0].position;
        _bullet.rBodyBullet.velocity = shotPoints[0].up * -1 * _bullet.speed;
        _bullet.currentShooter = transform;
    }
    public void SideShoot()
    {
        for (int index = 1; index != shotPoints.Length; index++)
        {
            Bullet _bullet = GameInstances.instance.poolSystemInstance.TryToGetBullet();
            _bullet.transform.position = shotPoints[index].position;
            _bullet.rBodyBullet.velocity = shotPoints[index].up * _bullet.speed;
            _bullet.currentShooter = transform;
        }
    }
}
