using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    private const float MOVESPEED = 5f;
    private Vector3 moveDir;
    private Rigidbody2D rgbd2d;

    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        rgbd2d.velocity = moveDir * MOVESPEED;
    }
}
