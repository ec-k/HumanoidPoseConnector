using System;
using UnityEngine;
using uOSC;

namespace HumanoidPoseConnector
{
    public static class MessageParser
    {
        public static (HumanBodyBones boneKey, Vector3 position, Quaternion rotation) ParseBoneTransformMessage(Message message)
        {
            var boneKey = (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), message.values[0] as string);
            var position = new Vector3(
                (float)message.values[1],
                (float)message.values[2],
                (float)message.values[3]);
            var rotation = new Quaternion(
                (float)message.values[4],
                (float)message.values[5],
                (float)message.values[6],
                (float)message.values[7]);
            return (boneKey, position, rotation);
        }

        public static (string key, float weight) ParseBlendshapeMessage(Message message)
        {
            var key = message.values[0] as string;
            var weight = (float)message.values[1];
            return (key, weight);
        }
    }
}
