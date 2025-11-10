using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class EnemyCar : MonoBehaviour
{
    public float MoveSpeed = 0;
    public float TurnSpeed = 2;
    public float ChaseSpeed = 1;

    private Vector3 _moveDirection;

    private Player _player;

    private GameManager _gameManager;
    private AudioManager _audioManager;
    public GameObject particle;

    public enum EnemyType
    {
        Crazy = 0,
        Straight = 1
    }

    private EnemyType _enemyType;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _enemyType = (EnemyType)Random.Range(0,2);
        _gameManager = FindObjectOfType<GameManager>();
        _audioManager = FindObjectOfType<AudioManager>();
    }
    private void UpdateCrazy()
    {
        if (_moveDirection == Vector3.zero)
        {
            _moveDirection = Random.value < 0.5f ? Vector3.left : Vector3.right;
        }

        transform.position += _moveDirection * TurnSpeed * Time.deltaTime;

        if (transform.position.x <= -2f || transform.position.x >= 5f)
        {
            _moveDirection = -_moveDirection;
        }
    }

    private void UpdateStraight()
    {
        transform.position += -Vector3.forward * MoveSpeed;
    }

    private void Update()
    {
        switch (_enemyType)
        {
            case EnemyType.Crazy:
                UpdateCrazy();
                break;
            case EnemyType.Straight:
                UpdateStraight();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            CameraShaker.Instance.ShakeOnce(5f, 5f, 0.1f, .7f);
            _gameManager.DecreaseHealth();
            GameObject spawnParticle = Instantiate(particle, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            _audioManager.Play("Impact");
            Destroy(spawnParticle, 2);
            Destroy(gameObject);
        }
    }
}
