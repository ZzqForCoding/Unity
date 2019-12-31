using UnityEngine;

public class Born : MonoBehaviour {
    //主角
    public GameObject[] playerPrefab;
    //敌人列表
    public GameObject[] enemyPrefabList;
    //true就产生玩家
    public int createPlayer;

    // Use this for initialization
    void Start() {
        //BornTank()(1s后调用)
        Invoke("BornTank", 1f);
        //然后调用完1s后就销毁
        Destroy(gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BornTank() {
        //控制坦克的产生
        if (createPlayer == 1) {
            //在爆炸特效的位置产生主角
            //第三个是旋转参数(初始角0, 0, 0)
            Instantiate(playerPrefab[0], transform.position, Quaternion.identity);
        } else if (createPlayer == 2) {
            Instantiate(playerPrefab[1], transform.position, Quaternion.identity);
        } else {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }
    }
}
