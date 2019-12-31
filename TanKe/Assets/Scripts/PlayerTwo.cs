using UnityEngine;

public class PlayerTwo : MonoBehaviour {
    public float speed = 3;
    //拿到坦克的SpriteRenderer组件改变图片进而实现坦克的转向
    private SpriteRenderer sr;
    //存储坦克图片,不用去声明几个位置,直接拖入几个图片,数组大小就是几
    public Sprite[] tankSprite;
    //子弹
    public GameObject bulletPrefab;
    //子弹在各个方向的朝向
    private Vector3 bulletEulerAngles;
    //控制攻击CD
    private float timeVal;
    //爆炸特效
    public GameObject explosionPrefab;
    //无敌状态(true代表是)
    private bool isDefended = true;
    //无敌时间(三秒)
    private float defendTimeVal = 3;
    //无敌特效
    public GameObject defendEffectPrefab;
    //AudioSource组件
    public AudioSource MoveAudio;
    //移动音效
    public AudioClip[] tankAudio;

    //赋值，开始就会调用
    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //无敌时间的判定
        if(isDefended) {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if(defendTimeVal <= 0) {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
	}

    private void FixedUpdate() {
        if(PlayerManager.Instance.isDefeat == true) {
            return;
        }
        Move();
        //攻击cd的判定
        if (timeVal >= 0.4f) {
            Attack();
        } else {
            timeVal += Time.fixedDeltaTime;
        }
    }

    //坦克的攻击方法
    public void Attack() {
        if(Input.GetKeyDown(KeyCode.KeypadEnter)) {
            //子弹产生的角度:当前坦克的角度 + 子弹各个方向的朝向
            //第三个参数是四元数(transform中的rotation是欧拉角,Quaternion.Euler()这个方法是将欧拉角转成四元数)(别管是什么,也是旋转的一个单位)
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
        }
    }

    //坦克的移动方法
    private void Move() {
        float v = Input.GetAxisRaw("PlayerTwoVertical");

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

        if(Mathf.Abs(v) > 0.05f) {
            MoveAudio.clip = tankAudio[1];
            if(!MoveAudio.isPlaying) {
                MoveAudio.Play();
            }
        }

        //当同时按下两个键就不处理水平方向的移动
        if (v != 0) return;

        float h = Input.GetAxisRaw("PlayerTwoHorizontal");

        transform.Translate(Vector3.right * speed * h * Time.fixedDeltaTime, Space.World);
        if (h < 0) {
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        } else if (h > 0) {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }

        if (Mathf.Abs(h) > 0.05f) {
            MoveAudio.clip = tankAudio[1];
            if (!MoveAudio.isPlaying) {
                MoveAudio.Play();
            }
        } else {
            MoveAudio.clip = tankAudio[0];
            if (!MoveAudio.isPlaying) {
                MoveAudio.Play();
            }
        }
    }

    //坦克的死亡方法
    private void Die() {
        if(isDefended) {
            return;
        }
        //玩家生命值-1
        PlayerManager.Instance.isDeadTwo = true;
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }

}
