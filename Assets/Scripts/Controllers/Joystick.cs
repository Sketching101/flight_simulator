using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class Joystick : MonoBehaviour
    {
        [Header("Joystick Visualizer")]
        [SerializeField] private Transform JoystickVis;

        [Header("Anchors")]
        [SerializeField] private Transform BaseAnchor;
        [SerializeField] private Transform GripAnchor;
        [SerializeField] private Transform TopAnchor;

        [Header("Grabbable Object")]
        [SerializeField] private OVRGrabbable GripObject;

        private float BaseToTopDist;

        [Header("Test Indicators")]
        [SerializeField] Vector3 BaseToGripNorm;
        Vector3 OriginalTopPosition;
        [SerializeField] float DistToOriginalTopPosition;

        float DistCovered = 0.0f;
        float speed = 1.5f;
        float startTime = 0.0f;

        bool GrabbedFlag = false;

        // Use this for initialization
        void Awake()
        {
            BaseToTopDist = (TopAnchor.localPosition - BaseAnchor.localPosition).magnitude;
            OriginalTopPosition = GripAnchor.localPosition;
            DistToOriginalTopPosition = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateValues();
            RotateJoystick();
            if(!GripObject.isGrabbed && GrabbedFlag)
            {
                GrabbedFlag = false;
                Debug.Log("Let go");
                StartCoroutine(OnLetGo());
            } else if(GripObject.isGrabbed && !GrabbedFlag)
            {
                GrabbedFlag = true;
            }

        }

        private void UpdateValues()
        {
            Vector3 NewBaseToGripNorm = (GripAnchor.localPosition - BaseAnchor.localPosition).normalized;
            if(NewBaseToGripNorm != BaseToGripNorm)
            {
                BaseToGripNorm = (GripAnchor.localPosition - BaseAnchor.localPosition).normalized;
                TopAnchor.localPosition = BaseToGripNorm * BaseToTopDist;
            }
            DistToOriginalTopPosition = (GripAnchor.localPosition - OriginalTopPosition).magnitude;
        }

        private void RotateJoystick()
        {
            JoystickVis.LookAt(GripAnchor.position, Vector3.forward);
        }

        private IEnumerator OnLetGo()
        {
            Vector3 StartPos = GripAnchor.localPosition;
            float journeyLength = Vector3.Distance(StartPos, OriginalTopPosition);
            startTime = Time.time;

            while (!GripObject.isGrabbed && DistToOriginalTopPosition > 0.01f)
            {
                Debug.Log("Looping");
                float distCovered = (Time.time - startTime) * speed;
                // Fraction of journey completed equals current distance divided by total distance.
                float fractionOfJourney = distCovered / journeyLength;
                GripAnchor.localPosition = Vector3.Lerp(StartPos, OriginalTopPosition, fractionOfJourney);
                yield return null;
            }
            Debug.Log("Distance Complete or grabbed");
            yield return null;
        }
    }
}