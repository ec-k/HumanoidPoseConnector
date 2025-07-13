using UnityEngine;
using uOSC;
using VRM;

namespace HumanoidPoseConnector
{
    public class BlendshapeMessagePacker
    {
        const string _address = "/VMC/Ext/Blend/Val";

        public Bundle PackAllBlendshapes(SkinnedMeshRenderer skm)
        {
            var bundle = new Bundle();

            var count = skm.sharedMesh.blendShapeCount;
            for (var i = 0; i < count; i++)
            {
                var name = skm.sharedMesh.GetBlendShapeName(i);
                var weight = skm.GetBlendShapeWeight(i);
                bundle.Add(new Message(_address, name, weight));
            }

            return bundle;
        }

        public Bundle PackVRMDefaultBlendshape(SkinnedMeshRenderer skm)
        {
            var bundle = new Bundle();

            var list = BlendshapeNameList.VRoidDefaultParameterNames;
            foreach (var name in list)
            {
                var presetName = BlendshapeUtils.VRoidDefualtToVRMDefault(name);
                if (presetName == BlendShapePreset.Unknown)
                    continue;

                var normalizedWeight = GetNormalizedWeight(name, skm);
                bundle.Add(new Message(_address, presetName.ToString(), normalizedWeight));
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
                var normalizedWeight = GetNormalizedWeight(name, skm);
                bundle.Add(new Message(_address, name, normalizedWeight));
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

        float GetNormalizedWeight(string name, SkinnedMeshRenderer skm)
        {
            var weight = GetBlendshapeWeight(name, skm);
            var normlaizedWeight = weight / 100f;
            return normlaizedWeight;
        }
    }
}
