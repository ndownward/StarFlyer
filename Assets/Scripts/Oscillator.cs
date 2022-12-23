using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 4f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        Debug.Log(startingPos);
    }

    // Update is called once per frame
    void Update()
    {
        //protect against NaN
        if (period <= Mathf.Epsilon){
            return;
        }

        //value continuously grows
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;

        //goes from -1 to 1
        float rawSineWave = Mathf.Sin(cycles * tau);
        
        //changes range from 0 to 1
        movementFactor = (rawSineWave + 1f) / 2;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
