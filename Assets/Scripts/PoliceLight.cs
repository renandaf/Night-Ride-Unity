using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLight : MonoBehaviour
{
    public float Delay;
    private Light PointLight;
    private bool IsEnable;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(Delay);
        PointLight = GetComponent<Light>();
        IsEnable = false;
        StartCoroutine(Flicker());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            PointLight.enabled = !IsEnable;
            IsEnable = PointLight.enabled;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
