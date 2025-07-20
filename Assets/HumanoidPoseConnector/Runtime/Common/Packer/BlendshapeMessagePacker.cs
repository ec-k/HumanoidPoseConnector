using UnityEngine;
using uOSC;
using VRM;

namespace HumanoidPoseConnector
{
    public class BlendshapeMessagePacker
    {
        const string _address = "/VMC/Ext/Blend/Val";

        public Bundle PackBlendshapes(VRMBlendShapeProxy proxy)
        {
            var bundle = new Bundle();

            var shapes = proxy.GetValues();
            foreach (var shape in shapes)
            {
                var name = shape.Key.ToString();
                var weight = shape.Value;
                bundle.Add(new Message(_address, name, weight));
            }

            return bundle;
        }
    }
}
