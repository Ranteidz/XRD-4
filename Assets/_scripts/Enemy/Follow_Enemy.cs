using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Follow_Enemy : MonoBehaviour
{
    public enum States {Patrol,Follow, Attack}
    
    public NavMeshAgent agent;

    public Transform target;
    public Transform[] wayPoints;
    
    public States currentState;
    [Header("Shooting")] 
    public Transform shootingPoint;

    public float shootDistance = 10f;
    private bool inSight;
    private Vector3 directionToTarget;

    [Header("Weapon")] 
    public float fireRate = 2f;
    public float velocity;
    public GameObject projectile;
    private bool _canShoot;

    private int currentWayPoint;
    // Start is called before the first frame update
    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStates();
        CheckIfTargetInRangeForFollow();

        StartCoroutine(StartAttack());
    }

    private void UpdateStates()
    {
        switch (currentState)
        {
            case States.Patrol:
                Patrol();
                break;
            case States.Follow:
                Follow();
                break;
            case States.Attack:
                Attack();
                break;
            
        }
    }

    private void CheckIfTargetInRangeForFollow()
    {
        directionToTarget = target.position - transform.position;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, directionToTarget.normalized, out hit))
        {
            inSight = hit.transform.CompareTag("Player");
        }
        
        currentState = States.Follow;
    }

    private void Patrol()
    {
        if (agent.destination != wayPoints[currentWayPoint].position)
        {
            agent.destination = wayPoints[currentWayPoint].position;
        }

        if (HasReached())
        {
           
            currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
        }
    }
    private void Follow()
    {
        if (agent.remainingDistance <= shootDistance && inSight)
        {
            agent.ResetPath();
            /*currentState = States.Attack;*/
        }
        else
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }    
        }
    }
    private void Attack()
    {
        if (!inSight)
        {
            currentState = States.Follow;
        }
        LookAtTarget();
        StartCoroutine(StartAttack());
    }

    private void Shoot()
    {
        var spawnedProjectile = Instantiate(projectile, shootingPoint.position, Quaternion.identity);
        var moveDirection = (target.transform.position - spawnedProjectile.transform.position).normalized;

        spawnedProjectile.GetComponent<Rigidbody>().velocity = velocity * moveDirection;
    }

    private IEnumerator StartAttack()
    {
        _canShoot = false;
        Shoot();
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }

    private void LookAtTarget()
    {
        Vector3 lookDirection = directionToTarget;
        lookDirection.y = 0f;

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
    }

    private bool HasReached()
    {
        return (agent.hasPath && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
    }
}
