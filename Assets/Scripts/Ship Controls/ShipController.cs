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


        // Use this for initialization
        void Start()
        {
            velocity_t = Velocity;
        }

        void Update()
        {
            //Vector3 JoystickInput = RotateJoystick.GetJoystickOut();
            //float ThrottleInput = VelocityThrottle.GetThrottleOut();
            //Debug.Log(JoystickInput);
            
            //pitch = JoystickInput.y;
            //yaw = JoystickInput.x;

            //acceleration_t = Axelaration * ThrottleInput;
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            Vector3 JoystickInput = RotateJoystick.GetJoystickOut();
            float ThrottleInput = VelocityThrottle.GetThrottleOut();
            Debug.Log(JoystickInput);

            pitch = JoystickInput.y;
            yaw = JoystickInput.x;

            acceleration_t = Axelaration * ThrottleInput;
             ShipTransform.Rotate(ShipTransform.right, pitch / 10, Space.World);
             ShipTransform.Rotate(ShipTransform.forward, yaw * -1 /10, Space.World);

             velocity_t += acceleration_t * Time.fixedDeltaTime;
             
            /*
            if (velocity_t <= Velocity)
                velocity_t = Velocity;
            else if (velocity_t > max_velocity)
                velocity_t = max_velocity;*/


            //ShipTransform.position += velocity_t * ShipTransform.forward * Time.fixedDeltaTime;
        }
    }
}
