using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("WeaponStats")]
    [SerializeField, Range(1, 32)]
    int _numOfShots = 1;
    [SerializeField, Range(0f, 360f)]
    float _spread = 0.5f;
    //rate of fire
    [SerializeField]
    float _rateOfFire = 2f;
    [SerializeField]
    GameObject _bulletPrefab;
    [SerializeField]
    Vector3 _shootingOffset = Vector3.forward * 1.5f + Vector3.up;
    float shotCooldown = 0;
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        GameManager.Player = this;
        OnDeath.AddListener(GameManager.EndGame);
    }

    Vector2 desiredVelocity = Vector2.zero;
    Vector2 currentVelocity = Vector2.zero;
    public float MaxVelocity = 7;
    public float HealthRegen = 5;

    //[Header("Speed")]
    //public float SpeedUp;

    //[Header("SlowDown")]
    //public float SlowDown;
    override protected void FixedUpdate()
    {
        Heal(HealthRegen * Time.fixedDeltaTime);
        float desiredSpeed = desiredVelocity.magnitude;
        float currentSpeed = currentVelocity.magnitude;
        currentVelocity = desiredVelocity;
        setVelocity(currentVelocity);
        Vector3 mousePointer = PlayerBrain.MousePointedPosition(GameManager.Instance.MainCamera);
        transform.LookAt(new Vector3(mousePointer.x,transform.position.y,mousePointer.z));
    }

    protected override void Update()
    {
        base.Update();
        UpdateCooldowns();
    }

    public override void MoveInDirection(Vector3 velocity)
    {
        Vector2 movementVector = new Vector2(velocity.x, velocity.z);
        if(movementVector.magnitude > 1f)
            movementVector.Normalize();
        desiredVelocity = movementVector * MaxVelocity;
    }
    void setVelocity(Vector2 velocity)
    {
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.y);
    }
    [ContextMenu("DO")]
    void DO()
    {
        Debug.Log("Do!");
    }

    public override void Attack(Vector3 direction)
    {
        if (direction != Vector3.zero && shotCooldown <= 0)
        {
            Vector3 shootingOffset = transform.right * _shootingOffset.x + transform.up * _shootingOffset.y + transform.forward * _shootingOffset.z;
            Debug.DrawLine(transform.position, transform.position + shootingOffset, Color.red, 1f);
            for (int i = 0; i < _numOfShots; i++)
                Instantiate(_bulletPrefab, transform.position + shootingOffset,
                    Quaternion.FromToRotation(Vector3.forward, direction.normalized) * Quaternion.Lerp(Quaternion.identity, Random.rotation, _spread / 360f));
            shotCooldown = 1 / _rateOfFire;
        }
    }

    void UpdateCooldowns()
    {
        if (shotCooldown > 0) { shotCooldown -= Time.deltaTime; }
        else if (shotCooldown < 0) { shotCooldown = 0; }
    }

    override public void GetDmg(float dmg)
    {
        GameManager.AddScore(-69);
        base.GetDmg(dmg);
    }
}
