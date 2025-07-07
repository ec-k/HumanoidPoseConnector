using System;
using System.Collections.Generic;
using uOSC;
using UnityEngine;

namespace HumanoidPoseConnector
{
    [RequireComponent(typeof(uOscServer))]
    public class PoseReceiver : MonoBehaviour
    {
        uOscServer _server;
        public float LastReceivedTime { get; private set; }

        public Dictionary<HumanBodyBones, Quaternion> Results { get; set; }

        void Start()
        {
            Results = new();
            _server = GetComponent<uOscServer>();
            _server.onDataReceived.AddListener(OnDataReceived);
        }

        void OnDataReceived(Message message)
        {
            var boneKey = (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), message.address);
            var rotation = new Quaternion((float)message.values[0], (float)message.values[1], (float)message.values[2], (float)message.values[3]);
            Results[boneKey] = rotation;
            LastReceivedTime = Time.time;
        }
    }
}
