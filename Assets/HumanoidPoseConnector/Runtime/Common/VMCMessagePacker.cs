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

        public VMCMessagePacker()
        { 
        }

        public Bundle GenerateVMCMessage(Bundle mainMessageBundle)
        {
            var bundle = new Bundle();
         
            var availability = (_avatarAnimator is not null) && (_faceSkinMesh is not null);
            var availabilityMsg = GenerateAvailabilityMessage(availability);
            var timeMsg = GenerateTimeMessage(Time.time);

            bundle.Add(mainMessageBundle);
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
