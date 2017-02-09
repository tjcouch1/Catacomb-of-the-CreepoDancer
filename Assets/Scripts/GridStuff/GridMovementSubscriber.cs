using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovement))]
public class GridMovementSubscriber : MonoBehaviour {

	public delegate Vector2 MovementDeclarator();
	public delegate bool AttackMethod();

	static LayerMask p_laymask = LayerMask.GetMask("Player");
	static LayerMask e_laymask = LayerMask.GetMask("Enemies");
	static LayerMask l_laymask = LayerMask.GetMask("Level");

	public enum MoveStates {
		NULL,		// NOT PROCESSED
		IDLE,		// NOT MOVING
		MOVING, 	// MOVING
		PROCESSING 	// CURRENTLY PROCESSING. 
	};

	// Potentially caches a GMS for later processing
	GridMovementSubscriber cached_gms = null;	

	// Movement delegate
	MovementDeclarator _movMethod = null;

	// Attack method
	AttackMethod _attackMethod = null;

	// Space in which movement is declared to.
	Vector2 declaredMovement = Vector2.zero;

	// State of the current enemy movement. 
	MoveStates state = MoveStates.IDLE;

	public void ProcessState() 
	{
		if(cached_gms != null) {
			if(t_gms.GetState() == MoveStates.MOVING){
				// They're moving? Cool. Let's move. 
				state = MoveStates.MOVING;
			}
			else {
				// Else we're tenative about action. 
				state = MoveStates.TENATIVE;
				// cache the gms so we don't have to waste getting it later. 
				cached_gms = t_gms;
			}
		}

		// Check for null case
		if(declaredMovement == Vector2.zero) {
			state = MoveStates.IDLE;
		}

		else {
			// Do a raycast to check for player and environment collision. 
			// Shoot out a ray to check for a collision with the level layer. 
			RaycastHit2D hit = Physics2D.Raycast(
				transform.position + (Vector3)declaredMovement, // origin
				Vector2.up, 		// Lookup table (direction)!
				0.1f, 		// Only check this cell unit on Grid
				LayerMask.GetMask("Level", "Player", "Enemies")
			); 	

			if(hit.collider != null) {
				LayerMask mask = hit.collider.gameObject.layer;
				// Switch off the mask

				// Player layer hit. 
				if(mask == p_laymask) {
					state = MoveStates.IDLE;
				}

				// Enemies layer hit
				else if(mask == e_laymask) {
					GridMovementSubscriber t_gms = 
						hit.collider.gameObject.GetComponent<GridMovementSubscriber>();

					if(t_gms != null) {
						if(t_gms.GetState() == MoveStates.MOVING){
							// They're moving? Cool. Let's move. 
							state = MoveStates.MOVING;
						}
						else {
							// Else we're tenative about action. 
							state = MoveStates.TENATIVE;
							// cache the gms so we don't have to waste getting it later. 
							cached_gms = t_gms;
						}
					}
					// Else the enemy doesn't move. Therefore I can't move. 
					else {
						state = MoveStates.IDLE;
					}

				}

				// Level layer hit
				else if(mask == l_laymask) {
					state = MoveStates.IDLE;
				}
			}
			else{
				state = MoveStates.MOVING;
			}
		}
	}

	// Updates the movement compoent of the enemy. 
	public void DeclareMovement()
	{
		bool attacked = false;

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
			ProcessState();
		}
	}

	// Actually moves the enemy. 
	public void Move() 
	{
		if(state == MoveStates.MOVING) {
			transform.position += (Vector3)declaredMovement;
		}

		state = MoveStates.IDLE;
		cached_gms = null;
	}

	public void SetMovementMethod(MovementDeclarator method) { _movMethod = method; }
	public void SetAttackMethod(AttackMethod method) { _attackMethod = method; }
	public MoveStates GetState() { return state; }
}
