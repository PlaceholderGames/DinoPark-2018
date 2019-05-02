using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEventsTwo : MonoBehaviour {
    [SerializeField]
    bool printTime = true;

    [SerializeField]
    Light light;

    private TimeOfDayTwo time;
    private float hourSplit = 0.0416f;
    private float lastCheck = 0;
    private int digitalTime = 0;

    private void Start()
    {
        time = light.GetComponent<TimeOfDayTwo>();
        
    }

    private void Update()
    {
        TimeToUpdate();
    }

    private void TimeToUpdate()
    {
        if (time.currentTimeOfDay < 0.02)
        {
            lastCheck = 0;
            digitalTime = 0;
        }

        if (time.currentTimeOfDay > lastCheck + hourSplit)
        {
            if (printTime)
            {
                digitalTime++;
                print(digitalTime + ":00");
            }

            lastCheck += hourSplit;
        }
    }
}
