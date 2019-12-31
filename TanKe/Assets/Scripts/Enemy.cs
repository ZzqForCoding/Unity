using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed = 3;
    //拿到坦克的SpriteRenderer组件改变图片进而实现坦克的转向
    private SpriteRenderer sr;
    //存储坦克图片,不用去声明几个位置,直接拖入几个图片,数组大小就是几
    public Sprite[] tankSprite;     //(上右下左)
    //子弹
    public GameObject bulletPrefab;
    //子弹在各个方向的朝向
    private Vector3 bulletEulerAngles;
    //控制攻击CD
    private float timeVal;
    //爆炸特效
    public GameObject explosionPrefab;
    //敌人的攻击间隔
    private float AttackTimeVal;
    //计时器
    private float timeValChangeDirection;
    //控制垂直的行走
    private float v = -1;
    //控制水平的行走
    private float h;

    //赋值，开始就会调用
    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start() {
        AttackTimeVal = Random.Range(1.0f, 5.0f);
    }

    // Update is called once per frame
    void Update() {
        //攻击时间间隔的判定
        if (timeVal >= AttackTimeVal) {
            Attack();
            AttackTimeVal = Random.Range(1.0f, 6.0f);
        } else {
            timeVal += Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        Move();
    }

    //坦克的攻击方法
    public void Attack() {
            //子弹产生的角度:当前坦克的角度 + 子弹各个方向的朝向
            //第三个参数是四元数(transform中的rotation是欧拉角,Quaternion.Euler()这个方法是将欧拉角转成四元数)(别管是什么,也是旋转的一个单位)
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
    }

    //坦克的移动方法
    private void Move() {
        if(timeValChangeDirection >= 4) {
            int num = Random.Range(0, 8);
            if(num > 5) {
                v = -1;
                h = 0;
            } else if(num  == 0) {
                v = 1;
                h = 0;
            } else if(num > 0 && num <= 2) {
                h = -1;
                v = 0;
            } else if(num > 2 && num <= 4) {
                h = 1;
                v = 0;
            }
            timeValChangeDirection = 0;
        } else {
            timeValChangeDirection += Time.fixedDeltaTime;
        }

        //第二个参数代表以世界坐标轴来移动
        transform.Translate(Vector3.up * speed * v * Time.fixedDeltaTime, Space.World);

        if (v > 0) {
            //切换为向上的图片
            sr.sprite = tankSprite[0];
            //改变坦克子弹的朝向为向上
            bulletEulerAngles = new Vector3(0, 0, 0);
        } else if (v < 0) {
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }

        //当同时按下两个键就不处理水平方向的移动
        if (v != 0) return;

        transform.Translate(Vector3.right * speed * h * Time.fixedDeltaTime, Space.World);
        if (h < 0) {
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        } else if (h > 0) {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
    }

    //坦克的死亡方法
    private void Die() {
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }
 
    //若敌人和敌人碰撞了,就促使他们转向
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            timeValChangeDirection = 4;
        }
    }

}
