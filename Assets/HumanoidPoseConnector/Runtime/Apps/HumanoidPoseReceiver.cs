using System;
using System.Collections.Generic;
using UnityEngine;
using uOSC;

namespace HumanoidPoseConnector
{
    [RequireComponent(typeof(uOscServer))]
    public class PoseReceiver : MonoBehaviour
    {
        uOscServer _server;
        public Dictionary<HumanBodyBones, (Vector3, Quaternion)> PoseResults { get; private set; }
        public Dictionary<string, float> BlendshapeResults { get; private set; }
        public float LastReceivedTime { get; private set; }
        public bool IsAvailable { get; private set; }

        public event Action OnBlendshapeApplyRequested;

        const string AvailabilityMessageAddress = "/VMC/Ext/OK";
        const string BoneTransformMessageAddress = "/VMC/Ext/Bone/Pos";
        const string BlendshapeWeightMessageAddress = "/VMC/Ext/Blend/Val";
        const string BlendshapeApplyingMessageAddress = "/VMC/Ext/Blend/Apply";
        const string TimeMessageAddress = "/VMC/Ext/T";

        void Start()
        {
            PoseResults = new();
            BlendshapeResults = new();
            _server = GetComponent<uOscServer>();
            _server.onDataReceived.AddListener(OnDataReceived);
        }

        void OnDataReceived(Message message)
        {
            var address = message.address;
            switch (address)
            {
                case AvailabilityMessageAddress:
                    IsAvailable = (int)message.values[0] == 1;
                    break;
                case BoneTransformMessageAddress:
                    var bone = MessageParser.ParseBoneTransformMessage(message);
                    PoseResults[bone.boneKey] = (bone.position, bone.rotation);
                    break;
                case BlendshapeWeightMessageAddress:
                    var parameter = MessageParser.ParseBlendshapeMessage(message);
                    BlendshapeResults[parameter.key] = parameter.weight;
                    break;
                case BlendshapeApplyingMessageAddress:
                    OnBlendshapeApplyRequested?.Invoke();
                    break;
                case TimeMessageAddress:
                    LastReceivedTime = (float)message.values[0];
                    break;
                default:
                    throw new ArgumentException($"Unknown message address received: {address}", nameof(message.address));
            }
        }
    }
}
