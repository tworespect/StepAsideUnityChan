using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

	//アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;
	//Unityちゃんを移動させるコンポーネントを入れる
	private Rigidbody myRigidbody;
	//前進するための力
	private float forwardForce = 800.0f;
	//左右に移動するための力
	private float turnForce = 500.0f;
	//ジャンプするための力
	private float upForce = 500.0f; 
	//左右に移動できる範囲
	private float movableRange = 3.4f;

	//動きを減速させる係数
	private float coefficient = 0.95f;

	//ゲーム終了時の判定
	private bool isEnd = false;

	//ゲーム終了時に表示するテキスト
	private GameObject stateText;

	//スコアを表示するテキスト
	private GameObject scoreText;

	//得点
	private int score = 0;

	//左ボタン押下の判定
	private bool isLButtonDown = false;

	//右ボタン押下の判定
	private bool isRButtonDown = false;


	// Use this for initialization
	void Start () {

		//Animatorコンポーネントを取得する
		this.myAnimator = GetComponent<Animator>();

		//走るアニメーションを開始
		this.myAnimator.SetFloat ("Speed" , 1);

		//Rigidbdyコンポーネントを取得する
		this.myRigidbody = GetComponent<Rigidbody>();

		//シーン中のstateTextオブジェクトを取得
		this.stateText = GameObject.Find("GameResultText");

		//シーン中のscoreTextを取得
		this.scoreText = GameObject.Find("ScoreText");

	}


	// Update is called once per frame
	void Update () {

		//ゲーム終了ならUnityちゃんの動きを減速する
		if (this.isEnd) {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAnimator.speed *= this.coefficient;
		}


		//Unityちゃんを前方向の力を加える
		this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);


		//unityちゃんを矢印キーまたはボタンに応じて左右に移動させる
		if ((Input.GetKey (KeyCode.LeftArrow)||this.isLButtonDown) && -this.movableRange < this.transform.position.x) {
			//左へ移動
			this.myRigidbody.AddForce (-this.turnForce, 0, 0);
		}else if ((Input.GetKey(KeyCode.RightArrow)||this.isRButtonDown) && this.transform.position.x < this.movableRange){
			//右へ移動
			this.myRigidbody.AddForce(this.turnForce,0,0);

		}

		//Jumpステートの場合はJumpにfalseをセット
		//GetCurrentAnimatorStateInfo(0)で現在のアニメーションの状態を取得し、
		//「IsName」関数で取得したステートの名前が引数の文字列と一致してるか調べる
		//setBool関数は、第一引数に与えられたパラメータに、第二引数の値に代入する関数

		if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("Jump")) {
			this.myAnimator.SetBool ("Jump", false);
		}

		//ジャンプしていない時にスペースが押されたらジャンプする
		if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f) {
			//ジャンプアニメを再生（追加）
			this.myAnimator.SetBool ("Jump", true);
			//Unityちゃんに上方向の力を加える
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}


	}
		//トリガーモードで他のオブジェクトと衝突した場合の処理
	void OnTriggerEnter(Collider other){

		//障害物に衝突した場合

		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
			this.isEnd = true;
			//GAMEOVErを表示
			this.stateText.GetComponent<Text>().text = "GAME OVER...";

		}

		//ゴール地点に到着した場合
		if (other.gameObject.tag == "GoalTag") {

			this.isEnd = true;

			//GOALに到着したら表示
			this.stateText.GetComponent<Text>().text = "CLEAR!!";

		}

		//コインに衝突した場合

		if (other.gameObject.tag == "CoinTag") {


			//スコアを追加
			this.score += 10;

			//獲得した点数を表示
			this.scoreText.GetComponent<Text>().text = "Score  " + this.score + "pt";

			//パーティクル再生
			GetComponent<ParticleSystem>().Play();
		

			//衝突したコインを破棄する
			Destroy (other.gameObject);

		}
			}

		//ジャンプボタンを押した場合の処理
	public void GetMyJumpButtonDown(){
		if(this.transform.position.y < 0.5f){
			this.myAnimator.SetBool ("Jump", true);
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}
	//左ボタンを押し続けた場合の処理（追加）
	public void GetMyLeftButtonDown() {
		this.isLButtonDown = true;
	}
	//左ボタンを離した場合の処理（追加）
	public void GetMyLeftButtonUp() {
		this.isLButtonDown = false;
	}

	//右ボタンを押し続けた場合の処理（追加）
	public void GetMyRightButtonDown() {
		this.isRButtonDown = true;
	}
	//右ボタンを離した場合の処理（追加）
	public void GetMyRightButtonUp() {
		this.isRButtonDown = false;



	}



}