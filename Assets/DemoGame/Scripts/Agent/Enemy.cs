using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DemoGame.Scripts.TargetSystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace DemoGame.Scripts.Agent
{
    public class Enemy : AgentBase
    {
        public UnityAction OnDestroyed;
        [SerializeField] private NavMeshAgent navMeshAgent;
        private List<TargetBase> _targetArray = new();
        private bool _isCoroutineActive;
        private bool _canMove;
        private bool _isDestroyed;

        protected override void OnDestroy()
        {
            OnDestroyed?.Invoke();
            _isDestroyed = true;
            base.OnDestroy();
            StopAllCoroutines();
        }

        private void Start()
        {
            FindTargetAndMove(.5f);
        }

        public void FixedUpdate()
        {
            if (_isDead)
                return;
            Dead();
        }

        protected override void Dead()
        {
            if (!(transform.position.y < -1f)) return;
            _isDead = true;
            Destroy(gameObject);
        }

        private void FindTargetAndMove(float time)
        {
            if (_isCoroutineActive) StopAllCoroutines();
            else _isCoroutineActive = true;
            GetAllTargetsExceptItself();
            Invoke(nameof(TargetAndMoveCoroutine), time);
        }
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (!other.CompareTag("ExitCollider")) return;
            navMeshAgent.enabled = false;
            _canMove = false;
            StopAllCoroutines();
            var enemyControllers = TargetManager.GetAlLEnemies();
            enemyControllers.Remove(this);
            foreach (var enemyController in enemyControllers)
                enemyController.RemoveArray(gameObject);
        }

        public void TargetAndMoveCoroutine()
        {
            if (!navMeshAgent.enabled)
            {
                return;
            }

            _canMove = true;
            StartCoroutine(TargetAndMove());
        }

        public void RemoveArray(GameObject @object)
        {
            StopAllCoroutines();
            Invoke(nameof(RemoveArrayTimed), 0.3f);
        }
        void RemoveArrayTimed()
        {
            FindTargetAndMove(0f);
        }

        private void GetAllTargetsExceptItself()
        {
            if (_targetArray.Count > 0) _targetArray.Clear();
            _targetArray = TargetManager.GetAllActiveTargets().Where(x => x != this).ToList();
        }

        /// <summary>
        /// SAHNEDEKİ EN YAKIN HEDEFİ BULMASI
        /// </summary>
        private void ArrangeTargetsByDistance()
        {
            _targetArray = _targetArray.Where(x => x != null).OrderBy(i => Vector3.Distance(i.transform.position, transform.position))
                .ToList();
        }

        private void MoveToNearestTargets()
        {
            if (!_canMove) return;
            navMeshAgent.SetDestination(_targetArray[0].transform.position);
        }

        private IEnumerator TargetAndMove()
        {
            while (!_isDestroyed)
            {
                ArrangeTargetsByDistance();
                MoveToNearestTargets();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}