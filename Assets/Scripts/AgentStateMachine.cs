using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentStateMachine : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public NavMeshAgent NavMeshAgent { get { return _navMeshAgent; } }

    private Animator _animator;
    public Animator Animator { get { return _animator; } }

    [SerializeField] private Transform _target;
    public Transform Target { get { return _target; } }

    [SerializeField] private float _attackRange = 6f;
    public float AttackRange { get { return _attackRange; } }

    [SerializeField] private bool _isObstacle;
    public bool IsObstacle { get { return _isObstacle; } }

    [SerializeField] private bool _isRoofObstacle;
    public bool IsRoofObstacle { get { return _isRoofObstacle; } }

    [SerializeField] private float _speed;
    public float Speed { get { return _speed; } }

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _speed = _navMeshAgent.speed;
    }

    private void Start()
    {
        SetupAgent("Goal");
    }

    public void SetupAgent(string targetTag)
    {
        _animator.SetBool("isRun", true);
        _target = GameObject.FindGameObjectWithTag(targetTag).transform;
        ChooseTarget(_target);
    }

    public void ChooseTarget(Transform target)
    {
        // Clear Target
        if (_target != null)
        {
            _target = null;
        }

        // Set Target
        _target = target;
    }

    public void RunState(bool isChase)
    {
        // Set Animation
        _animator.SetBool("isRun", isChase);
        transform.LookAt(_target);

        // Move to target
        if (isChase)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_target.position);
        }
        else
        {
            _navMeshAgent.isStopped = true;
        }
    }

    public void AttackState(bool isAttack)
    {
        if (isAttack)
        {
            _animator.SetTrigger("isAttack");
        }
        else
        {
            _animator.ResetTrigger("isAttack");
        }
    }

    public void JumpState()
    {
        _isObstacle = false;
        _animator.SetTrigger("isJump");
    }

    public void CrouchState(bool isCrouch)
    {
        _animator.SetBool("isCrouch", isCrouch);
        _animator.SetBool("isRun", !isCrouch);

        if (isCrouch)
        {
            _navMeshAgent.speed = 1f;
        }
        else
        {
            _navMeshAgent.speed = _speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            _isObstacle = true;
        }

        if (other.gameObject.CompareTag("RoofObstacle"))
        {
            _isRoofObstacle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RoofObstacle"))
        {
            _isRoofObstacle = false;
        }
    }
}
