using uOSC;
using UnityEngine;

namespace HumanoidPoseConnector
{
    public class VMCMessagePacker
    {
        const string _availabilityMessageAddress = "/VMC/Ext/OK";
        const string _timeMessageAddress = "/VMC/Ext/T";

        Animator _avatarAnimator;
        SkinnedMeshRenderer _faceSkinMesh;
        PoseMessagePacker _poseMessagePacker;
        BlendshapeMessagePacker _blendshapeMessagePacker;

        bool _isPerfectSyncEnabled;

        public VMCMessagePacker(GameObject avatar, bool isPerfectSyncEnabled = false)
        {
            _avatarAnimator = avatar.GetComponent<Animator>();
            if (_avatarAnimator is null)
                Debug.LogError("Avatar animator is not found.");
            _faceSkinMesh = avatar.transform.Find("Face").GetComponent<SkinnedMeshRenderer>();
            if (_avatarAnimator is null)
                Debug.LogError("Avatar's face SkinnedMeshRenderer is not found.");

            _poseMessagePacker = new PoseMessagePacker();
            _blendshapeMessagePacker = new BlendshapeMessagePacker();
            _isPerfectSyncEnabled = isPerfectSyncEnabled;
        }

        public Bundle GenerateVMCMessage()
        {
            var bundle = new Bundle();

            var poseMsg = _poseMessagePacker.BundleHumanoidBones(_avatarAnimator);
            var blendshapeMsg = _isPerfectSyncEnabled
                ? _blendshapeMessagePacker.BundleVRMDefaultBlendshape(_faceSkinMesh)
                : _blendshapeMessagePacker.BundlePerfectSyncBlendshape(_faceSkinMesh);
            var availability = (_avatarAnimator is not null) && (_faceSkinMesh is not null);
            var availabilityMsg = GenerateAvailabilityMessage(availability);
            var timeMsg = GenerateTimeMessage(Time.time);

            bundle.Add(poseMsg);
            bundle.Add(blendshapeMsg);
            bundle.Add(availabilityMsg);
            bundle.Add(timeMsg);

            return bundle;
        }

        Message GenerateAvailabilityMessage(bool isAvailable)
        {
            int value = isAvailable ? 1 : 0;
            return new Message(_availabilityMessageAddress, value);
        }

        Message GenerateTimeMessage(float time)
            => new Message(_timeMessageAddress, time);
    }
}
