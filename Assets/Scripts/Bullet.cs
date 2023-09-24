using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float _initialVelocity = 10f;
    [SerializeField]
    float _timeToLive = 5f;
    [SerializeField]
    float _dmg = 5f;
    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = transform.forward * _initialVelocity;
            StartCoroutine(DelayedDestroy(_timeToLive));
        }
        else
            Destroy(gameObject);
    }
    IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(_timeToLive);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Entity entity = collision.collider.gameObject.GetComponentInParent<Entity>();
        if (entity != null)
        {
            entity.GetDmg(_dmg);
        }
        Destroy(gameObject);
    }
}
