using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Entity
{
    [Header("WeaponStats")]
    [SerializeField,Range(1,32)]
    int _numOfShots = 1;
    [SerializeField,Range(0f,360f)]
    float _spread = 0.5f;
    //rate of fire
    [SerializeField]
    float _rateOfFire = 2f;
    [SerializeField]
    GameObject _bulletPrefab;
    [SerializeField]
    Vector3 _shootingOffset = Vector3.forward*1.5f;
    float shotCooldown;
    override protected void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.green);
        UpdateCooldowns();
    }
    public override void Attack(Vector3 direction)
    {
        if (direction != Vector3.zero && shotCooldown <= 0)
        {
            Vector3 shootingOffset = transform.right * _shootingOffset.x + transform.up * _shootingOffset.y + transform.forward * _shootingOffset.z;
            Debug.DrawLine(transform.position,transform.position + shootingOffset,Color.red,1f);
            for (int i = 0; i < _numOfShots; i++)
                Instantiate(_bulletPrefab, transform.position + shootingOffset,
                    Quaternion.FromToRotation(Vector3.forward, direction.normalized) * Quaternion.Lerp(Quaternion.identity, Random.rotation, _spread/360f));
            shotCooldown = 1 / _rateOfFire;
        }
    }
    float? lastMoveTime = null;
    public override void MoveTo(Vector3 position)
    {
        if (position == transform.position)
        {
            agent.enabled = false;
            Vector3 playerToEntity = (gameObject.transform.position - GameManager.Player.transform.position);
            float fullToPlayerRotation = Vector3.Angle(transform.forward, -new Vector3(playerToEntity.x, 0f, playerToEntity.z).normalized);
            if (lastMoveTime != null)
                if (agent.angularSpeed * (Time.time - (float)lastMoveTime) > fullToPlayerRotation)
                    transform.rotation *= Quaternion.AngleAxis(fullToPlayerRotation, Vector3.up);
                else
                    transform.rotation *= Quaternion.AngleAxis(agent.angularSpeed * (float)(Time.time - lastMoveTime), Vector3.up);
            lastMoveTime = Time.time;
        }
        else
        {
            agent.enabled = true;
            lastMoveTime = null;
            base.MoveTo(position);
        }
    }
    void UpdateCooldowns()
    {
        if (shotCooldown > 0) { shotCooldown -= Time.deltaTime; }
        else if (shotCooldown < 0) { shotCooldown = 0; }
    }
}
