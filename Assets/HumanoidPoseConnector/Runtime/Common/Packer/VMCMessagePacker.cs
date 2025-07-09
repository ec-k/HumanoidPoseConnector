using uOSC;
using UnityEngine;

namespace HumanoidPoseConnector
{
    public class VMCMessagePacker
    {
        const string AvailabilityMessageAddress = "/VMC/Ext/OK";
        const string TimeMessageAddress = "/VMC/Ext/T";

        Animator _avatarAnimator;
        SkinnedMeshRenderer _skm;

        public Bundle PackVMCMessage(Bundle mainMessageBundle)
        {
            var bundle = new Bundle();
         
            var availability = (_avatarAnimator is not null) && (_skm is not null);
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
            return new Message(AvailabilityMessageAddress, value);
        }

        Message GenerateTimeMessage(float time)
            => new Message(TimeMessageAddress, time);
    }
}
