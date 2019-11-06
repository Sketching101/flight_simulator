﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManualControls;
using ShipControllerNS;

public class Projectile : MonoBehaviour {

    public GameObject cannonProjectile;
    public GameObject rocketProjectile;

    public Transform[] CannonSpawn;
    public Transform[] RocketSpawn;

    public Joystick joystick;
    public Throttle throttle;
    public ShipController shipController;

    public Aiming aimCalculator;

    int CannonTurn = 0;
    int RocketTurn = 0;

    public int MaxProjectiles = 10;
    int projectile_count = 0;
    public float CooldownPrimaryMax = 0.05f;

    public float time_primary = 0.0f;

    public bool CooldownPrimary = false;

    public float CooldownSecondaryMax = 1f;

    public float time_secondary = 0.0f;

    public bool CooldownSecondary = false;


    // Use this for initialization
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PrimaryFire();
        SecondaryFire();
    }

    private void PrimaryFire()
    {
        if (joystick.GrabbedBy != OVRInput.Controller.None && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, joystick.GrabbedBy) && !CooldownPrimary)
        {
            time_primary = CooldownPrimaryMax;
            CooldownPrimary = true;
            CannonTurn++;
            if (CannonTurn >= CannonSpawn.Length)
                CannonTurn = 0;
            else if (CannonTurn < 0)
                CannonTurn = CannonSpawn.Length - 1;
            Transform SpawnPos = CannonSpawn[CannonTurn];
            Vector3 dir = aimCalculator.FireDirectionPrimary(SpawnPos);
            Quaternion rot = aimCalculator.FireRotationPrimary(dir);
            GameObject clone = Instantiate(cannonProjectile, SpawnPos.position, rot) as GameObject;

            clone.GetComponent<Rigidbody>().velocity = dir * (300 + shipController.velocity_display);
            SFXManager.Instance.PlayClip(CannonTurn);
            projectile_count++;
        }

        if (CooldownPrimary)
        {
            time_primary -= Time.deltaTime;

            if (time_primary < 0)
            {
                CooldownPrimary = false;
            }
        }
    }

    private void SecondaryFire()
    {
        if (throttle.GrabbedBy != OVRInput.Controller.None && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, throttle.GrabbedBy) && !CooldownSecondary)
        {
            time_secondary = CooldownSecondaryMax;
            CooldownSecondary = true;
            RocketTurn++;
            if (RocketTurn >= RocketSpawn.Length)
                RocketTurn = 0;
            else if (RocketTurn < 0)
                RocketTurn = RocketSpawn.Length - 1;

            Vector3 TargetPos;

            Transform SpawnPos = RocketSpawn[RocketTurn];

            Vector3 dir = aimCalculator.FireDirectionSecondary(SpawnPos, out TargetPos);
            Quaternion rot = aimCalculator.FireRotationSecondary(dir);
            GameObject clone = Instantiate(rocketProjectile, SpawnPos.position, rot) as GameObject;
            clone.GetComponent<RocketController>().TargetPosition = TargetPos;

            clone.GetComponent<Rigidbody>().velocity = dir * 300;

            SFXManager.Instance.PlayClip(2 + RocketTurn);
        }

        if (CooldownSecondary)
        {
            time_secondary -= Time.deltaTime;

            if (time_secondary < 0)
            {
                CooldownSecondary = false;
            }
        }
    }
}
