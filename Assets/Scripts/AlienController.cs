using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AlienController : MonoBehaviour
{
  private float moveSpeed = 1.0f;
  private float rotationSpeed = 1.0f;
  private float attackDistance = 10.0f;
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
      if (Time.time >= nextAttackTime)
      {
        animator.SetBool("isAttacking", true);
        nextAttackTime = Time.time + 1.0f;
      }
    }
    else
    {
      animator.SetBool("isAttacking", false);
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
}
