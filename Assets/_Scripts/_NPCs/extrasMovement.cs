using System;
using System.Collections;
using System.Collections.Generic;
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
            _navMeshAgent.SetDestination(RandomDestination());
        }

    }

    // Update is called once per frame
    void Update()
    {
        _isNear = IsTargetWithinDistance(_stoppingDistance);
        if (_navMeshAgent != null && !_isNear)
        {
            _navMeshAgent.speed = _speed;
            ChangeAnimation(_isNear);
            _navMeshAgent.SetDestination(RandomDestination());
        }
        else
        {
            _navMeshAgent.SetDestination(_navMeshAgent.transform.position);
            Stop();
            ChangeAnimation(_isNear);
        }
    }


    private Vector3 RandomDestination()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;

        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private void Stop()
    {
        _navMeshAgent.speed = 0;

        Vector3 targetDirection = _player.transform.position - transform.position;
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 150f * Time.deltaTime); 
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
}
