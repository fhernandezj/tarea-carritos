#pragma strict
var bullet : Rigidbody;
var power : float = 1500;

function Start () {
	
}

function Update () {
	if(Input.GetKey(KeyCode.X)){
		var instance : Rigidbody = Instantiate(bullet, transform.position, transform.rotation);
		var fwd : Vector3 = transform.TransformDirection(Vector3.forward);
		instance.AddForce(fwd * power);
	}
}
