using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace HumanoidPoseConnector.Tests
{
    public class BlendshapeNameListTest
    {
        [Test]
        public void VRoidDefaultNameExistenceTest()
        {
            var path = "Assets/Resources/Avatars/DefauldBlendshapeAvatar/Nico.prefab";
            var avatar = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            Assert.IsNotNull(avatar, "Avatar is not found.");

            var faceObj = avatar.transform.Find("Face");
            var skm = faceObj.GetComponent<SkinnedMeshRenderer>();
            var nameList = BlendshapeNameList.VRoidDefaultParameterNames;

            foreach (var name in nameList)
            {
                var index = skm.sharedMesh.GetBlendShapeIndex(name);
                if (index < 0)
                    Assert.Fail($"index {index} is not exist.");
                    break;
            }
            Assert.Pass();
        }

        [Test]
        public void PerfectSyncNameExistenceTest()
        {
            var path = "Assets/Resources/Avatars/PerfectSyncAvatar/violet_perfectSync.prefab";
            var avatar = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            Assert.IsNotNull(avatar, "Avatar is not found.");

            var faceObj = avatar.transform.Find("Face");
            var skm = faceObj.GetComponent<SkinnedMeshRenderer>();
            var nameList = BlendshapeNameList.PerfectSyncParameterNames;

            foreach (var name in nameList)
            {
                var index = skm.sharedMesh.GetBlendShapeIndex(name);
                if (index < 0)
                    Assert.Fail($"index {index} is not exist.");
                break;
            }
            Assert.Pass();
        }
    }
}
