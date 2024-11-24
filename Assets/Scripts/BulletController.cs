using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
  private const float BULLET_SPEED = 2.0f;
  private const float BULLET_LIFETIME = 0.5f;
  private Rigidbody rb;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    rb.AddForce(transform.forward * BULLET_SPEED, ForceMode.Impulse);

    Destroy(gameObject, BULLET_LIFETIME);
  }
}
