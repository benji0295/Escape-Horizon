using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
  // Update is called once per frame
  private void Update()
  {
    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
    {
      if (Input.GetKeyDown(KeyCode.Return))
      {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelOne");
      }
    }

    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Coming Soon")
    {
      if (Input.GetKeyDown(KeyCode.Return))
      {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
      }
    }
  }
}
