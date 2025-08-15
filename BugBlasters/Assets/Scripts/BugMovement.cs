using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BugBlasters
{
    // BUG DAMAGE CONTROLLER AND BUG HEALTH CONTROLER
    public class BugMovement : MonoBehaviour
    {
        public Transform target;
        public float smoothTime = 0.3F;
        private Vector3 velocity = Vector3.zero;
        public enum LadyBugType { LadyBugBlack = 0, LadyBugGreen = 1 }
        public LadyBugType LadyBugT;
        public int LadyBugHealth;
        public bool AttackTarget = false;
        public float AttackPower;
        void Start() { }
        void Update()
        {
            if (!AttackTarget)
                transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Device")
            {
                AttackTarget = true;
                GamePlayManager.GamePlayManagerInstance.UpdateDeviceHealthFill(AttackPower);
                AudioManager.AudioManagerInstance.PlayCrackAction();
                this.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                Invoke("EnableDeathEffect", 0.05f);
            }
        }
        public void LadyBugTakeDamage(int TapDamage)
        {
            LadyBugHealth -= TapDamage;
            this.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            AudioManager.AudioManagerInstance.PlayTapAction();
            if (LadyBugHealth <= 0)
            {
                this.GetComponent<SpriteRenderer>().enabled = false;
                GamePlayManager.GamePlayManagerInstance.UpdateKillCounter(1);
                Invoke("EnableDeathEffect", 0.05f);
            }
        }
        void OnMouseDown()
        {
            LadyBugTakeDamage(1);
        }
        public void EnableDeathEffect()
        {
            Destroy(this.gameObject);
        }
    }
}