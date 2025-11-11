using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadMenu : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float ZThreshold = 30;
    public float RoadCount = 3;
   
    public GameObject _spawnedVisual;
    // Start is called before the first frame update
    void Start()
    {
        ResetVisual();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition -= transform.forward * MoveSpeed * Time.deltaTime;
        if (transform.position.z <= -ZThreshold + -10)
        {
            transform.localPosition = transform.forward * ((ZThreshold - 10) * (RoadCount - 1));
            ResetVisual();
        }
    }
    private void ResetVisual()
    {
        List<GameObject> _visualPrefab = Resources.LoadAll<GameObject>("Road").ToList();
        if(_spawnedVisual != null){
            Destroy(_spawnedVisual);
        }
        _spawnedVisual = (Instantiate(_visualPrefab[Random.Range(0, _visualPrefab.Count)], this.transform));
    }
}
