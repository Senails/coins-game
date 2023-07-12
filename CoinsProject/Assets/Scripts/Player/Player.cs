using System;
using UnityEngine;

using static AsyncLib;

public class Player : MonoBehaviour
{
    public static Player Self;


    private Animator _animator;
    private Action<Action> _trotlingAttack = CreateTrotlingFunc(800);


    public int MaxHealth;
    public int Health;
    public int PowerAttack;


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
            _animator.SetBool("isAttack",false);
        },340);
    }


    public void RemoveHealth(int count){
        this.Health-=count;
        
        if (this.Health<=0){
            this.Health = 0;
            OnDeath.Invoke();
        }
        OnChengeHealth.Invoke(this.Health,this.MaxHealth);
    }
    public void AddHealth(int count){
        Health += count;
        Health =  Health > MaxHealth ? MaxHealth : Health;
        OnChengeHealth?.Invoke( Health, MaxHealth);
    }
}
