using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovement))]
public class GridMovementSubscriber : MonoBehaviour {

	public delegate Vector2 MovementMethod();
	public delegate bool AttackMethod();



	public enum MoveStates {
		NULL,		// NOT PROCESSED
		PROCESSING, // Has started processing
		IDLE,		// NOT MOVING
		MOVING, 	// PROCESSING
		FINISHED 	// CURRENTLY PROCESSING. 
	};

	// Movement delegate
	MovementMethod _movMethod = null;

	// Attack method
	AttackMethod _attackMethod = null;

	// Space in which movement is declared to.
	Vector2 declaredMovement = Vector2.zero;

	// State of the current enemy movement. 
	MoveStates state = MoveStates.NULL;

	// Processed the current state. 
	void ProcessState() 
	{
		// Check for null case
		if(declaredMovement == Vector2.zero) {
			state = MoveStates.IDLE;
		}

		else {
			// Do a raycast to check for player and environment collision. 
			// Shoot out a ray to check for a collision with the level layer. 
			RaycastHit2D hit = Physics2D.Raycast(
				transform.position + (Vector3)declaredMovement, // origin
				Vector2.up, 		// direction!
				0.1f, 		// Only check this cell unit on Grid
				LayerMask.GetMask("Level", "Player", "Enemies")
			); 	

			// Hit something. 
			if(hit.collider != null) {
				LayerMask mask = hit.collider.gameObject.layer;
				// Switch off the mask
				Debug.Log(mask);

				// Player layer hit. 
				if(mask == LayerMask.NameToLayer("Player")) {
					state = MoveStates.IDLE;
				}

				// Enemies layer hit
				else if(mask == LayerMask.NameToLayer("Enemies")) {
					Debug.Log("Hit an enemy");
					GridMovementSubscriber t_gms = 
						hit.collider.gameObject.GetComponent<GridMovementSubscriber>();

					if(t_gms != null) {

						MoveStates t_state = t_gms.GetState();
						// If it has not processed its mo

						// Force it to finish its movement. Recursive. 
						if(t_state == MoveStates.NULL) {
							t_gms.Move();
							ProcessState();
						}
						// If it hits this, then we've gotten into a recursive loop. Time to end it. 
						// Or it's just finished. Either way, it has already processed. 
						else {
							state = MoveStates.IDLE;
						}
					}
					// Else the enemy doesn't move. Therefore I can't move. 
					else {
						state = MoveStates.IDLE;
					}
				}

				// Level layer hit
				else if(mask == LayerMask.NameToLayer("Level")) {
					state = MoveStates.IDLE;
				}
			}
			else{
				state = MoveStates.MOVING;
			}
		}
		// Debug.Log(transform.name + ": State- " + state.ToString());
	}

	// Updates the movement compoent of the enemy. 
	public void Move()
	{
		bool attacked = false;

		// Start processing. 
		state = MoveStates.PROCESSING;


		// Calls attack method. Should return true if everything is cool
		if(_attackMethod != null) {
			attacked = _attackMethod();
			if(attacked){
				state = MoveStates.IDLE; // DO NOT MOVE IF TRUE
			}
		}

		// Only call movement if the enemy did not attack. 
		if(_movMethod != null && attacked == false) {
			// Grab movement from the _movMethod
			declaredMovement = _movMethod();

			// Upon exiting, it should either be IDLE or MOVING. 
			ProcessState();
		}

		if(state == MoveStates.MOVING) {
			transform.position += (Vector3)declaredMovement;
		}

		// LABEL AS FINISHED
		state = MoveStates.FINISHED;
	}

	public void SetMovementMethod(MovementMethod method) { _movMethod = method; }
	public void SetAttackMethod(AttackMethod method) { _attackMethod = method; }
	public MoveStates GetState() { return state; }
	public void SetState(MoveStates ms) { state = ms; }
}
