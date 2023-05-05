using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class LidRotation : MonoBehaviour
    {
        [SerializeField] float _rotationFullOpenLid;
        [SerializeField] float _rotationFullCloseLid;

        [SerializeField] float _cameraPosFullOpenLid;
        [SerializeField] float _cameraPosFullCloseLid;

        void Update()
        {
            this.transform.localEulerAngles = RelativeLidRotation();
        }

        Vector3 RelativeLidRotation()
        {
            float _camPosition = CurrentCameraY();

            if (_camPosition > _cameraPosFullCloseLid) return Vector3.zero;

            if (_camPosition < _cameraPosFullOpenLid) return new Vector3(0, _rotationFullOpenLid, 0);

            float _lidRotRange = Mathf.Abs(_rotationFullOpenLid - _rotationFullCloseLid);

            float _rotRatio = Mathf.Abs((_camPosition - _cameraPosFullCloseLid) / (_cameraPosFullOpenLid - _cameraPosFullCloseLid));

            float _lidRotation = _lidRotRange * _rotRatio;

            return new Vector3(0, _lidRotation, 0);
        }

        float CurrentCameraY()
        {
            return Camera.main.transform.position.y;
        }
    }
}
