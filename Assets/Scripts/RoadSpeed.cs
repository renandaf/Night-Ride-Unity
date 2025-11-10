using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpeed : MonoBehaviour
{
    float normalSpeed;
    public List<Road> roads;
    float currentTime;
    public float timeToAddSpeed;

    private void Start()
    {
        normalSpeed = roads[0].MoveSpeed;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        if(currentTime >= timeToAddSpeed)
        {
            foreach (Road road in roads) {
                road.MoveSpeed += 1;             
            }
            normalSpeed += 1;
            currentTime = 0;
        }
    }

    public void ChangeSpeed(int option)
    {
        if (option == 0) {
            foreach (Road road in roads)
            {
                road.MoveSpeed = normalSpeed;
            }
        }
        else if( option == 1){
            foreach (Road road in roads)
            {
                road.MoveSpeed = road.MoveSpeed * 1.7f;
            }
        }
        else
        {
            foreach (Road road in roads)
            {
                road.MoveSpeed = road.MoveSpeed / 2f;
            }
        }
        
        
    }
}
