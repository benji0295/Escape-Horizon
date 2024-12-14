using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
  private const float BULLET_SPEED = 200.0f;
  private const float BULLET_LIFETIME = 0.5f;
  private Rigidbody rb;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    rb.velocity = transform.forward * BULLET_SPEED;

    Destroy(gameObject, BULLET_LIFETIME);
  }

}
