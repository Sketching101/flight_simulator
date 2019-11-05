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


        [Header("Mobility Stats")]
        [SerializeField]
        private float MinVelocity;
        [SerializeField]
        private float Axelaration;
        [SerializeField]
        private float max_velocity;

        bool Collided = false;

        [Header("Dmg and Health Stats")]
        [SerializeField]
        private float HP;
        [SerializeField]
        private float FireRate;
        [SerializeField]
        private Transform[] Canons;

        float DamageRate = 1f;


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
        void Awake()
        {
            velocity_t = MinVelocity;
        }

        void Update()
        {
            if (Collided)
                HP -= Time.deltaTime * DamageRate;
            Vector3 JoystickInput = RotateJoystick.GetJoystickOut();
            float ThrottleInput = VelocityThrottle.GetThrottleOut();

            pitch = JoystickInput.y;
            yaw = JoystickInput.x;

            acceleration_t = Axelaration * ThrottleInput;
            velocity_t += acceleration_t * Time.deltaTime;

            
            if (velocity_t <= MinVelocity)
                velocity_t = MinVelocity;
            else if (velocity_t > max_velocity)
                velocity_t = max_velocity;


            ShipTransform.position += velocity_t * ShipTransform.forward * Time.deltaTime;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ShipTransform.Rotate(ShipTransform.right, pitch / 10, Space.World);
            ShipTransform.Rotate(ShipTransform.forward, yaw * -1 / 10, Space.World);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "CrashCollision")
                Collided = true;
        }

        void OnTriggerExit(Collider other)
        {
            Collided = false;
        }
    }
}
