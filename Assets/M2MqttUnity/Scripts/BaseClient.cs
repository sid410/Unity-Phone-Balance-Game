using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;


namespace M2MqttUnity
{
    /// <summary>
    /// M2MQTT that subscribes on a given topic
    /// </summary>
    public class BaseClient : M2MqttUnityClient
    {
        public delegate void MessageReceivedDelegate(string topic, string message);
        private Dictionary<string, MessageReceivedDelegate> m_messageHandlers = new Dictionary<string, MessageReceivedDelegate>();

        [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
        public bool autoTest = false;
        public bool stopConnection = false;
        public bool restart = false;
        public string topic = "M2MQTT_Unity/test";
        public string lastMsg;

        private List<string> eventMessages = new List<string>();

        public void RegisterTopicHandler(string topic, MessageReceivedDelegate messageReceivedDelegate)
        {
            if (!m_messageHandlers.ContainsKey(topic))
            {
                m_messageHandlers.Add(topic, null);
            }

            m_messageHandlers[topic] += messageReceivedDelegate;
        }

        public void UnregisterTopicHandler(string topic, MessageReceivedDelegate messageReceivedDelegate)
        {
            if (m_messageHandlers.ContainsKey(topic))
            {
                m_messageHandlers[topic] -= messageReceivedDelegate;
            }
        }


        //Update method called every frame
        protected override void Update()
        {
            base.Update(); // call ProcessMqttEvents()

            if (stopConnection)
            {
                stopConnection = false;
                Disconnect();
            }

            if (restart) //needs some time
            {
                Disconnect();
                restart = false;
                Connect();
            }

            if (eventMessages.Count > 0)
            {
                foreach (string msg in eventMessages)
                {

                }
                eventMessages.Clear();
            }
        }


        protected override void OnConnected()
        {
            base.OnConnected();

            if (autoTest)
            {
                client.Publish(topic, System.Text.Encoding.UTF8.GetBytes("On_Connect message sent on topic "+topic), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
        }


        protected override void SubscribeTopics()
        {
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
        }

        protected override void UnsubscribeTopics()
        {
            client.Unsubscribe(new string[] { topic });
        }


        protected override void DecodeMessage(string _topic, byte[] message)
        {
            string msg = System.Text.Encoding.UTF8.GetString(message);
            foreach (string topicKey in m_messageHandlers.Keys)
            {
                //if (m_messageHandlers.ContainsKey(_topic))
                if (_topic.Contains(topicKey))
                {
                    MessageReceivedDelegate messageReceivedDelegate = m_messageHandlers[topicKey];
                    if (messageReceivedDelegate != null)
                    {
                        messageReceivedDelegate(_topic, msg);
                    }
                }
            }
        }
        
        private void OnDestroy()
        {
            Disconnect();
        }
    }



}


