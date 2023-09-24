using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    protected const float MAX_HEALTH = 100f;
    public float Health { get; protected set; } = MAX_HEALTH;
    //[SerializeField]
    protected NavMeshAgent agent;
    //[SerializeField]
    protected Rigidbody rb;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (agent == null)
            if (!TryGetComponent(out agent))
                Debug.LogWarning($"Couldnt find NavMeshAgent for Entity in {gameObject.name}!");
        if (rb == null)
            if (!TryGetComponent(out rb))
                Debug.LogError($"Couldnt find Rigidbody for Entity in {gameObject.name}!");
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
    protected virtual void FixedUpdate()
    {
    
    }
    public virtual void MoveTo(Vector3 position)
    {
        if (position == transform.position)
            agent.isStopped = true;
        else
            agent.isStopped = false;

        agent.SetDestination(position);
    }
    public virtual void MoveInDirection(Vector3 direction)
    {
        rb.velocity = new Vector3(direction.x,rb.velocity.y,direction.z);
    }
    public virtual void Attack(Vector3 direction)
    {
        Debug.Log("Attack");
    }
    public virtual void GetDmg(float dmg)
    {
        Debug.Log($"Dmg {dmg}");
        if (dmg < 0)
            return;
        Health += dmg;
        if (Health < 0f)
            Health = 0f;
    }
    public virtual void Heal(float heal)
    {
        Debug.Log($"Heal {heal}");
        if (heal < 0)
            return;
        Health += heal;
        if(Health > MAX_HEALTH)
            Health = MAX_HEALTH;
    }

}
