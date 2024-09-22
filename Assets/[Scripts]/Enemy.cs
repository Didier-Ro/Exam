using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float enemySpeed;

    [Header("Target")]
    [SerializeField] private Transform target;
    [SerializeField] private float stoppingDistance;

    [Header("Shoot Config")]
    [SerializeField] private float fireCooldown;
    [SerializeField] private float detectionDistance;
    [SerializeField] private LayerMask playerMask;
    private float currentFireCooldown;

    private Rigidbody rigidBody;
    private NavMeshAgent agent;
    private ShootController shootController;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        shootController = GetComponent<ShootController>();

        agent.speed = enemySpeed;
        agent.stoppingDistance = stoppingDistance;
    }

    private void Start()
    {
        currentFireCooldown = fireCooldown;
    }

    private void Update()
    {
        FollowTarget();

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionDistance, playerMask))
        {
            currentFireCooldown -= Time.deltaTime;

            if (currentFireCooldown <= 0)
            {
                ShootPlayer();
                currentFireCooldown = fireCooldown;
            }
        }
    }

    private void FollowTarget()
    {
        agent.SetDestination(target.position);

        if (agent.remainingDistance <= stoppingDistance)
        {
            Vector3 targetPosition = target.position;

            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
    }

    private void ShootPlayer()
    {
        shootController.OnShoot();
    }
}