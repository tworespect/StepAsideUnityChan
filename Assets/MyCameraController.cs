using UnityEngine;
using System.Collections;

public class MyCameraController : MonoBehaviour {

	//Unityちゃんのオブジェクト
	private GameObject unitychan;
	//unityちゃんとカメラの距離
	private float difference;
	//左右に移動するための力


	// Use this for initialization
	void Start () {

		//unityちゃんのオブジェクトを取得
		this.unitychan = GameObject.Find("unitychan");
		//unityちゃんとカメラの位置（z座標）の差を求める
		this.difference = unitychan.transform .position.z - this.transform.position.z;
	
	
	}
	
	// Update is called once per frame
	void Update () {

		//unityちゃんの位置に合わせてカメラの位置を移動する
		this.transform.position = new Vector3(0,this.transform.position.y,this.unitychan.transform.position.z-difference);
	
	}
}
