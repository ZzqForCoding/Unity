using UnityEngine;

public class Heart : MonoBehaviour {
    //当受到伤害依靠renderer组件切换图标
    private SpriteRenderer sr;
    //老家收到伤害后的图片
    public Sprite BrokenSprite;
    //爆炸特效
    public GameObject explosionPrefab;
    //老家死亡音效
    public AudioClip DieAudio;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Die() {
        //切换图片
        sr.sprite = BrokenSprite;
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeat = true;
        //在当前播放此音效
        AudioSource.PlayClipAtPoint(DieAudio, transform.position);
    }

}
