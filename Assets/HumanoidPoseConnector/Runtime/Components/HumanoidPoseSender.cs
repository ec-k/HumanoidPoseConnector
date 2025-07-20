using uOSC;
using UnityEngine;
using VRM;

namespace HumanoidPoseConnector
{
    [RequireComponent(typeof(uOscClient))]
    public class HumanoidPoseSender : MonoBehaviour
    {
        uOscClient _client;
        [SerializeField] GameObject _avatar;
        [SerializeField] float _sendingRate = 60f;
        [SerializeField] bool _sendPose = true;
        [SerializeField] bool _sendHand = true;
        [SerializeField] bool _sendFacialBlendshapes = true;

        Animator _animator;
        VRMBlendShapeProxy _blendshapeProxy;
        PoseMessagePacker _poseMsgPacker;
        BlendshapeMessagePacker _blendshapeMsgPacker;
        VMCMessagePacker _vmcMsgPacker;

        public bool IsAvailable { get; set; }
        float _throttleTimer = 0f;
        float _sendingInterval => 1 / _sendingRate;

        void Start()
        {
            _client = GetComponent<uOscClient>();
            _vmcMsgPacker = new();
            _poseMsgPacker = new();
            _blendshapeMsgPacker = new();

            _animator = _avatar.GetComponent<Animator>();
            if (_animator is null)
                Debug.LogError("Avatar animator is not found.");
            _blendshapeProxy = _avatar.GetComponent<VRMBlendShapeProxy>();
            if (_blendshapeProxy is null)
                Debug.LogError("Avatar's face SkinnedMeshRenderer is not found.");
        }

        void Update()
        {
            _throttleTimer += Time.deltaTime;
            if(_sendingInterval <= _throttleTimer)
            {
                SendAvatarPoseMessage();
                _throttleTimer = 0f;
            }
        }

#nullable enable
        void SendAvatarPoseMessage(Bundle? optionalMessage = null)
        {
            var avatarMsg = new Bundle(Timestamp.Now);

            if (_sendPose) 
                avatarMsg.Add(_poseMsgPacker.PackPoseMessageBundle(_animator));
            if(_sendHand) 
                avatarMsg.Add(_poseMsgPacker.PackHandMessageBundle(_animator));
            if (_sendFacialBlendshapes) 
                avatarMsg.Add(_blendshapeMsgPacker.PackBlendshapes(_blendshapeProxy));

            var vmcMsg = _vmcMsgPacker.PackVMCMessage(avatarMsg, IsAvailable, Time.time);
            if(optionalMessage is not null)
                vmcMsg.Add(optionalMessage);
            _client.Send(vmcMsg);
        }
#nullable disable
    }
}
