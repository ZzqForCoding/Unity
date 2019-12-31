using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {
    //用来装饰地图的物体数组
    //0 老家 1 墙 2 障碍 3 出生效果 4 河流 5 草 6 空气墙
    public GameObject[] item;
    //已经有物体的坐标数组
    private List<Vector3> itemPositionList = new List<Vector3>();

    private void Awake() {
        InitMap();
    }

    private void InitMap() {
        //实例化老家
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //用墙把老家围起来
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i <= 1; i++) {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        //实例化外围空气墙
        for (int i = -10; i <= 10; i++) {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -10; i <= 10; i++) {
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i <= 8; i++) {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -8; i <= 8; i++) {
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }

        //初始化玩家一
        GameObject go1 = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        //设置产生的是玩家
        go1.GetComponent<Born>().createPlayer = 1;
        if (Option.choice == 2) {
            //初始化玩家二
            GameObject go2 = Instantiate(item[3], new Vector3(2, -8, 0), Quaternion.identity);
            //设置产生的是玩家
            go2.GetComponent<Born>().createPlayer = 2;
        }

        //产生敌人
        CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);

        //每个一段时间调用名为第一个参数的方法产生敌人
        InvokeRepeating("CreateEnemy", 4, 5);

        //实例化地图
        for (int i = 0; i < 60; i++) {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++) {
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++) {
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++) {
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
        }
    }

    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation) {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }

    //产生随机位置的方法
    private Vector3 CreateRandomPosition() {
        //不生成x = -10， 10的两列, y = 8 , -8两行的游戏物体
        while (true) {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8));
            if(!HasThePosition(createPosition)) {
                return createPosition;
            }
        }
    }

    //用来判断位置列表中是否有这个位置
    private bool HasThePosition(Vector3 createPos) {
        for (int i = 0; i < itemPositionList.Count; i++) {
            if (createPos == itemPositionList[i]) {
                return true;
            }
        }
        return false;
    }

    //一段时间就生成敌人
    private void CreateEnemy() {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        if(num == 0) {
            EnemyPos = new Vector3(-10, 8, 0);
        } else if(num == 1) {
            EnemyPos = new Vector3(0, 8, 0);
        } else {
            EnemyPos = new Vector3(10, 8, 0);
        }
        CreateItem(item[3], EnemyPos, Quaternion.identity);
    }

}
