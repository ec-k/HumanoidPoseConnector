using uOSC;
using UnityEngine;

namespace HumanoidPoseConnector
{
    [RequireComponent(typeof(uOscClient))]
    public class PoseSender : MonoBehaviour
    {
        uOscClient _client;
        [SerializeField] Animator _avatarAnimator;
        [SerializeField] float _sendingRate = 60f;
        [SerializeField] bool _sendPose = true;
        [SerializeField] bool _sendHand = true;
        [SerializeField] bool _sendFacialBlendshapes = true;

        public bool IsActive { get; set; }
            
        float _timer = 0f;
        float _sendingInterval => 1 / _sendingRate;

        void Start()
        {
            _client = GetComponent<uOscClient>();
        }

        void Update()
        {
            if (_avatarAnimator is null || !IsActive) 
                return;

            _timer += Time.deltaTime;

            if(_sendingInterval <= _timer)
            {
                SendAvatarPoseMessage();
                _timer = 0f;
            }
        }

        void SendAvatarPoseMessage()
        {
            var msg = new Bundle(Timestamp.Now);
            if (_sendPose) msg.Add(SetPoseMessage());
            if(_sendHand) msg.Add(SetHandMessage());
            if (_sendFacialBlendshapes) msg.Add(SetBlendshapes());
            _client.Send(msg);
        }

        Bundle SetPoseMessage()
        {
            var msg = new Bundle();
            msg.Add(SetRotationMessage(HumanBodyBones.Hips));
            msg.Add(SetRotationMessage(HumanBodyBones.Spine));
            msg.Add(SetRotationMessage(HumanBodyBones.Chest));
            msg.Add(SetRotationMessage(HumanBodyBones.UpperChest));
            msg.Add(SetRotationMessage(HumanBodyBones.Neck));
            msg.Add(SetRotationMessage(HumanBodyBones.Head));
            msg.Add(SetRotationMessage(HumanBodyBones.Jaw));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftEye));
            msg.Add(SetRotationMessage(HumanBodyBones.RightEye));

            msg.Add(SetRotationMessage(HumanBodyBones.LeftShoulder));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftUpperArm));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftLowerArm));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftHand));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftUpperLeg));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftLowerLeg));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftFoot));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftToes));

            msg.Add(SetRotationMessage(HumanBodyBones.RightShoulder));
            msg.Add(SetRotationMessage(HumanBodyBones.RightUpperArm));
            msg.Add(SetRotationMessage(HumanBodyBones.RightLowerArm));
            msg.Add(SetRotationMessage(HumanBodyBones.RightHand));
            msg.Add(SetRotationMessage(HumanBodyBones.RightUpperLeg));
            msg.Add(SetRotationMessage(HumanBodyBones.RightLowerLeg));
            msg.Add(SetRotationMessage(HumanBodyBones.RightFoot));
            msg.Add(SetRotationMessage(HumanBodyBones.RightToes));

            return msg;
        }

        Bundle SetHandMessage()
        {
            var msg = new Bundle();
            msg.Add(SetRotationMessage(HumanBodyBones.LeftThumbProximal));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftThumbIntermediate));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftThumbDistal));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftIndexProximal));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftIndexIntermediate));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftIndexDistal));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftMiddleProximal));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftMiddleIntermediate));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftMiddleDistal));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftRingProximal));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftRingIntermediate));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftRingDistal));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftLittleProximal));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftLittleIntermediate));
            msg.Add(SetRotationMessage(HumanBodyBones.LeftLittleDistal));

            msg.Add(SetRotationMessage(HumanBodyBones.RightThumbProximal));
            msg.Add(SetRotationMessage(HumanBodyBones.RightThumbIntermediate));
            msg.Add(SetRotationMessage(HumanBodyBones.RightThumbDistal));
            msg.Add(SetRotationMessage(HumanBodyBones.RightIndexProximal));
            msg.Add(SetRotationMessage(HumanBodyBones.RightIndexIntermediate));
            msg.Add(SetRotationMessage(HumanBodyBones.RightIndexDistal));
            msg.Add(SetRotationMessage(HumanBodyBones.RightMiddleProximal));
            msg.Add(SetRotationMessage(HumanBodyBones.RightMiddleIntermediate));
            msg.Add(SetRotationMessage(HumanBodyBones.RightMiddleDistal));
            msg.Add(SetRotationMessage(HumanBodyBones.RightRingProximal));
            msg.Add(SetRotationMessage(HumanBodyBones.RightRingIntermediate));
            msg.Add(SetRotationMessage(HumanBodyBones.RightRingDistal));
            msg.Add(SetRotationMessage(HumanBodyBones.RightLittleProximal));
            msg.Add(SetRotationMessage(HumanBodyBones.RightLittleIntermediate));
            msg.Add(SetRotationMessage(HumanBodyBones.RightLittleDistal));
            return msg;
        }

        // TODO: Implement this.
        Bundle SetBlendshapes()
        {
            var msg = new Bundle();
            return msg;
        }

        Message SetRotationMessage(HumanBodyBones humanBodyBones)
        {
            var boneRotation = _avatarAnimator.GetBoneTransform(humanBodyBones).localRotation;
            return new Message(humanBodyBones.ToString(), boneRotation.x, boneRotation.y, boneRotation.z, boneRotation.w);
        }
    }
}
