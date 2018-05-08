using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportBehaviour : MonoBehaviour {


    public AudioClip soundEffect;
    protected AudioSource audioout;

    private void Start()
    {
        audioout = GameObject.Find("FXSource").GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update () {
		if(this.name == "transporterCogBolt" || this.name == "transporterBoltCog") {
			this.transform.GetChild(0).gameObject.SetActive(true);
			this.transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = true;

			if (!(audioout.isPlaying)) {
				audioout.PlayOneShot(soundEffect);
			}
		}
	}
}
