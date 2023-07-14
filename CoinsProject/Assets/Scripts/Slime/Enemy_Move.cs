using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameCordsLib;

public class Enemy_Move : MonoBehaviour
{
    public float Speed = 2.5f;
    public float AgrRadius = 7f;


    private Vector2 _startPosition;
    private Animator _animator;
    private Rigidbody2D _rb;
    private Enemy_Fight _enemy;



    private void Start(){
        _startPosition = transform.position;
        _animator = gameObject.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _enemy = gameObject.GetComponent<Enemy_Fight>();
    }

    
    private void Update(){
        Vector2 direction = new Vector2(0,0);
        if (!_enemy.IsDeath){
            if(CheckObjectOnScreen(gameObject)){
                direction = FindDirection();
            }else{
                transform.position = _startPosition;
            }
        }
        ChangeAnimation(direction);
        Move(direction);
    }

    private Vector2 FindDirection(){
        Vector2 toPlayerVector = Player.Self.transform.position - transform.position;
        if (toPlayerVector.magnitude<AgrRadius){
            return toPlayerVector.normalized;
        }
        Vector2 toSpawnVector = _startPosition - (Vector2)transform.position;

        if (toSpawnVector.magnitude < (toSpawnVector.normalized.magnitude*3)){
            if (toSpawnVector.magnitude<0.01) return new Vector2(0,0);
            return toSpawnVector;
        }

        return toSpawnVector.normalized;
    }
    private void Move(Vector2 direction){
        _rb.velocity = direction*Speed;
    }
    private void ChangeAnimation(Vector2 direction){
        _animator.SetFloat("activeSpeed",direction.magnitude);
        if(direction.magnitude==0) return;
        _animator.SetFloat("directionX",direction.x);
        _animator.SetFloat("directionY",direction.y);
    }
}
