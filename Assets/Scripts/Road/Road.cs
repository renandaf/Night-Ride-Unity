using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Road : MonoBehaviour
{
    public bool isModif;
    public float MoveSpeed = 5;
    public float ZThreshold = 30;   
    public int RoadCount = 3;
    private GameManager _gameManager;
    public List<GameObject> _obstaclePrefab;
    public GameObject _coins;
    public GameObject _health;
    public int objectPerRoad;
    public int coinSpawnCount;
    public List<GameObject> _spawnedObstacle;
    public List<GameObject> _spawnedCoins;
    public List<GameObject> _spawnedHealth;
    public GameObject _spawnedVisual;
    public int obstacleDistance, coinsDistance,healthDistance;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        ResetObstacle();
        ResetCoins();
        ResetHeatlh();
        ResetVisual();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.IsGameOver)
        {
            return;
        }

        transform.localPosition -= transform.forward * MoveSpeed * Time.deltaTime;
        if (transform.position.z <= -ZThreshold + -10)
        {
            transform.localPosition = transform.forward * ((ZThreshold - 10) * (RoadCount - 1));
            ResetObstacle();
            ResetCoins();
            ResetHeatlh();
            ResetVisual();
            int a = Random.Range(0,4);
            if(a == 1 && isModif){
                ResetQuestion();
            }         
        }
    }

    private void ResetObstacle()
    {
        if (_spawnedQuestion != null)
        {
            Destroy(_spawnedQuestion);
        }
        foreach (GameObject prefab in _spawnedObstacle)
        {
            Destroy(prefab);
           
        }
        _spawnedObstacle.Clear();
        int z = 1;
        for (int i = 0; i < objectPerRoad; i++)
        {  
            _spawnedObstacle.Add(Instantiate(_obstaclePrefab[Random.Range(0, _obstaclePrefab.Count)], this.transform));
            _spawnedObstacle[i].transform.localPosition = new Vector3(Random.Range(-1.2f, 4.2f), -5, z);
            z += obstacleDistance;
        }
    }

    public GameObject _spawnedQuestion;
    public GameObject _questionPrefab;

    private void ResetQuestion()
    {
        _spawnedQuestion = (Instantiate(_questionPrefab, this.transform));
        _spawnedQuestion.transform.position = new Vector3(0.9955686F, 5.7899F, 30F);
    }

    private void ResetVisual()
    {
        List<GameObject> _visualPrefab = Resources.LoadAll<GameObject>("Road").ToList();
        if(_spawnedVisual != null){
            Destroy(_spawnedVisual);
        }
        _spawnedVisual = (Instantiate(_visualPrefab[Random.Range(0, _visualPrefab.Count)], this.transform));
    }

    private void ResetCoins()
    {
        foreach (GameObject coin in _spawnedCoins)
        {
            Destroy(coin);
        }
        _spawnedCoins.Clear();
        int z = 3;
        for (int i = 0; i < coinSpawnCount; i++)
        {
            _spawnedCoins.Add(Instantiate(_coins, this.transform));
            _spawnedCoins[i].transform.localPosition = new Vector3(Random.Range(-1.2f, 4.2f), -4.2f, z);
            z += coinsDistance;
        }
    }

    private void ResetHeatlh()
    {
        foreach (GameObject health in _spawnedHealth)
        {
            Destroy(health);
        }
        _spawnedHealth.Clear();
        for (int i = 0; i < Random.Range(-1,2); i++)
        {
            _spawnedHealth.Add(Instantiate(_health, this.transform));
            _spawnedHealth[i].transform.localPosition = new Vector3(Random.Range(-1.2f, 4.2f), -4.2f, healthDistance);           
        }
    }
}
