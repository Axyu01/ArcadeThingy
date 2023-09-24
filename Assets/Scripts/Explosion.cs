using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    UnityEvent onExplosionStart;
    [SerializeField]
    float explosionDuration;
    [SerializeField]
    float explosionRadius;
    [SerializeField]
    float explosionDmg;
    void Start()
    {
        onExplosionStart?.Invoke();
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, explosionRadius,LayerMask.GetMask("Entity"));
        foreach (Collider collider in colliders) 
        {
            Entity entity = collider.gameObject.GetComponentInParent<Entity>();
            if (entity != null)
            {
                entity.GetDmg(explosionDmg);
            }
        }
        StartCoroutine(DestroyAfter());
    }

    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(explosionDuration);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
