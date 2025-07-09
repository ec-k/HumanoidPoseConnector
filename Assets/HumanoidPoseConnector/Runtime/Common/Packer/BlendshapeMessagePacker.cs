using UnityEngine;
using uOSC;

namespace HumanoidPoseConnector
{
    public class BlendshapeMessagePacker
    {
        const string _address = "/VMC/Ext/Blend/Val";


        public Bundle PackVRMDefaultBlendshape(SkinnedMeshRenderer skm)
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

        public Bundle PackPerfectSyncBlendshape(SkinnedMeshRenderer skm)
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

        float GetBlendshapeWeight(string name, SkinnedMeshRenderer skm)
        {
            // NOTE: The index below is -1 when "name" is not found.
            var index = skm.sharedMesh.GetBlendShapeIndex(name);
            var weight = skm.GetBlendShapeWeight(index);
            return weight;
        }
    }
}
