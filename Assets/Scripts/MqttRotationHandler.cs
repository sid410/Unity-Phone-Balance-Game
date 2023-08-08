using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity;
using System;

public class MqttRotationHandler : MonoBehaviour
{
    public BaseClient baseClient;
    public GameObject PhoneGO;

    private Quaternion rot;
    private string[] radString;
    private float Yaw, Roll, Pitch;

    private void OnEnable()
    {
        baseClient.RegisterTopicHandler("UnityRotation", HandleRotation);
    }
    private void OnDisable()
    {
        baseClient.UnregisterTopicHandler("UnityRotation", HandleRotation);
    }

    private void HandleRotation(string topic, string message)
    {
        if (topic != "UnityRotation") return;

        radString = message.Split(',');

        Yaw = ConvertRadiansToDegrees(double.Parse(radString[0]));
        Roll = ConvertRadiansToDegrees(double.Parse(radString[1]));
        Pitch = ConvertRadiansToDegrees(double.Parse(radString[2]));

        rot = Quaternion.Euler(Roll, Pitch, -Yaw);
        PhoneGO.transform.localRotation = rot;
    }

    public static float ConvertRadiansToDegrees(double radians)
    {
        double degrees = (180 / Math.PI) * radians;
        return ((float)degrees);
    }
}
