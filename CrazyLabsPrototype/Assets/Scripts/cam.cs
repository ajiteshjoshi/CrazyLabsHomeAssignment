using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private CarController carController;
    [SerializeField] private Transform car;
    private Transform child;
    [SerializeField] private float speed;
    [SerializeField] private float shakeMag;
    void Start()
    {
        carController = FindObjectOfType<CarController>();
        child = car.transform.Find("constraint");
    }

    
    void Update()
    {
        
    }

    


   /* public IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration) 
        {
            float x = Random.Range(-1f, -1f) * magnitude;
            float y = Random.Range(-1f, -1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }*/
}
