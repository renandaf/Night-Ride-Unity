using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Obstacle : MonoBehaviour
{
    private GameManager _gameManager;
    private AudioManager _audioManager;
    public GameObject particle;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player)){          
            CameraShaker.Instance.ShakeOnce(5f, 5f, 0.1f, .7f);
            _gameManager.DecreaseHealth();
            GameObject spawnParticle = Instantiate(particle,transform.position + new Vector3(0,1,0),Quaternion.identity);
            _audioManager.Play("Impact");
            Destroy(spawnParticle,2);
            Destroy(gameObject);
        }
    }
}
