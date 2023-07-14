using System;
using UnityEngine;

using static AsyncLib;

public class Player : MonoBehaviour
{
    public static Player Self;


    private Animator _animator;
    private Action<Action> _trotlingAttack = CreateTrotlingFunc(800);


    public int MaxHealth = 100;
    public int Health = 100;
    public int PowerAttack = 30;
    public float AttackRange = 2;


    public event Action<int,int> OnChengeHealth;
    public event Action OnDeath;


    private void Awake() {
        Self = this;
    }
    private void Start() {
        _animator = this.GetComponent<Animator>();
    }
    private void Update() {
        if (Input.GetKey(OptionsManager.Config.KyeDictionary["Атака"])){
            _trotlingAttack(Attack);
        }
    }


    private void Attack(){
        _animator.SetBool("isAttack",true);
        setTimeout(()=>{
            foreach(var enemy in Enemy_Fight.EnemyList) TryAttackEnemy(enemy);
        },100);
        setTimeout(()=>{
            _animator.SetBool("isAttack",false);
        },340);
    }
    private void TryAttackEnemy(Enemy_Fight enemy){
        Vector2 delta = enemy.transform.position - transform.position;
        if (delta.magnitude>AttackRange) return;
        delta = delta.normalized;
        Vector2 plyerDirection = PlayerMoveController.PlayerDirection.normalized;

        float agle1 = Mathf.Atan2(delta.x,delta.y)*180/MathF.PI;
        float agle2 = Mathf.Atan2(plyerDirection.x,plyerDirection.y)*180/MathF.PI;

        float deltaAngle = agle1-agle2;

        if (Math.Abs(deltaAngle)<60 || Math.Abs(deltaAngle)>300){
            enemy.RemoveHealth(PowerAttack);
        }
    }


    public void RemoveHealth(int count){
        this.Health-=count;
        
        if (this.Health<=0){
            this.Health = 0;
            OnDeath?.Invoke();
        }
        OnChengeHealth.Invoke(this.Health,this.MaxHealth);
    }
    public void AddHealth(int count){
        Health += count;
        Health =  Health > MaxHealth ? MaxHealth : Health;
        OnChengeHealth?.Invoke( Health, MaxHealth);
    }
}
