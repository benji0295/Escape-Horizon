using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipController : MonoBehaviour
{
  private Rigidbody rigidbody;
  private int thrustSensor = 0;
  private Vector3 torqueSensor;
  private bool isPlayerNear = false;

  public float thrustSpeed = 10.0f;
  public float torqueSpeed = 0.1f;

  public TMP_Text livesText;
  public GameObject missile;
  public GameObject spawnPrompt;

  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
    livesText.text = "Lives: " + GameManager.lives;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.UpArrow))
    {
      thrustSensor = 1;
    }
    else if (Input.GetKey(KeyCode.DownArrow))
    {
      thrustSensor = -1;
    }
    else
    {
      thrustSensor = 0;
    }
    torqueSensor = Vector3.zero;
    if (Input.GetKey(KeyCode.W))
    {
      torqueSensor += transform.right;
    }
    else if (Input.GetKey(KeyCode.S))
    {
      torqueSensor -= transform.right;
    }
    if (Input.GetKey(KeyCode.D))
    {
      torqueSensor += transform.up;
    }
    else if (Input.GetKey(KeyCode.A))
    {
      torqueSensor -= transform.up;
    }
    if (Input.GetKey(KeyCode.Q))
    {
      torqueSensor += transform.forward;
    }
    else if (Input.GetKey(KeyCode.E))
    {
      torqueSensor -= transform.forward;
    }
  }
  private void FixedUpdate()
  {
    rigidbody.AddForce(transform.forward * thrustSensor * thrustSpeed);
    rigidbody.AddTorque(torqueSensor * torqueSpeed);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Respawn"))
    {
      isPlayerNear = true;
      if (spawnPrompt != null)
      {
        spawnPrompt.SetActive(true);
      }
    }
    if (other.CompareTag("Portal"))
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("WinScreen");
    }
    if (other.CompareTag("Meteor"))
    {
      GameManager.lives--;
      livesText.text = "Lives: " + GameManager.lives;

      if (GameManager.lives <= 0)
      {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoseScreen");
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
  }
}
