using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public GameObject bullet;
  public GameObject spawnPrompt;
  public GameObject enterShipPrompt;

  private bool hasKey = false;
  private bool isPlayerNear = false;

  // Start is called before the first frame update
  void Start()
  {
    if (spawnPrompt != null)
    {
      spawnPrompt.SetActive(false);
    }
    if (enterShipPrompt != null)
    {
      enterShipPrompt.SetActive(false);
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.R))
    {
      Fire();
    }
    if (isPlayerNear && hasKey && Input.GetKeyDown(KeyCode.E))
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("LevelThree");
    }
  }

  private void Fire()
  {
    var bulletPosition = transform.position + transform.forward * 2.0f;
    var bulletRotation = transform.rotation;

    Instantiate(bullet, bulletPosition, bulletRotation);

  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Key"))
    {
      Destroy(other.gameObject);
      hasKey = true;
      Debug.Log("Player has key");
    }
    if (other.CompareTag("EnterShip"))
    {
      if (hasKey)
      {
        Debug.Log("Player has entered the ship");
      }
      else
      {
        Debug.Log("Player does not have key");
      }
    }
    if (other.CompareTag("Respawn"))
    {
      isPlayerNear = true;
      if (spawnPrompt != null)
      {
        spawnPrompt.SetActive(true);
      }
    }
    if (other.CompareTag("EnterShip"))
    {
      isPlayerNear = true;
      if (enterShipPrompt != null)
      {
        enterShipPrompt.SetActive(true);

      }
    }
  }
  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Respawn"))
    {
      isPlayerNear = false;
      if (spawnPrompt != null)
      {
        spawnPrompt.SetActive(false);
      }
    }
    if (other.CompareTag("EnterShip"))
    {
      isPlayerNear = false;
      if (enterShipPrompt != null)
      {
        enterShipPrompt.SetActive(false);
      }
    }
  }
}
