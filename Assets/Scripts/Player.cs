using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsInverted;
    float speed;
    float horizontal;
    float vertical;

    void Start()
    {
        var carData = GameData.GetCarData(GameData.SelectedCarId);
        speed = carData.Speed;
        if (carData != null)
        {
            ChangeCarModel(carData.Model);
        }
    }

    private void ChangeCarModel(GameObject carPrefab)
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        Instantiate(carPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
       
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (!IsInverted)
        {
            transform.position += new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(-horizontal, 0, -vertical) * speed * Time.deltaTime;
        }
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -1, 4);
        position.z = Mathf.Clamp(position.z, -10, 0);
        transform.position = position;

    }
}
