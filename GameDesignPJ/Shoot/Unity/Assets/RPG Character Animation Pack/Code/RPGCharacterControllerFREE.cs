using UnityEngine;
using System.Collections;
//using UnityEditor.Events;

public enum Weapon{
	UNARMED = 0,
	RELAX = 8
}

public class RPGCharacterControllerFREE : MonoBehaviour{
	#region Variables

	//Components
	Rigidbody rb;
	protected Animator animator;
	public GameObject target;
	[HideInInspector]
	public Vector3 targetDashDirection;
	public Camera sceneCamera;
	public bool useNavMesh = false;
	private UnityEngine.AI.NavMeshAgent agent;
	private float navMeshSpeed;
	public Transform goal;

	//jumping variables
	public float gravity = -1f;
	[HideInInspector]
	public bool canJump = false;
	bool isJumping = false;
	[HideInInspector]
	public bool isGrounded;
	public float jumpSpeed = 12;
	public float doublejumpSpeed = 12;
	bool doublejumping = true;
	[HideInInspector]
	public bool canDoubleJump = false;
	[HideInInspector]
	public bool isDoubleJumping = false;
	bool doublejumped = false;
	bool isFalling;
	bool startFall;
	float fallingVelocity = -1f;

	// Used for continuing momentum while in air
	public float inAirSpeed = 8f;
	float maxVelocity = 2f;
	float minVelocity = -2f;

	//rolling variables
	public float rollSpeed = 8;
	bool isRolling = false;
	public float rollduration;

	//movement variables
	[HideInInspector]
	public bool canMove = true;
	public float walkSpeed = 1.35f;
	float moveSpeed;
	public float runSpeed = 6f;
	float rotationSpeed = 40f;
	Vector3 inputVec;
	Vector3 newVelocity;

	//Weapon and Shield
	[HideInInspector]
	public Weapon weapon;
	int rightWeapon = 0;
	int leftWeapon = 0;
	[HideInInspector]
	public bool isRelax = false;

	//isStrafing/action variables
	[HideInInspector]
	public bool canAction = true;
	[HideInInspector]
	public bool isStrafing = false;
	[HideInInspector]
	public bool isDead = false;
	public float knockbackMultiplier = 1f;
	bool isKnockback;

	//inputs variables
	float inputHorizontal = 0f;
	float inputVertical = 0f;
	float inputDashVertical = 0f;
	float inputDashHorizontal = 0f;
	float inputBlock = 0f;
	bool inputLightHit;
	bool inputDeath;
	bool inputAttackR;
	bool inputAttackL;
	bool inputCastL;
	bool inputCastR;
	bool inputJump;

	#endregion

	#region Initialization and Inputs

	void Start(){
		//set the animator component
		animator = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody>();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.enabled = false;
	}

	void Inputs(){
		inputDashHorizontal = 0;//Input.GetAxisRaw("DashHorizontal");
		inputDashVertical = 0;//Input.GetAxisRaw("DashVertical");
		inputHorizontal = Input.GetAxis("Horizontal");
		inputVertical = 0;//Input.GetAxisRaw("Vertical");
		inputLightHit = false;//Input.GetButtonDown("LightHit");
		inputDeath = false;//Input.GetButtonDown("Death");
		inputAttackL = false;//Input.GetButtonDown("AttackL");
		inputAttackR = false;//Input.GetButtonDown("AttackR");
		inputCastL = false;//Input.GetButtonDown("CastL");
		inputCastR = false;//Input.GetButtonDown("CastR");
		inputBlock = 0;//Input.GetAxisRaw("TargetBlock");
		inputJump = false;//Input.GetButtonDown("Jump");
	}

	#endregion

	#region Updates

	void Update(){
		//make sure there is animator on character
		if(animator){
			Inputs();
			if(canMove && !isDead && !useNavMesh){
				CameraRelativeMovement();
			} 
			//Navmesh
		}
		//Pause
		if(Input.GetKeyDown(KeyCode.P)){
			if(Time.timeScale != 1){
				Time.timeScale = 1;
			}
			else{
				Time.timeScale = 0f;
			}
		}
	}

	void FixedUpdate(){
		CheckForGrounded();
		//apply gravity force
		//rb.AddForce(0, 0, 0, ForceMode.Acceleration);
		AirControl();
		//check if character can move
		if(canMove && !isDead){
			moveSpeed = UpdateMovement();  
		}
		//check if falling
		if(rb.velocity.y < fallingVelocity && useNavMesh != true){
			isFalling = true;
			animator.SetInteger("Jumping", 2);
			canJump = false;
		}
		else{
			isFalling = false;
		}
	}

	//get velocity of rigid body and pass the value to the animator to control the animations
	void LateUpdate(){
		if(!useNavMesh){
			//Get local velocity of charcter
			float velocityXel = transform.InverseTransformDirection(rb.velocity).x;
			float velocityZel = transform.InverseTransformDirection(rb.velocity).z;
			//Update animator with movement values
			animator.SetFloat("Velocity X", velocityXel / runSpeed);
			animator.SetFloat("Velocity Z", velocityZel / runSpeed);
			//if character is alive and can move, set our animator
			if(!isDead && canMove){
				if(moveSpeed > 0){
					animator.SetBool("Moving", true);
				}
				else{
					animator.SetBool("Moving", false);
				}
			}
		}
		else{
			animator.SetFloat("Velocity X", agent.velocity.sqrMagnitude);
			animator.SetFloat("Velocity Z", agent.velocity.sqrMagnitude);
			if(navMeshSpeed > 0){
				animator.SetBool("Moving", true);
			}
			else{
				animator.SetBool("Moving", false);
			}
		}
	}

	#endregion

	#region UpdateMovement

	void CameraRelativeMovement(){
		//converts control input vectors into camera facing vectors
		Transform cameraTransform = sceneCamera.transform;
		//Forward vector relative to the camera along the x-z plane   
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		//Right vector relative to the camera always orthogonal to the forward vector
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		if(!isRolling){
			targetDashDirection = inputDashHorizontal * right + inputDashVertical * -forward;
		}
		inputVec = inputHorizontal * right + inputVertical * forward;
	}

	//rotate character towards direction moved
	void RotateTowardsMovementDir(){
		if(inputVec != Vector3.zero && !isStrafing && !isRolling){
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputVec), Time.deltaTime * rotationSpeed);
		}
	}

	float UpdateMovement(){
		if(!useNavMesh){
			CameraRelativeMovement();
		}
		Vector3 motion = inputVec;
		if(isGrounded){
			//reduce input for diagonal movement
			if(motion.magnitude > 1){
				motion.Normalize();
			}
			if(canMove){
				//set speed by walking / running
				if(isStrafing){
					newVelocity = motion * walkSpeed;
				}
				else{
					newVelocity = motion * runSpeed;
				}
				//if rolling use rolling speed and direction
				if(isRolling){
				}
			}
		}
		else{
			//if we are falling use momentum
			newVelocity = rb.velocity;
		}
		if(!isStrafing || !canMove){
			RotateTowardsMovementDir();
		}
		if(isStrafing && !isRelax){
			//make character point at target
			Quaternion targetRotation;
			Vector3 targetPos = target.transform.position;
			targetRotation = Quaternion.LookRotation(targetPos - new Vector3(transform.position.x, 0, transform.position.z));
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, (rotationSpeed * Time.deltaTime) * rotationSpeed);
		}
		newVelocity.y = rb.velocity.y;
		rb.velocity = newVelocity;
		//return a movement value for the animator
		return inputVec.magnitude;
	}

	#endregion

	#region Jumping

	//checks if character is within a certain distance from the ground, and markes it IsGrounded
	void CheckForGrounded(){
		isGrounded = false;
	}

	void Jumping(){
	}

	public IEnumerator _Jump(){yield return null;}

	void AirControl(){
		if(!isGrounded){
			CameraRelativeMovement();
			Vector3 motion = inputVec;
			motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1) ? 0.7f : 1;
			//rb.AddForce(motion * inAirSpeed, ForceMode.Acceleration);
			//limit the amount of velocity we can achieve
			float velocityX = 0;
			float velocityZ = 0;
			if(rb.velocity.x > maxVelocity){
				velocityX = GetComponent<Rigidbody>().velocity.x - maxVelocity;
				if(velocityX < 0){
					velocityX = 0;
				}
				//rb.AddForce(new Vector3(-velocityX, 0, 0), ForceMode.Acceleration);
			}
			if(rb.velocity.x < minVelocity){
				velocityX = rb.velocity.x - minVelocity;
				if(velocityX > 0){
					velocityX = 0;
				}
				//rb.AddForce(new Vector3(-velocityX, 0, 0), ForceMode.Acceleration);
			}
			if(rb.velocity.z > maxVelocity){
				velocityZ = rb.velocity.z - maxVelocity;
				if(velocityZ < 0){
					velocityZ = 0;
				}
				//rb.AddForce(new Vector3(0, 0, -velocityZ), ForceMode.Acceleration);
			}
			if(rb.velocity.z < minVelocity){
				velocityZ = rb.velocity.z - minVelocity;
				if(velocityZ > 0){
					velocityZ = 0;
				}
				//rb.AddForce(new Vector3(0, 0, -velocityZ), ForceMode.Acceleration);
			}
		}
	}
	#endregion
	#region MiscMethods
	public void Attack(int attackSide){}
	public void AttackKick(int kickSide){}
	public void CastAttack(int attackSide){}
	public void GetHit(){}
	IEnumerator _Knockback(Vector3 knockDirection, int knockBackAmount, int variableAmount){yield return null;}
	IEnumerator _KnockbackForce(Vector3 knockDirection, int knockBackAmount, int variableAmount){yield return null;}
	public IEnumerator _Death(){
		yield return null;
	}
	public IEnumerator _Revive(){yield return null;}
	void Hit(){}
	void FootL(){}
	void FootR(){}
	void Jump(){}
	void Land(){}
	#endregion
	#region Rolling
	void Rolling(){}
	public IEnumerator _DirectionalRoll(float x, float v){yield return null;	}
	public IEnumerator _Roll(int rollNumber){yield return null;}
	#endregion
	#region _Coroutines
	public IEnumerator _LockMovementAndAttack(float delayTime, float lockTime){yield return null;}
	#endregion
}