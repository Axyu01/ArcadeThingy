using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    
    public float MAX_HEALTH = 100f;
    public float Health { get; protected set; }
    //[SerializeField]
    protected NavMeshAgent agent;
    //[SerializeField]
    protected Rigidbody rb;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Health = MAX_HEALTH;
        if(Health <= 0)
        {
            Health = 1;
        }
        if (agent == null)
            if (!TryGetComponent(out agent))
                Debug.LogWarning($"Couldnt find NavMeshAgent for Entity in {gameObject.name}!");
        if (rb == null)
            if (!TryGetComponent(out rb))
                Debug.LogError($"Couldnt find Rigidbody for Entity in {gameObject.name}!");
        GameManager.RegisterEntity(this);
        gameObject.SetActive(false);
    }
    [Header("Events")]
    public UnityEvent OnDeath;
    public UnityEvent OnDmg;

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
        if (dmg < 0)
            return;
        Health -= dmg;
        OnDmg.Invoke();
        if (Health <= 0f)
        {
            Health = 0f;
            OnDeath.Invoke();
            Kill();
        }
    }
    public virtual void Heal(float heal)
    {
        if (heal < 0)
            return;
        Health += heal;
        if(Health > MAX_HEALTH)
            Health = MAX_HEALTH;
    }

    public void Kill()
    {
        GameManager.UnregisterEntity(this);
        Destroy(gameObject);
    }

}
