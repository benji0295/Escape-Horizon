using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public TMP_Text livesText;
  public GameObject bullet;
  public GameObject spawnPrompt;
  public GameObject enterShipPrompt;
  public GameObject needKeyPrompt;
  public AudioClip laserSound;
  public AudioClip keySound;
  public AudioClip enterShipSound;

  public Transform gunBarrel;

  private bool hasKey = false;
  private bool isPlayerNear = false;
  private AudioSource audioSource;


  // Start is called before the first frame update
  void Start()
  {
    if (spawnPrompt != null)
    {
      spawnPrompt.SetActive(false);
      needKeyPrompt.SetActive(false);
    }
    if (enterShipPrompt != null)
    {
      enterShipPrompt.SetActive(false);
      needKeyPrompt.SetActive(false);
    }

    livesText.text = "Lives: " + GameManager.lives;

    audioSource = GetComponent<AudioSource>();
  }


  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Fire();
      if (laserSound != null)
      {
        audioSource.PlayOneShot(laserSound);
      }
      else
      {
        Debug.Log("Laser sound not assigned.");
      }
    }
    if (isPlayerNear && hasKey && Input.GetKeyDown(KeyCode.E))
    {
      audioSource.PlayOneShot(enterShipSound);
      UnityEngine.SceneManagement.SceneManager.LoadScene("LevelThree");
    }
  }

  private void Fire()
  {
    if (gunBarrel == null)
    {
      return;
    }

    var bulletPosition = gunBarrel.position;
    var bulletRotation = gunBarrel.rotation;

    Instantiate(bullet, bulletPosition, bulletRotation);

  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Key"))
    {
      Destroy(other.gameObject);
      hasKey = true;
      audioSource.PlayOneShot(keySound);
    }
    if (other.CompareTag("EnterShip"))
    {
      if (hasKey)
      {
        Debug.Log("Player has entered the ship");
      }
      else
      {
        needKeyPrompt.SetActive(true);
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
    if (other.CompareTag("EnemyBullet"))
    {
      GameManager.lives--;
      livesText.text = "Lives: " + GameManager.lives;
      if (GameManager.lives <= 0)
      {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoseScreen");
        GameManager.lives = 5;
      }
    }
    if (other.CompareTag("LevelPortal"))
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("LevelTwo");
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
        needKeyPrompt.SetActive(false);
      }
    }
  }
}
