using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    public GameObject LifePrefab;
    private List<GameObject> _spawnedLifes = new List<GameObject>();
    float position = -349.57f;

    public void SpawnLives(int lifeCount)
    {
        for (int i = 0; i < lifeCount; i++)
        {
            GameObject lifeGo = Instantiate(LifePrefab, transform);
            RectTransform lifeRT = (RectTransform)lifeGo.transform;
            lifeRT.localPosition = new Vector3(position, 398.5f, -43);
            lifeRT.localRotation = Quaternion.identity;
            lifeRT.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            _spawnedLifes.Add(lifeGo);
            position += 128;
        }
    }

    public void UpdateLives(int currentLife)
    {
        for (int i = 0; i < _spawnedLifes.Count; i++)
        {
            _spawnedLifes[i].SetActive(i < currentLife);
        }
    }
}
