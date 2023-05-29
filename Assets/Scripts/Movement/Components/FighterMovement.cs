using Movement.Commands;
using UI;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.Serialization;

namespace Movement.Components
{
    [RequireComponent(typeof(Rigidbody2D)), 
     RequireComponent(typeof(Animator)),
     RequireComponent(typeof(NetworkObject))]
    public sealed class FighterMovement : NetworkBehaviour, IMoveableReceiver, IJumperReceiver, IFighterReceiver
    {
        public static volatile Time time; 
        public float speed = 1.0f;
        public float jumpAmount = 1.0f;

        private NetworkVariable<float> vida = new NetworkVariable<float>(10);
        


        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private NetworkAnimator _networkAnimator;
        private Transform _feet;
        private LayerMask _floor;
        private LayerMask _player;

        public Healthbar _healthbar;
        public GameObject _health;

        private Vector3 _direction = Vector3.zero;
        private bool _grounded = true;
        
        private static readonly int AnimatorSpeed = Animator.StringToHash("speed");
        private static readonly int AnimatorVSpeed = Animator.StringToHash("vspeed");
        private static readonly int AnimatorGrounded = Animator.StringToHash("grounded");
        private static readonly int AnimatorAttack1 = Animator.StringToHash("attack1");
        private static readonly int AnimatorAttack2 = Animator.StringToHash("attack2");
        private static readonly int AnimatorHit = Animator.StringToHash("hit");
        private static readonly int AnimatorDie = Animator.StringToHash("die");

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _networkAnimator = GetComponent<NetworkAnimator>();

            _feet = transform.Find("Feet");
            _floor = LayerMask.GetMask("Floor");
            _player = LayerMask.GetMask("Fighter"); //si no funciona quitar

            this._healthbar.SetMaxHealth(this.vida.Value);  
            this._health.transform.SetParent(this.transform);
        }

        void Update()
        {
            if (!IsServer) return;

            _grounded = Physics2D.OverlapCircle(_feet.position, 0.1f, _floor);
            _networkAnimator.Animator.SetFloat(AnimatorSpeed, this._direction.magnitude);
            _networkAnimator.Animator.SetFloat(AnimatorVSpeed, this._rigidbody2D.velocity.y);
            _animator.SetBool(AnimatorGrounded, this._grounded);
        }

        void FixedUpdate()
        {
            _rigidbody2D.velocity = new Vector2(_direction.x, _rigidbody2D.velocity.y);
            setHealthBarClientRpc();
        }

        public void Move(IMoveableReceiver.Direction direction)
        {
            UpdateMoveServerRpc(direction);
        }

        [ServerRpc]
        public void UpdateMoveServerRpc(IMoveableReceiver.Direction direction)
        {
            if (direction == IMoveableReceiver.Direction.None)
            {
                this._direction = Vector3.zero;
                return;
            }

            bool lookingRight = direction == IMoveableReceiver.Direction.Right;
            _direction = (lookingRight ? 1f : -1f) * speed * Vector3.right;
            transform.localScale = new Vector3(lookingRight ? 1 : -1, 1, 1);
        }

        public void Jump(IJumperReceiver.JumpStage stage)
        {
            UpdateJumpServerRpc(stage);
        }
        [ServerRpc]
        public void UpdateJumpServerRpc(IJumperReceiver.JumpStage stage)
        {
            switch (stage)
            {
                case IJumperReceiver.JumpStage.Jumping:
                    if (_grounded)
                    {
                        float jumpForce = Mathf.Sqrt(jumpAmount * -2.0f * (Physics2D.gravity.y * _rigidbody2D.gravityScale));
                        _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    }
                    break;
                case IJumperReceiver.JumpStage.Landing:
                    break;
            }
        }

        
        
        public void Attack1()
        {
            _networkAnimator.SetTrigger(AnimatorAttack1);
        }


        public void Attack2()
        {
            _networkAnimator.SetTrigger(AnimatorAttack2);
        }


        public void TakeHit(float damage)
        {
            if (this.enabled == true)
            {
                takeHitServerRpc(damage);
                _networkAnimator.SetTrigger(AnimatorHit);

                if (this.vida.Value <= 0)
                {
                    Die();
                }
            }
        }

        [ServerRpc(RequireOwnership = false)]
        public void takeHitServerRpc(float damage)
        {
            this.vida.Value -= damage;
            this._healthbar.SetHealth(this.vida.Value);
            Debug.Log(this.vida.Value);


        }

        [ClientRpc]
        public void setHealthBarClientRpc()
        {
            this._healthbar.SetHealth(vida.Value);
        }

        public void Die()
        {
            _networkAnimator.SetTrigger(AnimatorDie);
            
            this.NetworkObject.Despawn();
        }
    }
}