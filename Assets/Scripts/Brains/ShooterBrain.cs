using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterBrain : Brain
{
    [Header("Distances")]
    [SerializeField]
    float shootingDistance = 5f;
    [SerializeField]
    float runAwayDistance = 0f;
    [Header("Properties")]
    [SerializeField,Range(1f,10f)]
    float runAwayPathing = 5f;
    [SerializeField, Range(0f, 1f)]
    float accurancy = 0.85f;
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector3 playerToEntity = (gameObject.transform.position - GameManager.Player.transform.position);
        float entityToPlayerDistance = playerToEntity.magnitude;
        if (shootingDistance <= entityToPlayerDistance)
        {
            controlledEntity.MoveTo(GameManager.Player.transform.position);
        }
        else if (runAwayDistance < entityToPlayerDistance && entityToPlayerDistance < shootingDistance)
        {
            controlledEntity.MoveTo(transform.position);
            if (Mathf.Abs(Vector3.Angle(transform.forward, -playerToEntity)) < 360f * (1 - accurancy))
                controlledEntity.Attack(-playerToEntity);
        }
        else if(entityToPlayerDistance <= runAwayDistance)
        {
            controlledEntity.MoveTo(GameManager.Player.transform.position+playerToEntity * runAwayPathing);
        }
    }
}
