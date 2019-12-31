using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    //单例模式
    private static PlayerManager instance;

    public static PlayerManager Instance {
        get {
            return instance;
        }

        set {
            instance = value;
        }
    }

    //玩家一
    //生命值
    public int lifeValueOne = 3;
    //得分
    public int playerScoreOne = 0;
    //杀的敌人数图片
    public Text PlayerScoreTextOne;
    //生命值图片
    public Text playerLifeValueTextOne;
    //是否死亡
    public bool isDeadOne;

    //玩家二
    //生命值
    public int lifeValueTwo = 3;
    //得分
    public int playerScoreTwo = 0;
    //杀的敌人数图片
    public Text PlayerScoreTextTwo;
    //生命值图片
    public Text playerLifeValueTextTwo;
    //是否死亡
    public bool isDeadTwo;

    //游戏是否失败
    public bool isDefeat;
    //出生特效
    public GameObject born;
    //游戏失败图片
    public GameObject isDefeatUI;



    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		if(Option.choice == 2) {
            PlayerScoreTextTwo.transform.parent.gameObject.SetActive(true);
            playerLifeValueTextTwo.transform.parent.gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(Option.choice == 2 && lifeValueOne == 0 && lifeValueTwo == 0 || Option.choice == 1 && lifeValueOne == 0) {
            //游戏失败,返回主界面
            isDefeat = true;
            //延时三秒调用名为第一个参数的方法
            Invoke("ReturnToTheMenu", 3);
        } else if(isDeadOne && lifeValueOne > 0) {
            Recover(1);
        } else if(isDeadTwo && lifeValueTwo > 0) {
            Recover(2);
        }
        PlayerScoreTextOne.text = playerScoreOne.ToString();
        playerLifeValueTextOne.text = lifeValueOne.ToString();
        if(Option.choice == 2) {
            PlayerScoreTextTwo.text = playerScoreTwo.ToString();
            playerLifeValueTextTwo.text = lifeValueTwo.ToString();
        }
        if (isDefeat) {
            isDefeatUI.SetActive(true);
            //延时三秒调用名为第一个参数的方法
            Invoke("ReturnToTheMenu", 3);
            return;
        }
	}

    private void Recover(int player) {
        if (player == 1) {
            lifeValueOne--;
            isDeadOne = false;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = 1;
        } else {
            lifeValueTwo--;
            isDeadTwo = false;
            GameObject go = Instantiate(born, new Vector3(2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = 2;
        }
    }

    public void addScore(GameObject b) {
        if(b.tag == "oneBullet") {
            playerScoreOne++;
        } else if(b.tag == "twoBullet") {
            playerScoreTwo++;
        }
    }

    private void ReturnToTheMenu() {
        SceneManager.LoadScene(0);
    }

}
