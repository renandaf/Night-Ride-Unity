using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gasoline : MonoBehaviour
{
    public GameObject particle;
    private GameManager _gameManager;
    private AudioManager _audioManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Sin(Time.time * 5) * 0.003f + transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            GameObject spawn = Instantiate(particle, transform.position - new Vector3(0,0,1.7f), Quaternion.Euler(-90f,0,0));
            Destroy(spawn, 2);
            Destroy(gameObject);
            _audioManager.Play("Health");
            _gameManager.IncreaseHealth();
        }
    }
}
