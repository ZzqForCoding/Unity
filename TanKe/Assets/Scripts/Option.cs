using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour {

    public static int choice = 1;

    public Transform posOne;

    public Transform posTwo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W)) {
            choice = 1;
            transform.position = posOne.position;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            choice = 2;
            transform.position = posTwo.position;
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            //需先点击File->Build Settings->把场景按顺序拖进去(从0开始数)
            //加载第二个场景
            SceneManager.LoadScene(1);
        }
	}
}
