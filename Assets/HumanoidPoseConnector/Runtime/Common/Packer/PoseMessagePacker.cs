using uOSC;
using UnityEngine;
using System.Collections.Generic;

namespace HumanoidPoseConnector
{
    public class PoseMessagePacker
    {
        const string _address = "/VMC/Ext/Bone/Pos";

        List<HumanBodyBones> poseBones = new()
        {
            HumanBodyBones.Hips,
            HumanBodyBones.Spine,
            HumanBodyBones.Chest,
            HumanBodyBones.UpperChest,
            HumanBodyBones.Neck,
            HumanBodyBones.Head,
            HumanBodyBones.Jaw,
            HumanBodyBones.LeftEye,
            HumanBodyBones.RightEye,
            HumanBodyBones.LeftShoulder,
            HumanBodyBones.LeftUpperArm,
            HumanBodyBones.LeftLowerArm,
            HumanBodyBones.LeftHand,
            HumanBodyBones.LeftUpperLeg,
            HumanBodyBones.LeftLowerLeg,
            HumanBodyBones.LeftFoot,
            HumanBodyBones.LeftToes,
            HumanBodyBones.RightShoulder,
            HumanBodyBones.RightUpperArm,
            HumanBodyBones.RightLowerArm,
            HumanBodyBones.RightHand,
            HumanBodyBones.RightUpperLeg,
            HumanBodyBones.RightLowerLeg,
            HumanBodyBones.RightFoot,
            HumanBodyBones.RightToes,
        };

        List<HumanBodyBones> fingerBones = new()
        {
            HumanBodyBones.LeftThumbProximal,
            HumanBodyBones.LeftThumbIntermediate,
            HumanBodyBones.LeftThumbDistal,
            HumanBodyBones.LeftIndexProximal,
            HumanBodyBones.LeftIndexIntermediate,
            HumanBodyBones.LeftIndexDistal,
            HumanBodyBones.LeftMiddleProximal,
            HumanBodyBones.LeftMiddleIntermediate,
            HumanBodyBones.LeftMiddleDistal,
            HumanBodyBones.LeftRingProximal,
            HumanBodyBones.LeftRingIntermediate,
            HumanBodyBones.LeftRingDistal,
            HumanBodyBones.LeftLittleProximal,
            HumanBodyBones.LeftLittleIntermediate,
            HumanBodyBones.LeftLittleDistal,
            HumanBodyBones.RightThumbProximal,
            HumanBodyBones.RightThumbIntermediate,
            HumanBodyBones.RightThumbDistal,
            HumanBodyBones.RightIndexProximal,
            HumanBodyBones.RightIndexIntermediate,
            HumanBodyBones.RightIndexDistal,
            HumanBodyBones.RightMiddleProximal,
            HumanBodyBones.RightMiddleIntermediate,
            HumanBodyBones.RightMiddleDistal,
            HumanBodyBones.RightRingProximal,
            HumanBodyBones.RightRingIntermediate,
            HumanBodyBones.RightRingDistal,
            HumanBodyBones.RightLittleProximal,
            HumanBodyBones.RightLittleIntermediate,
            HumanBodyBones.RightLittleDistal,
        };

        public Bundle PackAllBones(Animator animator)
        {
            var bundle = new Bundle();
            bundle.Add(PackPoseMessageBundle(animator));
            bundle.Add(PackHandMessageBundle(animator));
            return bundle;
        }

        public Bundle PackPoseMessageBundle(Animator animator)
        {
            var bundle = new Bundle();
            foreach (var bone in poseBones)
            {
                var message = GenerateBoneMessage(bone, animator);
                if (message is Message validMsg)
                    bundle.Add(validMsg);
            }

            return bundle;
        }

        public Bundle PackHandMessageBundle(Animator animator)
        {
            var bundle = new Bundle();

            foreach (var bone in fingerBones)
            {
                var message = GenerateBoneMessage(bone, animator);
                if (message is Message validMsg)
                    bundle.Add(validMsg);
            }

            return bundle;
        }

        Message? GenerateBoneMessage(HumanBodyBones bone, Animator animaator)
        {
            var boneTransform = animaator.GetBoneTransform(bone);
            if(boneTransform is null)
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
