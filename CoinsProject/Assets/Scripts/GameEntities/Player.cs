using UnityEngine;
using UnityEngine.EventSystems;

using OptionsTypes;
using static GameCordsLib;

public class Player : MonoBehaviour
{
    public float MaxSpeed=5;
    public float MinSpeed=4;


    public GameObject ImagesConteiner;
    private int _indexActiveImage;
    private Rigidbody2D _rb;


    private void Start() {
        _rb = this.GetComponent<Rigidbody2D>();
    }
    private void Update(){
        if (GameMeneger.Status == GameMeneger.GameStatus.pause) return;

        Vector2 direction = FindDirection();
        MovePlayer(direction);
        if (direction.magnitude==0) return;
        RecalculateImage(direction);
    }


    private void MovePlayer(Vector2 Direction){
        _rb.velocity = Direction;
    }
    private void RecalculateImage(Vector2 Direction){
        int imageIndex = 
        Mathf.Abs(Direction.y) > Mathf.Abs(Direction.x)?
        (Direction.y>0 ? 1:0):
        (Direction.x>0 ? 3:2);

        if (Direction.magnitude==0) imageIndex = 0;

        for(int i=0; i<ImagesConteiner.transform.childCount;i++){
            var child = ImagesConteiner.transform.GetChild(i);
            child.gameObject.SetActive(i==imageIndex);
        }

        _indexActiveImage = imageIndex;
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
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetAxisRaw("Vertical"),
        }.normalized;
        
        return Input.GetKey(KeyCode.LeftShift)?
        direction*MaxSpeed:
        direction*MinSpeed;
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
