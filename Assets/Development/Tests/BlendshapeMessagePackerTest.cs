using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using VRM;

namespace HumanoidPoseConnector.Tests {
    public class BlendshapeMessagePackerTest
    {
        [UnityTest]
        public IEnumerator VRoidDefaultBlendshapeGetingTest()
        {
            var avatarPrefab = Resources.Load<GameObject>("Avatars/DefauldBlendshapeAvatar/Nico");
            Assert.IsNotNull(avatarPrefab, "Avatar is not found.");
            var avatar = GameObject.Instantiate(avatarPrefab);
            yield return null;

            var proxy = avatar.GetComponent<VRMBlendShapeProxy>();
            var packer = new BlendshapeMessagePacker();
        }

        [UnityTest]
        public IEnumerator PerfectSyncBlendshapeGetingTest()
        {
            var avatarPrefab = Resources.Load<GameObject>("Avatars/PerfectSyncAvatar/violet_perfectSync");
            Assert.IsNotNull(avatarPrefab, "Avatar is not found.");
            var avatar = GameObject.Instantiate(avatarPrefab);
            yield return null;

            var proxy = avatar.GetComponent<VRMBlendShapeProxy>();
            var packer = new BlendshapeMessagePacker();
        }
    }
}
