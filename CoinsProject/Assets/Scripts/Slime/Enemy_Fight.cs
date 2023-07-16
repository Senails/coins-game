using System;
using System.Collections.Generic;
using UnityEngine;


using static AsyncLib;

public class Enemy_Fight : MonoBehaviour
{
    public static List<Enemy_Fight> EnemyList = new List<Enemy_Fight>();

    public int MaxHealth = 10;
    public int Health = 10;
    public int PowerAttack = 0;
    public bool IsDeath = false;


    private Animator _animator;
    private Action<Action> _trotlingAttack = CreateTrotlingFunc(600);


    private void Start() {
        _animator = this.GetComponent<Animator>();
        EnemyList.Add(this);
    }
  
    private void OnCollisionStay2D(Collision2D other) {
        if (IsDeath) return;
        if (other.gameObject != Player.Self.gameObject) return;
        _trotlingAttack(Attack);
    }
    private void OnDestroy() {
        EnemyList.Remove(this);
    }


    private void Attack(){
        Player.Self.RemoveHealth(PowerAttack);
    }


    public void RemoveHealth(int count){
        this.Health-=count;
        
        if (this.Health<=0){
            this.Health = 0;
            Death();
        }
    }
    private void Death(){
        _animator.SetBool("isDeath",true);
        IsDeath = true;
        setTimeout(()=>{
            gameObject.SetActive(false);
        },600);
    }
}
