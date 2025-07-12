using uOSC;

namespace HumanoidPoseConnector
{
    public class VMCMessagePacker
    {
        const string AvailabilityMessageAddress = "/VMC/Ext/OK";
        const string TimeMessageAddress = "/VMC/Ext/T";

        public Bundle PackVMCMessage(Bundle mainMessageBundle, bool isAvailable, float time)
        {
            var bundle = new Bundle();
         
            var availabilityMsg = GenerateAvailabilityMessage(isAvailable);
            var timeMsg = GenerateTimeMessage(time);

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
