using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopOffset : MonoBehaviour
{
    [SerializeField] Vector3 _laptopPosMaxOffset;
    [SerializeField] Vector3 _laptopPosMinOffset;

    [SerializeField] float _cameraPosMaxOffset;
    [SerializeField] float _cameraPosMinOffset;

    void Update()
    {
        this.transform.position = RelativeOffset();
    }

    Vector3 RelativeOffset()
    {
        float _camPosition = CurrentCameraY();

        if (_camPosition > _cameraPosMinOffset) return _laptopPosMinOffset;

        if (_camPosition < _cameraPosMaxOffset) return _laptopPosMaxOffset;

        float ratio = Mathf.Abs((_cameraPosMaxOffset - _camPosition)/(_cameraPosMaxOffset - _cameraPosMinOffset));

        return _laptopPosMaxOffset - ( (_laptopPosMaxOffset - _laptopPosMinOffset) * ratio);
    }

    float CurrentCameraY()
    {
        return Camera.main.transform.position.y;
    }
}
