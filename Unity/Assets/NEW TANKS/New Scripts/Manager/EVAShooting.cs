﻿using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class EVAShooting : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify the different players.
        public Rigidbody m_Shell;                   // Prefab of the shell.
        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        //public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
        //public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
        //public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
        //public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.
        public Animator EVAAnimator;

        public GameObject target;
        public GameObject head;

        float cTime = 0.0f;
        float shotDelay = 2.0f;
        bool canShot = true;
        public float v = 30.0f;


        //private string m_FireButton;                // The input axis that is used for launching shells.
        //private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
        //private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
        //private bool m_Fired;                       // Whether or not the shell has been launched with this button press.
        //private WaitForSeconds m_WaitToFire;

        private void OnEnable()
        {
            // When the tank is turned on, reset the launch force and the UI
            //m_CurrentLaunchForce = 30;
            //m_AimSlider.value = m_MinLaunchForce;
        }


        private void Start()
        {
            // The fire axis is based on the player number.
            //m_FireButton = "Fire" + m_PlayerNumber;

            // The rate that the launch force charges up is the range of possible forces by the max charge time.
            //m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        }


        private void Update()
        {
            m_FireTransform.transform.position = head.transform.position + m_FireTransform.forward; 
            m_FireTransform.transform.LookAt(target.transform.position);

            if (!canShot)
            {
                cTime += Time.deltaTime;
                if (cTime >= shotDelay)
                {
                    canShot = true;
                    cTime = 0.0f;
                }

            }

            if (canShot)
            {
                Fire();
                //GameObject bl = Instantiate(bullet, turret.transform.position, Quaternion.identity) as GameObject;
                //bl.GetComponent<Rigidbody>().velocity = turret.forward * v;
                canShot = false;
            }

            if (EVAAnimator != null) EVAAnimator.SetBool("isAttacking", false);
            // The slider should have a default value of the minimum launch force.
            //m_AimSlider.value = m_MinLaunchForce;

            // If the max force has been exceeded and the shell hasn't yet been launched...
            /*if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
            {
                // ... use the max force and launch the shell.
                m_CurrentLaunchForce = m_MaxLaunchForce;

                m_WaitToFire = new WaitForSeconds(1);
                Fire();
            }
            // Otherwise, if the fire button has just started being pressed...
            else if (Input.GetButtonDown(m_FireButton))
            {
                // ... reset the fired flag and reset the launch force.
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;
            }
            // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
            else if (Input.GetButton(m_FireButton) && !m_Fired)
            {
                // Increment the launch force and update the slider.
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

                m_AimSlider.value = m_CurrentLaunchForce;
            }
            // Otherwise, if the fire button is released and the shell hasn't been launched yet...
            else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
            {
                // ... launch the shell.
                m_WaitToFire = new WaitForSeconds(1);
                Fire();
            }*/
        }


        private void Fire()
        {
            // Set the fired flag so only Fire is only called once.
            //m_Fired = true;
            if (EVAAnimator != null) EVAAnimator.SetBool("isAttacking", true);

            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance =
                Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = v * m_FireTransform.forward;

            // Reset the launch force.  This is a precaution in case of missing button events.
            //m_CurrentLaunchForce = m_MinLaunchForce;
        }
    }
}