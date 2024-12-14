using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AlienController : MonoBehaviour
{
  public GameObject bullet;
  public Transform bulletSpawn;

  private float moveSpeed = 1.0f;
  private float rotationSpeed = 1.0f;
  private float attackDistance = 10.0f;
  private float chaseDistance = 20.0f;
  private float nextAttackTime = 0.0f;
  private Animator animator;
  private Rigidbody rigidbody;

  private UnityEngine.AI.NavMeshAgent navMeshAgent;
  private bool isDead = false;
  private GameObject player;


  private void Start()
  {
    animator = GetComponent<Animator>();
    rigidbody = GetComponent<Rigidbody>();
    navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    player = GameObject.FindGameObjectWithTag("Player");
  }

  // Update is called once per frame
  void Update()
  {
    if (isDead) return;

    float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

    if (distanceToPlayer <= attackDistance)
    {
      FacePlayer();

      if (Time.time >= nextAttackTime)
      {
        animator.SetBool("isAttacking", true);
        animator.SetBool("isWalking", false);
        Fire();
        nextAttackTime = Time.time + 2.0f;
      }
    }
    else if (distanceToPlayer <= chaseDistance)
    {
      navMeshAgent.SetDestination(player.transform.position);
      animator.SetBool("isAttacking", false);
      animator.SetBool("isWalking", true);
    }
    else
    {
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
      // Spawn bullet at the gun's current position
      var bulletPosition = bulletSpawn.position;

      // Lock the bullet direction at the moment of firing
      Vector3 fixedDirection = bulletSpawn.forward.normalized;

      // Create the bullet instance
      var firedBullet = Instantiate(bullet, bulletPosition, Quaternion.LookRotation(fixedDirection));

      // Apply velocity to the bullet to ensure it flies straight
      var bulletRigidbody = firedBullet.GetComponent<Rigidbody>();
      if (bulletRigidbody != null)
      {
        bulletRigidbody.velocity = fixedDirection * 200f; // Adjust speed as needed
      }
    }
  }
}
