#pragma strict
var bullet : Rigidbody;
var power : float = 1500;
var myAudio : AudioSource;

function Start () {
	
}

function Update () {
	if(Input.GetKey(KeyCode.Return)){
		var instance : Rigidbody = Instantiate(bullet, transform.position, transform.rotation);
		var fwd : Vector3 = transform.TransformDirection(Vector3.forward);
		instance.AddForce(fwd * power);
		myAudio.Play();
	}
}
