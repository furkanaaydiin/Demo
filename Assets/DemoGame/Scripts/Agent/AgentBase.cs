using System.Collections;
using DemoGame.Scripts.Components;
using DemoGame.Scripts.Pool.PoolObjects;
using DemoGame.Scripts.TargetSystem;
using DG.Tweening;
using UnityEngine;

namespace DemoGame.Scripts.Agent
{
    public class AgentBase : TargetBase
    {
        [Header("RepresentativeBaseValues")]
        [SerializeField] protected float characterSpeed;
        [SerializeField] protected float directionSpeed;
        [SerializeField] private float characterStrikingForce;
        private bool _isObjectTac;
        protected bool _isDead;
        protected virtual void CharacterMove(Vector2 movementDirection)
        {
        }

        protected virtual void Dead()
        {
            if (!(transform.position.y < -1f)) return;
            _isDead = true;
            GameOverTimer.OnTimeOver?.Invoke();
        }
        /// <summary>
        /// KARAKTERLERİMİZİN BİRBİRİNE UYGULADIKLARI İTME KDOU
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <param name="force"></param>
        /// <param name="character"></param>
        private void StrikingForce(Rigidbody rigidbody, float force, GameObject character)
        {
            if (_isObjectTac) return;
            var direction = (character.transform.position - transform.position).normalized;
            direction -= new Vector3(0f, direction.y, 0f);
            rigidbody.AddForce((direction * characterStrikingForce), ForceMode.Impulse);
            StartCoroutine(ForceCoroutine(rigidbody));
            _isObjectTac = true;
            StartCoroutine(ObjectTac());

        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Target") && collision.gameObject.layer == 6)
            {
                StrikingForce(collision.gameObject.GetComponent<Rigidbody>(), Random.Range(2f, 3f),
                    collision.gameObject);
            }
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Target") && other.gameObject.layer == 7)
            {
                GrowCharacterSize(gameObject.transform);
                other.GetComponent<Pickup>().OnTriggerDelete();
            }

            if (other.gameObject.CompareTag("Target") && other.gameObject.layer == 8)
            {
                StartCoroutine(SpeedObjectCoroutine());
                other.GetComponent<Pickup>().OnTriggerDelete();
            }
        }

        /// <summary>
        ///  KARAKTERLERİMİZİN SCALESİNİ BÜYÜTTÜKLERİ KOD  
        /// </summary>
        /// <param name="characterTransform"></param>
        private void GrowCharacterSize(Transform characterTransform)
        {
            var localScale = characterTransform.localScale;
            var targetScale = new Vector3(localScale.x + 0.1f, localScale.y + 0.1f
                , localScale.z + 0.1f);
            targetScale = new Vector3(Mathf.Clamp(targetScale.x, 1f, 3f),
                Mathf.Clamp(targetScale.y, 1f, 3f),
                Mathf.Clamp(targetScale.z, 1f, 3f));
            characterTransform.DOKill();
            characterTransform.DOScale(targetScale, 0.2f).SetEase(Ease.InBack);
        }
        /// <summary>
        ///  HIZLARINI BELİRLİ BİR SÜRE ARTTIRDIĞIMIZ KOD
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpeedObjectCoroutine()
        {
            characterSpeed += 2f;
            yield return new WaitForSeconds(10f);
            characterSpeed -= 2f;
        }
         /// <summary>
         ///  UYGULANAN İTME KODUNU BELİRLİ BİR SÜRE SONRA DURDURMA 
         /// </summary>
         /// <param name="rigidbody"></param>
         /// <returns></returns>
        private IEnumerator ForceCoroutine(Rigidbody rigidbody)
        {
            yield return new WaitForSeconds(.3f);
            rigidbody.velocity = Vector3.zero;
        }
        private IEnumerator ObjectTac()
        {
            yield return new WaitForSeconds(.3f);
            _isObjectTac = false;
        }
    }
}
