using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //第二个参数是时间,秒为单位
        Destroy(gameObject, 0.167f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
