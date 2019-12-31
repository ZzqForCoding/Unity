using UnityEngine;

public class Barrier : MonoBehaviour {
    //障碍物被击打的音效
    public AudioClip hitAudio;

    public void PlayerAudio() {
        //在当前时刻当前位置发出声音
        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
    }

}
