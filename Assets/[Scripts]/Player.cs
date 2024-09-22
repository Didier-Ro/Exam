using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(GameInput))]
[RequireComponent (typeof(ShootController))]
public class Player : MonoBehaviour
{
    [Header("Movement Configuration")]
    [SerializeField] private float playerSpeed = 10;
    [SerializeField] private float rotationSpeed = 10;
    private Vector2 inputVector;
    private Vector3 moveDir;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform grounCheck;
    [SerializeField] private float groundRadious = 0.5f;
    private bool isGrounded;

    [Header("Capsule Configuration")]
    [SerializeField] private Vector3 center = new Vector3(0 , 1f, 0);
    [SerializeField] private float radious = 0.5f;
    [SerializeField] private float height = 2f;

    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private ShootController shootController;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        shootController = GetComponent<ShootController>();

        rigidBody.freezeRotation = true;

        capsuleCollider.center = center;
        capsuleCollider.radius = radious;
        capsuleCollider.height = height;
    }

    private void Start()
    {
        GameInput.Instance.OnJumpAction += GameInput_OnJumpAction;
        GameInput.Instance.OnShootAction += GameInput_OnShootAction;
    }

    private void GameInput_OnShootAction(object sender, EventArgs e)
    {
        shootController.OnShoot();
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {
        Jump();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        HandleMovement();
        isGrounded = Physics.CheckSphere(grounCheck.position, groundRadious, layerMask);
    }

    void HandleMovement()
    {
        inputVector = GameInput.Instance.GetMovementVectorNormalize();
        moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        rigidBody.velocity = moveDir * playerSpeed;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    void Jump()
    {
        if (!isGrounded) return;
       
        rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(grounCheck.position, groundRadious);
    }
}