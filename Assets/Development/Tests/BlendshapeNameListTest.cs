using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace HumanoidPoseConnector.Tests
{
    public class BlendshapeNameListTest
    {
        [UnityTest]
        public IEnumerator VRoidDefaultNameExistenceTest()
        {
            var avatarPrefab = Resources.Load<GameObject>("Avatars/DefauldBlendshapeAvatar/Nico");
            Assert.IsNotNull(avatarPrefab, "Avatar is not found.");
            var avatar = GameObject.Instantiate(avatarPrefab);
            yield return null;  // wait until avatar loading is completed.

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

        [UnityTest]
        public IEnumerator PerfectSyncNameExistenceTest()
        {
            var avatarPrefab = Resources.Load<GameObject>("Avatars/PerfectSyncAvatar/violet_perfectSync");
            Assert.IsNotNull(avatarPrefab, "Avatar is not found.");
            var avatar = GameObject.Instantiate(avatarPrefab);
            yield return null;  // wait until avatar loading is completed.

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
