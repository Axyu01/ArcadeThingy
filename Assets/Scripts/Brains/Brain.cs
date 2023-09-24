using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    [SerializeField]
    protected Entity controlledEntity;

    protected virtual void Update()
    {

    }
    protected virtual void Start()
    {
        if(controlledEntity == null)
            if (!TryGetComponent<Entity>(out controlledEntity))
                Debug.LogError($"Couldnt find entity for Brain in {gameObject.name}!");
    }

    protected virtual void FixedUpdate()
    {
        
    }
}
