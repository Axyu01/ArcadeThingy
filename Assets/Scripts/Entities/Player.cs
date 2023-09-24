using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

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

    //[Header("Speed")]
    //public float SpeedUp;

    //[Header("SlowDown")]
    //public float SlowDown;
    override protected void FixedUpdate()
    {
        float desiredSpeed = desiredVelocity.magnitude;
        float currentSpeed = currentVelocity.magnitude;
        currentVelocity = desiredVelocity;
        setVelocity(currentVelocity);
    }

    bool TryGetLatestValueOccurenceTime(AnimationCurve curve, float value, float error, out float time)
    {
        time = 0f;
        return true;
    }
    bool isPrecise(float desiredValue, float currentValue, float error)
    {
        return (desiredValue - error <= currentValue && currentValue <= desiredValue + error);
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

    override public void GetDmg(float dmg)
    {
        GameManager.AddScore(-69);
        base.GetDmg(dmg);
    }
}
