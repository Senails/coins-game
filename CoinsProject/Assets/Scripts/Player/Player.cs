using UnityEngine;
using UnityEngine.EventSystems;

using OptionsTypes;
using static GameCordsLib;

public class Player : MonoBehaviour
{
    public float MaxSpeed=5;
    public float MinSpeed=4;


    private Animator _animator;
    private Rigidbody2D _rb;


    public static Player Self;

    private void Awake() {
        Self = this;
    }
    private void Start() {
        _rb = this.GetComponent<Rigidbody2D>();
        _animator = this.GetComponent<Animator>();
    }
    private void Update(){
        if (GameMeneger.Status == GameMeneger.GameStatus.pause) return;

        Vector2 direction = FindDirection();
        MovePlayer(direction);
        ChangeAnimation(direction);
    }


    private void MovePlayer(Vector2 Direction){
        _rb.velocity = Direction;
    }
    private void ChangeAnimation(Vector2 Direction){
        Vector2 vector = Direction.normalized;


        _animator.SetFloat("activeSpeed",vector.magnitude);
        if(vector.magnitude==0) return;
        _animator.SetFloat("directionX",vector.x);
        _animator.SetFloat("directionY",vector.y);
    }


    private Vector2 FindDirection(){
        if (OptionsManager.Config.MoveMode==MoveModeEnum.hybrid){
            Vector2 direction = FindDirectionKeyboard();
            if (direction.magnitude==0){
                direction = FindDirectionMouse();
            }
            return direction;
        }else if (OptionsManager.Config.MoveMode==MoveModeEnum.keyboard){
            return FindDirectionKeyboard();;
        }
        return FindDirectionMouse();
    }
    private Vector2 FindDirectionKeyboard(){
        Vector2 direction = new Vector2(){
            x = FindDirectionKeyboardX(),
            y = FindDirectionKeyboardY(),
        }.normalized;
        
        return Input.GetKey(KeyCode.LeftShift)?
        direction*MaxSpeed:
        direction*MinSpeed;
    }
    private float FindDirectionKeyboardX(){
        int i = 0;
        if (Input.GetKey(OptionsManager.Config.KyeDictionary["Вправо"])) i++;
        if (Input.GetKey(OptionsManager.Config.KyeDictionary["Влево"])) i--;
        return i;
    }
    private float FindDirectionKeyboardY(){
        int i = 0;
        if (Input.GetKey(OptionsManager.Config.KyeDictionary["Вверх"])) i++;
        if (Input.GetKey(OptionsManager.Config.KyeDictionary["Вниз"])) i--;
        return i;
    }
    private Vector2 FindDirectionMouse(){
        if (!Input.GetMouseButton(0)) return new Vector2(0,0);
        if (EventSystem.current.IsPointerOverGameObject()) return new Vector2(0,0);

        Vector2 mouseCords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 userCords = transform.position;
        Vector2 direction = (mouseCords - userCords).normalized;

        if (Input.GetKey(KeyCode.LeftShift)) return direction*MaxSpeed;
        
        float speedKoef = (userCords-mouseCords).magnitude/(GetSizesGameObject(Camera.main).magnitude/8);
        speedKoef = Mathf.Clamp(speedKoef,0,1);

        return direction*speedKoef*MaxSpeed;
    } 
}
