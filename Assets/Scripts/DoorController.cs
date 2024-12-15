using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

  public GameObject door;
  public GameObject prompt;
  public AudioClip doorOpenSound;

  private bool isPlayerNear = false;
  private AudioSource audioSource;

  // Start is called before the first frame update
  void Start()
  {
    if (prompt != null)
    {
      prompt.SetActive(false);
    }
    audioSource = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    if (isPlayerNear && Input.GetKeyDown(KeyCode.U))
    {
      if (door != null)
      {
        Destroy(door);
        audioSource.PlayOneShot(doorOpenSound);

        if (prompt != null)
        {
          prompt.SetActive(false);
        }
      }
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      isPlayerNear = true;
      if (prompt != null)
      {
        prompt.SetActive(true);
      }
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      isPlayerNear = false;
      if (prompt != null)
      {
        prompt.SetActive(false);
      }
    }
  }
}
