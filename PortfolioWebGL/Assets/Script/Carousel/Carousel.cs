using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class Carousel : MonoBehaviour
    {
        [SerializeField] private float _scaleFactor = 1.25f;
        [SerializeField] private float _extrudeDistance = 1.5f;
        [SerializeField] private float zoomTime = 0.2f;
        [SerializeField] private float unzoomTime = 0.5f;

        private Dictionary<Transform, (Vector3, Vector3)> _originalTransforms = new Dictionary<Transform, (Vector3, Vector3)>();

        Transform currentHitTarget;
        Spinning currentSpinTarget;

        void Start()
        {
            MouseRaycast.ObjectHitEvent += OnObjectHit;
        }

        void OnObjectHit(GameObject hitObject)
        {
            if (hitObject == null)
            {
                if (currentSpinTarget != null)
                {
                    currentSpinTarget.StartSpin();
                    currentSpinTarget = null;
                }

                if (currentHitTarget != null)
                {
                    UnzoomChild(currentHitTarget);
                    currentHitTarget = null;
                }
            }
            else
            {
                if (!_originalTransforms.ContainsKey(hitObject.transform))
                    _originalTransforms[hitObject.transform] = (hitObject.transform.localPosition, hitObject.transform.localScale);

                if (currentHitTarget == hitObject.transform) return;

                if (!IsChildOfSpinningRow(hitObject.transform)) return;

                if(currentHitTarget != null) UnzoomChild(currentHitTarget);

                currentHitTarget = hitObject.transform;

                ZoomChild(currentHitTarget, _scaleFactor, _extrudeDistance);

                if (currentSpinTarget == GetSpinningParent(hitObject.transform)) return;

                if (currentSpinTarget != null) currentSpinTarget.StartSpin();

                currentSpinTarget = GetSpinningParent(hitObject.transform);

                currentSpinTarget.StopSpin();
            }
        }

        Spinning GetSpinningParent(Transform child)
        {
            if (child.parent == null)
                return null;

            if (!child.parent.TryGetComponent<Spinning>(out Spinning spin))
                return null;

            return spin;
        }
        
        bool IsChildOfSpinningRow(Transform child)
        {
            if (child.parent == null)
                return false;

            if (!child.parent.TryGetComponent<Spinning>(out Spinning spin))
                return false;

            return true;
        }

        void ZoomChild(Transform _target, float scaleFactor, float extrudeFactor)
        {
            if (!_originalTransforms.ContainsKey(_target)) return;

            var (originalPos, originalScale) = _originalTransforms[_target];

            StartCoroutine(ScaleOverTime(_target, originalScale * scaleFactor, AddValueTowardsOrigin(originalPos, extrudeFactor) , zoomTime));
        }

        void UnzoomChild(Transform _target)
        {
            if (!_originalTransforms.ContainsKey(_target)) return;

            var (originalPos, originalScale) = _originalTransforms[_target];

            StartCoroutine(ScaleOverTime(_target, originalScale, originalPos, unzoomTime));
        }

        IEnumerator ScaleOverTime(Transform target, Vector3 scale, Vector3 extrude, float time)
        {
            float elapsedTime = 0;
            Vector3 startingScale = target.localScale;
            Vector3 startingPos = target.localPosition;

            while (elapsedTime < time)
            {
                target.localPosition = Vector3.Lerp(startingPos, extrude, (2 * elapsedTime / time));
                target.localScale = Vector3.Lerp(startingScale, scale, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            target.localScale = scale;
        }

        public Vector3 AddValueTowardsOrigin(Vector3 target, float value)
        {
            if (target == Vector3.zero || value == 0) return Vector3.zero;

            Vector3 origin = Vector3.zero;
            Vector3 direction = (origin + target).normalized;
            
            return target + (direction * value);
        }

        void OnDestroy()
        {
            MouseRaycast.ObjectHitEvent -= OnObjectHit;
        }
    }
}