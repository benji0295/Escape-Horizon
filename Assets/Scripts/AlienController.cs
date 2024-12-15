using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AlienController : MonoBehaviour
{
  public GameObject bullet;
  public Transform bulletSpawn;

  private float moveSpeed = 2.0f;
  private float rotationSpeed = 3.0f;
  private float attackDistance = 10.0f;
  private float chaseDistance = 20.0f;
  private float nextAttackTime = 0.0f;
  private Animator animator;

  private UnityEngine.AI.NavMeshAgent navMeshAgent;
  private bool isDead = false;
  private GameObject player;


  private void Start()
  {
    animator = GetComponent<Animator>();
    navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    player = GameObject.FindGameObjectWithTag("Player");
    navMeshAgent.speed = moveSpeed;
  }

  void Update()
  {
    if (isDead) return;

    float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

    if (distanceToPlayer <= attackDistance)
    {
      navMeshAgent.isStopped = true;
      FacePlayer();

      if (Time.time >= nextAttackTime)
      {
        animator.SetBool("isAttacking", true);
        animator.SetBool("isWalking", false);
        Fire();
        nextAttackTime = Time.time + 1.0f;
      }
    }
    else if (distanceToPlayer <= chaseDistance)
    {
      navMeshAgent.isStopped = false;
      navMeshAgent.SetDestination(player.transform.position);

      animator.SetBool("isAttacking", false);

      if (navMeshAgent.velocity.magnitude > 0.1f && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
      {
        animator.SetBool("isWalking", true);
        float speed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        animator.SetFloat("Speed", Mathf.Clamp(speed, 0, 1));
      }
      else
      {
        animator.SetBool("isWalking", false);
      }
    }
    else
    {
      navMeshAgent.isStopped = true;
      animator.SetBool("isAttacking", false);
      animator.SetBool("isWalking", false);
    }
  }

  private void FacePlayer()
  {
    Vector3 direction = player.transform.position - transform.position;
    direction.y = 0;

    if (direction != Vector3.zero)
    {
      Quaternion rotation = Quaternion.LookRotation(direction);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Bullet"))
    {
      isDead = true;
      animator.SetTrigger("isDead");
      Destroy(gameObject, 2.0f);
    }
  }

  private void Fire()
  {
    if (animator.GetBool("isAttacking"))
    {
      var bulletPosition = bulletSpawn.position;

      Vector3 firingDirection = transform.forward.normalized;

      var firedBullet = Instantiate(bullet, bulletPosition, Quaternion.LookRotation(firingDirection));

      var bulletRigidbody = firedBullet.GetComponent<Rigidbody>();
      if (bulletRigidbody != null)
      {
        bulletRigidbody.velocity = firingDirection * 200f;
      }
    }
  }
}
