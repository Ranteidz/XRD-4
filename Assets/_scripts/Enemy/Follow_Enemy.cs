using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Follow_Enemy : MonoBehaviour
{
    public enum States
    {
        Patrol,
        Follow,
        Attack
    }

    public NavMeshAgent agent;

    public Transform target;
    public Transform[] wayPoints;

    public States currentState;
    public float maxFollowDistance = 30;

    [Header("Shooting")] public Transform shootingPoint;

    public float shootDistance = 20f;

    [Header("Weapon")] public float fireRate = 2f;

    public float velocity;
    public GameObject projectile;
    private bool _canShoot = true;

    private int _currentWayPoint;
    private Vector3 _directionToTarget;

    private bool _inSight;

    // Start is called before the first frame update
    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateStates();
        CheckIfTargetInRangeForFollow();
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
        if (Vector3.Distance(transform.position, target.position) < shootDistance)
            currentState = States.Attack;
        else if (Vector3.Distance(transform.position, target.position) < maxFollowDistance)
            currentState = States.Follow;
        else
            currentState = States.Patrol;
    }

    private void Patrol()
    {
        if (agent.destination != wayPoints[_currentWayPoint].position)
            agent.destination = wayPoints[_currentWayPoint].position;

        if (HasReached()) _currentWayPoint = (_currentWayPoint + 1) % wayPoints.Length;

        if (_inSight && _directionToTarget.magnitude <= shootDistance) currentState = States.Follow;
    }

    private void Follow()
    {
        if (target != null) agent.SetDestination(target.position);
    }

    private void Attack()
    {
        agent.ResetPath();
        LookAtTarget();
        if (_canShoot) StartCoroutine(StartAttack());
    }

    private void Shoot()
    {
        var spawnedProjectile = Instantiate(projectile, shootingPoint.position, Quaternion.identity);
        var moveDirection = (target.transform.position - spawnedProjectile.transform.position).normalized;

        spawnedProjectile.GetComponent<Rigidbody>().velocity = velocity * moveDirection;

        if (spawnedProjectile != null) Destroy(spawnedProjectile, 5);
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
        var lookDirection = _directionToTarget;
        lookDirection.y = 0f;

        var lookRotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
    }

    private bool HasReached()
    {
        return agent.hasPath && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }
}