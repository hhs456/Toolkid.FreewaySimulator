using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VehicleArgs {
    [SerializeField, Tooltip("kilometer per hour")]
    protected float velocity = 0f;
    [SerializeField, Tooltip("kilometer per sec")]
    protected float acceleration = 0f;
    public float Velocity {
        get {
            return velocity;
        }
        private set {
            if (value > 0) {
                velocity = value;
            }
            else {
                velocity = 0;
            }
        }
    }
    public float Acceleration { get { return acceleration; } }

    public float SafetyDistance { get { return Velocity / 2f; } }

    public void SpeedUp(float a) {
        Velocity += a;        
    }
}
public enum TimeScale {
    Sec = 1,
    Min = 60,
    Hour = 3600
}

public enum DistanceScale {
    Meter = 1,
    TenMeter = 10,
    Kilometer = 1000    
}

public class VehicleBase : MonoBehaviour
{
    public Transform frontVehicle;
    public float safetyDistance = 0f;
    public float currentDistance = 0f;
    public float time = 0f;
    public float displacement;
    public float speedlimit = 100f;
    [SerializeField]
    protected VehicleArgs args;
    [SerializeField]
    private bool enableAccelerator = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (frontVehicle) {
            currentDistance = Vector3.Distance(transform.position, frontVehicle.position);
        }
        else {
            currentDistance = args.SafetyDistance / (float)Scaler.Main.distanceScale;
        }
        safetyDistance = args.SafetyDistance / (float)Scaler.Main.distanceScale;
        enableAccelerator = currentDistance > safetyDistance | !frontVehicle;
    }

    private void FixedUpdate() {
        if (!enableAccelerator) {
            args.SpeedUp(-args.Acceleration * Time.deltaTime);
        }
        else {
            if (args.Velocity < speedlimit) {
                args.SpeedUp(args.Acceleration * Time.deltaTime);
            }
        }
        var realSpeed = args.Velocity * Time.deltaTime * 1000f / 3600f / (float)Scaler.Main.distanceScale * (float)Scaler.Main.timeScale;
        time += Time.deltaTime;
        displacement += realSpeed;
        transform.Translate(0f, 0f, realSpeed);
    }
}
