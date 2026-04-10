using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var status1 = StatusManager.CreateStatus(power:1);
        var status2 = StatusManager.CreateStatus(power:2, defense:3);
        var sum = StatusManager.SumStatus(status1, status2);
        StatusManager.ShowStatus(sum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
