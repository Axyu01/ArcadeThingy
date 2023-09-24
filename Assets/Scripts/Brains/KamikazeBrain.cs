using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeBrain : Brain
{
    [SerializeField]
    float explosionDistance = 5f;
    protected override void FixedUpdate()
    {
        controlledEntity.MoveTo(GameManager.Player.transform.position);
        base.FixedUpdate();
        if (Mathf.Abs((gameObject.transform.position - GameManager.Player.transform.position).magnitude) < explosionDistance)
            controlledEntity.Attack(new Vector3(Random.value, Random.value, Random.value));
    }
}
