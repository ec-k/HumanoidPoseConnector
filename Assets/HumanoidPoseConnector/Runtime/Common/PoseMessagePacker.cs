using uOSC;
using UnityEngine;
using System;

namespace HumanoidPoseConnector
{
    public class PoseMessagePacker
    {
        const string _address = "/VMC/Ext/Bone/Pos";

        public Bundle BundleHumanoidBones(Animator animator)
        {
            var bundle = new Bundle();

            var values = Enum.GetValues(typeof(HumanBodyBones));
            foreach (var boneObj in values)
            {
                var bone = (HumanBodyBones)boneObj;
                var msg = GenerateBoneMessage(bone, animator);
                if (msg is Message validMessage)
                    bundle.Add(validMessage);
            }

            return bundle;
        }

        Message? GenerateBoneMessage(HumanBodyBones bone, Animator animaator)
        {
            var boneTransform = animaator.GetBoneTransform(bone);
            if (boneTransform is null)
                return null;

            var boneName = bone.ToString();
            return new Message(
                _address,
                boneName,
                boneTransform.localPosition.x,
                boneTransform.localPosition.y,
                boneTransform.localPosition.z,
                boneTransform.localRotation.x,
                boneTransform.localRotation.y,
                boneTransform.localRotation.z,
                boneTransform.localRotation.w);
        }
    }
}
