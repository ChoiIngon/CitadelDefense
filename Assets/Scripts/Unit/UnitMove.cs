using UnityEngine;
using System.Collections;

public class UnitMove : MonoBehaviour {
    public float speed;
	[HideInInspector]
    public Vector3 start;
	[HideInInspector]
    public Vector3 end;
    public float interpolate
    {
        get { return _interpolate; }
    }

    protected float _interpolate;
    protected float distance = 0.0f;

	public virtual void Init(Vector3 start, Vector3 end)
	{
		transform.position = start;
		this.start = start;
		this.end = end;
		distance = Vector3.Distance (start, end);
		_interpolate = 0.0f;
		if (0.0f >= distance) {
			_interpolate = 1.00001f;
		}
	}
}

/*
public class Movement_Jump : UnitMove
{
	
	float elapsedTime;
	float startY;
	float timeScale;
	float gravity;
	float jumpTime;
	float jumpStop;
	//Vector3 jumpPower;
	float jumpPower;
	float highestTime;
	public Movement_Jump(CharacterMove move, float jumpPower, float jumpTime, float jumpStop = 0.0f) : base(move)
	{
		velocity = move.velocity;
		this.elapsedTime = 0.0f;
		this.startY = move.Position.y;
		//this.gravity = Mathf.Pow(jumpPower.magnitude, 2.0f) * Mathf.Pow(jumpPower.y / jumpPower.magnitude, 2.0f) / (2.0f * jumpPower.y);
		this.gravity = Mathf.Pow(jumpPower, 2.0f) / (2.0f * jumpPower);
		//float time = jumpPower.magnitude * (jumpPower.y / jumpPower.magnitude) / this.gravity;
		highestTime = jumpPower / this.gravity;
		this.jumpTime = highestTime / (jumpTime * 0.5f);
		this.jumpPower = jumpPower;
		this.jumpStop = jumpStop;
	}

	public override void Update()
	{
		base.Update();
		if (elapsedTime > this.highestTime && 0.0f < jumpStop)
		{
			jumpStop -= Time.deltaTime;
			elapsedTime += Time.deltaTime * this.jumpTime * 0.1f;

			if(move.unit.State == XX_UNIT_STATE_TYPE.XX_UNIT_STATE_DAMAGE)
				move.unit.SubState = XX_UNIT_SUBSTATE_TYPE.XX_UNIT_SUBSTATE_KNOCKDOWN;
		}
		else
		{
			elapsedTime += Time.deltaTime * this.jumpTime;
		}
		//float height = jumpPower.magnitude * (jumpPower.magnitude / jumpPower.y) * elapsedTime - (0.5f * gravity * Mathf.Pow(elapsedTime, 2.0f));
		float height = jumpPower * elapsedTime - (0.5f * gravity * Mathf.Pow(elapsedTime, 2.0f));

		height += startY;
		if (height > move.Position.y)
		{
			this.velocity.y = Mathf.Abs(height - move.Position.y);
		}
		else
		{
			this.velocity.y = -1.0f * Mathf.Abs(height - move.Position.y);
		}

		Vector3 velocity = this.velocity;
		velocity.x *= Time.deltaTime;
		move.characterMoveController.move(velocity);

		if (0.0f >= velocity.y && move.isGrounded)
		{
			next.velocity = Vector3.zero;
			move.movement = next;
		}
	}
}
	*/
