using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterController : MonoBehaviour
{
    [SerializeField]
    private float shootingDelay = 0.2f;

    [SerializeField]
    private Transform bulletPrefab;

    [SerializeField]
    private Transform shootPoint;

    [SerializeField]
    private bool useRaycast;

    [SerializeField]
    private ParticleSystem muzzleFlashPrefab;

    [SerializeField]
    private ParticleSystem hitPrefab;

    [SerializeField]
    private AudioSource _bulletAudio;

    Camera _mainCamera;
    Animator _animator;
    bool _pullingTrigger;
    float _nextShootTime;

    void Awake()
    {
        _mainCamera = Camera.main;
        _animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        GunControls();
    }

    void FixedUpdate()
    {
        Vector3 direction = RotateTowardsMouse();
        GunFiring(direction);
    }

    private void GunControls()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _animator.SetLayerWeight(1, 1f);
            _pullingTrigger = true;
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _animator.SetLayerWeight(1, 0f);
            _pullingTrigger = false;
        }

        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            useRaycast = !useRaycast;
        }
    }

    private Vector3 RotateTowardsMouse()
    {
        Vector3 direction;

        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var raycastHit, Mathf.Infinity))
        {
            direction = raycastHit.point - transform.position;
        }
        else
        {
            direction = ray.GetPoint(50) - transform.position;
        }

        direction = direction.normalized;
        direction.y = 0;

        return direction;
    }

    private void GunFiring(Vector3 direction)
    {
        if (!direction.Equals(Vector3.zero) && CanShoot())
        {
            if (_pullingTrigger)
            {
                transform.forward = direction;
                if (useRaycast)
                {
                    ShootRaycast(direction);
                }
                else
                {
                    Shoot(direction);
                }
            }
        }
    }

    bool CanShoot()
    {
        return Time.time > _nextShootTime;
    }

    void Shoot(Vector3 direction)
    {
        
        Transform bulletTransform = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        ParticleSystem muzzleFlash = Instantiate(muzzleFlashPrefab, shootPoint.position, shootPoint.rotation);
        muzzleFlash.Play();
        muzzleFlash.transform.parent = shootPoint;
        _bulletAudio.PlayOneShot(_bulletAudio.clip);
        bulletTransform.GetComponent<Bullet>().Setup(direction);
        _nextShootTime = Time.time + shootingDelay;
    }

    void ShootRaycast(Vector3 direction)
    {
        ParticleSystem muzzleFlash = Instantiate(muzzleFlashPrefab, shootPoint.position, shootPoint.rotation);
        muzzleFlash.transform.parent = shootPoint;
        _nextShootTime = Time.time + shootingDelay;
        _bulletAudio.PlayOneShot(_bulletAudio.clip);
        if (Physics.Raycast(shootPoint.position, direction, out var raycastHit, 30f))
        {
            Instantiate(hitPrefab, raycastHit.point, transform.rotation);
        }
    }

}
