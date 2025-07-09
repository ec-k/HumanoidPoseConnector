using UnityEngine;
using uOSC;

namespace HumanoidPoseConnector
{
    public class BlendshapeMessagePacker
    {
        const string _address = "/VMC/Ext/Blend/Val";

        public Bundle BundleVRMDefaultBlendshape(SkinnedMeshRenderer skm)
        {
            var bundle = new Bundle();

            var list = BlendshapeNameList.VRoidDefaultParameterNames;
            foreach (var name in list)
            {
                var weight = GetBlendshapeWeight(name, skm);
                bundle.Add(new Message(_address, name, weight));
            }

            bundle.Add(new Message("/VMC/Ext/Blend/Apply"));
            return bundle;
        }

        public Bundle BundlePerfectSyncBlendshape(SkinnedMeshRenderer skm)
        {
            var bundle = new Bundle();

            var list = BlendshapeNameList.PerfectSyncParameterNames;
            foreach (var name in list)
            {
                var weight = GetBlendshapeWeight(name, skm);
                bundle.Add(new Message(_address, name, weight));
            }

            bundle.Add(new Message("/VMC/Ext/Blend/Apply"));
            return bundle;
        }

        // NOTE: this returns -1 when "name" is not found.
        float GetBlendshapeWeight(string name, SkinnedMeshRenderer skm)
        {
            var index = skm.sharedMesh.GetBlendShapeIndex(name);
            var weight = skm.GetBlendShapeWeight(index);
            return weight;
        }
    }
}
