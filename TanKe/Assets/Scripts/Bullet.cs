using UnityEngine;

public class Bullet : MonoBehaviour {
    //子弹的移动速度
    public float moveSpeed = 10;
    //存储是否是自己的子弹
    public bool isPlayerBullet;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //第一个参数代表沿着自身y轴移动(因为坦克是根据y轴转向的),第二个参数代表世界坐标系
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
	}

    private void OnTriggerEnter2D(Collider2D collider) {
        switch(collider.tag) {
            case "Tank":
                if (!isPlayerBullet) {
                    //调用Tank标签的Die方法
                    collider.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                //调用Heart标签的Die方法
                collider.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if(isPlayerBullet) {
                    PlayerManager.Instance.addScore(gameObject);
                    collider.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                //墙被子弹打掉
                Destroy(collider.gameObject);
                //子弹也消失
                Destroy(gameObject);
                break;
            case "Barrier":
                if (isPlayerBullet) {
                    collider.SendMessage("PlayerAudio");
                }
                //子弹消失
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

}
