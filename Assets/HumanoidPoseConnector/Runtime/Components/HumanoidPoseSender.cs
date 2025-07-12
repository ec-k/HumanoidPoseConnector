using uOSC;
using UnityEngine;

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
        [SerializeField] bool _isPerfaceSync = false;

        Animator _animator;
        SkinnedMeshRenderer _faceMeshRenderer;
        PoseMessagePacker _poseMsgPacker;
        BlendshapeMessagePacker _blendshapeMsgPacker;
        VMCMessagePacker _vmcMsgPacker;

        public bool IsAvailable => (_animator is not null) && (_faceMeshRenderer is not null);
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
            _faceMeshRenderer = _avatar.transform.Find("Face").GetComponent<SkinnedMeshRenderer>();
            if (_faceMeshRenderer is null)
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

            if (_sendPose) avatarMsg.Add(_poseMsgPacker.PackPoseMessageBundle(_animator));
            if(_sendHand) avatarMsg.Add(_poseMsgPacker.PackHandMessageBundle(_animator));
            if (_sendFacialBlendshapes)
                if (_isPerfaceSync)
                    avatarMsg.Add(_blendshapeMsgPacker.PackPerfectSyncBlendshape(_faceMeshRenderer));
                else
                    avatarMsg.Add(_blendshapeMsgPacker.PackVRMDefaultBlendshape(_faceMeshRenderer));

            var vmcMsg = _vmcMsgPacker.PackVMCMessage(avatarMsg, IsAvailable, Time.time);
            if(optionalMessage is not null)
                vmcMsg.Add(optionalMessage);
            _client.Send(vmcMsg);
        }
#nullable disable
    }
}
