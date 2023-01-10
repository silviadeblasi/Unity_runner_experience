using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class extrasMovement : MonoBehaviour
{
    //public List<Transform> waypoints;

    private NavMeshAgent _navMeshAgent;
    [SerializeField] private Collider _groundCollider;
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _stoppingDistance = 1f;
    private float _speed = 1;
    [Range(1, 500)] public float walkRadius;
    private bool _isNear;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();   
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if(_navMeshAgent != null)
        {
            _navMeshAgent.speed = _speed;
            SetDestination();
        }

    }

    // Update is called once per frame
    void Update()
    {
        _isNear = IsTargetWithinDistance(_stoppingDistance);
        if ((_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance) && !_isNear)
        {
            _navMeshAgent.isStopped = false;
            ChangeAnimation(_isNear);
            SetDestination();
        }
        else if((_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance) && _isNear)
        {
            Talk();
            //ChangeAnimation(_isNear);
        } else if ((_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance) && !_isNear)
        {
            _navMeshAgent.isStopped = false;
            ChangeAnimation(_isNear);
        }
    }


    private void SetDestination()
    {
       NavMeshPath path = new NavMeshPath();
       Vector3 RandomPosition = GetRandomPositionOnGround();
        //_navMeshAgent.CalculatePath(randomPosition, path);
        _navMeshAgent.SetDestination(RandomPosition);
        Debug.Log(_navMeshAgent.path);
    }

    private void Talk()
    {
        _navMeshAgent.isStopped = true;
        Vector3 targetDirection = _player.transform.position - transform.position;
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 150f * Time.deltaTime);
        if(transform.rotation.Equals(targetRotation))
         ChangeAnimation(_isNear);
    }

    private bool IsTargetWithinDistance(float distance)
    {
        return (_player.transform.position - transform.position).sqrMagnitude <= distance * distance;
    }

    private void ChangeAnimation(bool _isNear)
    {
        if (_isNear)
        {
            _animator.SetBool("isNear", true);

        }
        else
        {
            _animator.SetBool("isNear", false);
        }

    }

    public Vector3 GetRandomPositionOnGround()
    {
        Vector3 min = _groundCollider.bounds.min;
        Vector3 max = _groundCollider.bounds.max;
        return new Vector3(Random.Range(min.x, max.x), 2f, Random.Range(min.z, max.z));
    }
}
