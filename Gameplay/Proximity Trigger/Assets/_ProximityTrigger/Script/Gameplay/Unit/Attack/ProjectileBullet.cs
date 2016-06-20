using System;
using UnityEngine;

namespace Gameplay.Unit.Attack
{
    public class ProjectileBullet : BaseBullet
    {
        [SerializeField]
        private float velocity = 10;

        private new Rigidbody rigidbody;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            base.DestroyBullet(0);
        }

        private void Update()
        {
            Vector3 movement = transform.forward*velocity*Time.deltaTime;
            rigidbody.MovePosition(rigidbody.position + movement);
        }
    }
}