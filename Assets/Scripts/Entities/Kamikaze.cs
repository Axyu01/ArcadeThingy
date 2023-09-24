using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : Entity
{
    [SerializeField]
    GameObject explosionPrefab;
    override protected void Start()
    {
        base.Start();
    }
    protected override void FixedUpdate()
    {
        
    }
    public override void Attack(Vector3 direction)
    {
        Boom();
    }
    public void Boom()
    {
        Debug.Log("BOOM!!!!!");
        Instantiate(explosionPrefab,transform.position,Quaternion.identity);
        Kill();
    }
    
}
