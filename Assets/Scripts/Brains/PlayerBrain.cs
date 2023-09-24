using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : Brain
{
    override protected void Update()
    {
        Vector2 velocity = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            velocity += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            velocity += Vector2.down;
        if (Input.GetKey(KeyCode.A))
            velocity += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            velocity += Vector2.right;
        if (Input.GetKey(KeyCode.LeftShift))
            controlledEntity.MoveInDirection(new Vector3(velocity.x, 0f, velocity.y));
        else 
            controlledEntity.MoveInDirection(new Vector3(velocity.x, 0f, velocity.y)*0.5f);
        if (Input.GetKey(KeyCode.Mouse0))
            controlledEntity.Attack(MousePointedPosition(GameManager.Instance.MainCamera) - transform.position);
    }
    protected override void FixedUpdate()
    {
        //removes default behaviour
    }
    public static Vector3 MousePointedPosition(Camera cam)
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var hitInfo);
        if (hitInfo.collider == null)
            return Vector3.zero;
        else
            return hitInfo.point;
    }
}
