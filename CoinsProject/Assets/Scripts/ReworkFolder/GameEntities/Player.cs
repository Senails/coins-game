using UnityEngine;
using UnityEngine.EventSystems;

using static OptionsManager;
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
        var direction = findDirection();
        movePlayer(direction);
        recalculateImage(direction);
    }


    private void movePlayer(Vector2 Direction){
        _rb.velocity = Direction;
    }
    private void recalculateImage(Vector2 Direction){
        int imageIndex;
        if (Mathf.Abs(Direction.y) > Mathf.Abs(Direction.x)){
            imageIndex = (Direction.y>0)?1:0;
        }else{
            imageIndex = (Direction.x>0)?3:2;
        }
        if (Direction.magnitude==0) imageIndex = 0;

        for(int i=0; i<ImagesConteiner.transform.childCount;i++){
            var child = ImagesConteiner.transform.GetChild(i);
            child.gameObject.SetActive(i==imageIndex);
        }

        _indexActiveImage = imageIndex;
    }




    private Vector2 findDirection(){
        if (OptionsManager.MoveMode==MoveModeEnum.hybrid){
            Vector2 direction = findDirectionKeyboard();
            if (direction.magnitude==0){
                direction = findDirectionMouse();
            }
            return direction;
        }else if (OptionsManager.MoveMode==MoveModeEnum.keyboard){
            return findDirectionKeyboard();;
        }
        return findDirectionMouse();
    }
    private Vector2 findDirectionKeyboard(){
        Vector2 direction = new Vector2(){
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetAxisRaw("Vertical"),
        }.normalized;

        direction = Input.GetKey(KeyCode.LeftShift)?
        direction*MaxSpeed:
        direction*MinSpeed;

        return direction;
    }
    private Vector2 findDirectionMouse(){
        if (!Input.GetMouseButton(0)) return new Vector2(0,0);

        Vector2 mouseCords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 userCords = transform.position;
        Vector2 direction = (mouseCords - userCords).normalized;

        if (Input.GetKey(KeyCode.LeftShift)){
            return direction*MaxSpeed;
        }
        
        float speedKoef = (userCords-mouseCords).magnitude/(GetSizesGameObject(Camera.main).magnitude/8);
        speedKoef = Mathf.Clamp(speedKoef,0,1);

        return direction*speedKoef*MaxSpeed;
    } 
}
