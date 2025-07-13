using VRM;

namespace HumanoidPoseConnector
{
    public static class BlendshapeUtils
    {
        public static BlendShapePreset VRoidDefualtToVRMDefault(string name) => name switch
        {
            "Fcl_ALL_Neutral" => BlendShapePreset.Neutral,
            "Fcl_MTH_A" => BlendShapePreset.A,
            "Fcl_MTH_I" => BlendShapePreset.I,
            "Fcl_MTH_U" => BlendShapePreset.U,
            "Fcl_MTH_E" => BlendShapePreset.E,
            "Fcl_MTH_O" => BlendShapePreset.O,
            "Fcl_EYE_Close" => BlendShapePreset.Blink,
            "Fcl_EYE_Close_L" => BlendShapePreset.Blink_L,
            "Fcl_EYE_Close_R" => BlendShapePreset.Blink_R,
            "Fcl_ALL_Angry" => BlendShapePreset.Angry,
            "Fcl_ALL_Fun" => BlendShapePreset.Fun,
            "Fcl_ALL_Joy" => BlendShapePreset.Joy,
            "Fcl_ALL_Sorrow" => BlendShapePreset.Sorrow,
            _ => BlendShapePreset.Unknown,
        };
    }
}
