using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
  public List<GameObject> astroids;
  public int asteroidCount = 200;
  public float distanceFromGenerator = 50.0f;

  void Start()
  {
    for (int i = 0; i < asteroidCount; i++)
    {
      int asteroidTypeIndex = Random.Range(0, astroids.Count);
      var asteroidPosition = Random.insideUnitSphere * distanceFromGenerator;
      var asteroidRotation = Random.rotation;

      float scale = Random.Range(0.5f, 5.0f);

      var currentAsteroid = Instantiate(astroids[asteroidTypeIndex], asteroidPosition, asteroidRotation);
      currentAsteroid.transform.localScale = new Vector3(scale, scale, scale);
    }
  }
}
