using UnityEngine;
using UnityEngine.UI;

    public class EVAShooting : MonoBehaviour
    {
        public int m_PlayerNumber = 1;
        [Range(1, 40)]
        public int m_TotalBullets = 10;
        public int countBullets;
        private PatrolMovement _patrolMovement;
        private WanderMovement _wanderMovement;
        public Rigidbody m_Shell;
        public Transform m_FireTransform;
        public Animator EVAAnimator;
    public Transform _spawnPoint;

    public GameObject target;
        public GameObject head;

        float cTime = 0.0f;
        float shotDelay = 2.0f;
        bool canShot = true;
        public float v = 30.0f;


        private void Start()
        {
            countBullets = m_TotalBullets;
            _patrolMovement = GetComponent<PatrolMovement>();
            _wanderMovement = GetComponent<WanderMovement>();
        }
    public void RechargeBullets()
    {
        Debug.Log("Recarga!");
        countBullets = m_TotalBullets;
    }

        private void Update()
        {
            m_FireTransform.transform.position = head.transform.position + m_FireTransform.forward; 
            m_FireTransform.transform.LookAt(target.transform.position);
            //
            //if (!canShot)
            //{
            //    cTime += Time.deltaTime;
            //    if (cTime >= shotDelay)
            //    {
            //        canShot = true;
            //        cTime = 0.0f;
            //    }
            //
            //}
            //
            //if (canShot)
            //{
            //    Fire();
            //    canShot = false;
            //}
            //
            //if (EVAAnimator != null) EVAAnimator.SetBool("isAttacking", false);
        }

        public void Fire()
        {
            if (countBullets>=1)
            {
                // Set the fired flag so only Fire is only called once.
                //m_Fired = true;
                if (EVAAnimator != null) EVAAnimator.SetBool("isAttacking", true);

                // Create an instance of the shell and store a reference to it's rigidbody.
                Rigidbody shellInstance =
                    Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

                // Set the shell's velocity to the launch force in the fire position's forward direction.
                shellInstance.velocity = v * m_FireTransform.forward;

                countBullets--;
            }
        }
    }