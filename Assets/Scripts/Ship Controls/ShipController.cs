using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManualControls;

namespace ShipController

{
    public class ShipController : MonoBehaviour
    {
        [Header("Manual Controls")]
        [SerializeField]
        private Joystick RotateJoystick;
        [SerializeField]
        private Throttle VelocityThrottle;

        [Header("Ship Objects")]
        [SerializeField]
        private Transform ShipTransform;
        [SerializeField]
        private Rigidbody ShipRB;


        [Header("Stats")]
        [SerializeField]
        private float Velocity;
        [SerializeField]
        private float Axelaration;
        [SerializeField]
        private float max_velocity;

        [Header("Debug Values")]
        [SerializeField]
        private float time_t = 0;
        [SerializeField]
        float yaw = 0.0f;
        [SerializeField]
        float pitch = 0.0f;


        float velocity_t = 0;
        float acceleration_t = 0;
        Vector3 JoystickInput;
        float ThrottleInput;

        // Use this for initialization
        void Start()
        {
            velocity_t = Velocity;
            JoystickInput = RotateJoystick.GetJoystickOut();
            ThrottleInput = VelocityThrottle.GetThrottleOut();

        }

        void LateUpdate()
        {
            UpdateCoop();
        }

        // Update is called several times per frame
        void FixedUpdate()
        {
            FixedUpdateCoop(true);
        }

        private void UpdateCoop()
        {
            JoystickInput = RotateJoystick.GetJoystickOut();
            ThrottleInput = VelocityThrottle.GetThrottleOut();

            pitch = JoystickInput.y * 3;
            yaw = JoystickInput.x * -1;

            acceleration_t = Axelaration * ThrottleInput;
        }

        private void FixedUpdateCoop(bool move)
        {
            velocity_t += acceleration_t * Time.fixedDeltaTime;

            if (velocity_t <= Velocity)
                velocity_t = Velocity;
            else if (velocity_t > max_velocity)
                velocity_t = max_velocity;

            ShipTransform.Rotate(ShipTransform.right, pitch * Time.fixedDeltaTime, Space.World);

            ShipTransform.Rotate(ShipTransform.forward, yaw * Time.fixedDeltaTime, Space.World);

            if (move)
                ShipTransform.Translate(velocity_t * ShipTransform.forward * Time.fixedDeltaTime);
        }

        private void FixedUpdateFun(bool move)
        {
            ShipTransform.Rotate(ShipTransform.right, pitch * Time.fixedDeltaTime, Space.World);

            ShipTransform.Rotate(ShipTransform.forward, yaw * Time.fixedDeltaTime, Space.World);

            if(move)
                ShipTransform.Translate(velocity_t * ShipTransform.forward * Time.fixedDeltaTime);

            JoystickInput = RotateJoystick.GetJoystickOut();
            ThrottleInput = VelocityThrottle.GetThrottleOut();

            pitch = JoystickInput.y * 3;
            yaw = JoystickInput.x * -1;

            acceleration_t = Axelaration * ThrottleInput;

            velocity_t += acceleration_t * Time.fixedDeltaTime;


            if (velocity_t <= Velocity)
                velocity_t = Velocity;
            else if (velocity_t > max_velocity)
                velocity_t = max_velocity;
        }

        private void UpdateFun(bool move)
        {
            ShipTransform.Rotate(ShipTransform.right, pitch * Time.deltaTime, Space.World);

            ShipTransform.Rotate(ShipTransform.forward, yaw * Time.deltaTime, Space.World);

            if (move)
                ShipTransform.Translate(velocity_t * ShipTransform.forward * Time.deltaTime);

            JoystickInput = RotateJoystick.GetJoystickOut();
            ThrottleInput = VelocityThrottle.GetThrottleOut();

            pitch = JoystickInput.y * 3;
            yaw = JoystickInput.x * -1;

            acceleration_t = Axelaration * ThrottleInput;

            velocity_t += acceleration_t * Time.deltaTime;


            if (velocity_t <= Velocity)
                velocity_t = Velocity;
            else if (velocity_t > max_velocity)
                velocity_t = max_velocity;
        }
    }
}
