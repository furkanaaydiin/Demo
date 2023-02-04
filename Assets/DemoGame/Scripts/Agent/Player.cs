using DemoGame.Scripts.Components;
using UnityEngine;
using UnityEngine.AI;

namespace DemoGame.Scripts.Agent
{
    public class Player : AgentBase
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private  Rigidbody playerRigidbody;
        [SerializeField] private NavMeshAgent navMeshAgent;
      
        public void FixedUpdate()
        {
            if (_isDead)
                return;
            SetInput(joystick.Direction);
            Dead();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            ScoreHandler.Instance.Scored(10);
        }
      
        private void SetInput(Vector2 movementJoystickDirection)
        {
            if (_isDead)
                return;
            CharacterMove(movementJoystickDirection);
        }
      /// <summary>
      /// JOYSTİCK İLE HAREKET 
      /// </summary>
      /// <param name="movementJoystickDirection"></param>
        protected override void CharacterMove(Vector2 movementJoystickDirection)
        {
            var playerTransform = transform;
             playerRigidbody.MovePosition(playerRigidbody.position + playerTransform.forward * (characterSpeed * Time.deltaTime));
             if (movementJoystickDirection.magnitude > 0.01f)
            {
                var direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
                var lookRotation = Quaternion.LookRotation(direction);
                playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, lookRotation,
                    Time.deltaTime * directionSpeed);
                if (navMeshAgent.velocity.magnitude > 5)
                {
                        navMeshAgent.velocity = navMeshAgent.velocity.normalized;
                }
            }
        }
    }
}

       
