using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public GameObject bullet;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.R))
    {
      Fire();
    }
  }

  private void Fire()
  {
    var bulletPosition = transform.position + transform.forward * 2.0f;
    var bulletRotation = transform.rotation;

    Instantiate(bullet, bulletPosition, bulletRotation);

  }
}
