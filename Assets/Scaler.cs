using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour {
    public static Scaler Main { set; get; }
    public TimeScale timeScale = TimeScale.Sec;
    public DistanceScale distanceScale = DistanceScale.Meter;
    // Start is called before the first frame update
    void Awake()
    {
        Main = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
