using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity;

public class MqttMessageHandler : MonoBehaviour
{
    public BaseClient baseClient;

    private void OnEnable()
    {
        baseClient.RegisterTopicHandler("UNMqtt", HandleMessage);
    }

    private void OnDisable()
    {
        baseClient.UnregisterTopicHandler("UNMqtt", HandleMessage);
    }

    private void HandleMessage(string topic, string message)
    {
        Debug.Log("topic: " + topic + " message: " + message);
    }
}
