﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIHunter/Decisions/_Look")]
public class _LookDecision : AIDecision {

	public override bool Decide(AIStateController controller){
		bool targetVisiable = Look(controller);
		return targetVisiable;
	}

	private bool Look(AIStateController controller){
//		RaycastHit hit;
//
//		Debug.DrawRay (controller.eye.position, controller.eye.forward.normalized * controller.enemyStats.lookRange, Color.green);
//
//		if (Physics.SphereCast (controller.eye.position, controller.enemyStats.lookSphereCastRadius, controller.eye.forward, out hit, controller.enemyStats.lookRange)
//		    && hit.collider.CompareTag ("Player")) {
//			controller.chaseTarget = hit.transform;
//			return true;
//		} else {
//			return false;
//		}
		controller.chaseTarget.Clear ();

		Collider[] targetsInViewRadius = Physics.OverlapSphere (controller.transform.position, controller.enemyStats.lookRange, controller.targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			if (targetsInViewRadius [i].tag.Equals ("Player")) {
				NetworkCharacter target = targetsInViewRadius [i].GetComponent<NetworkCharacter>(); 
				if (target.Team == GameEnum.TeamType.Survivor && target.CurrentState != CharacterState.Dead)
					controller.chaseTarget.Add (target);
				}
			} 
			
		if (controller.chaseTarget.Count > 0)
			return true;
		else
			return false;
	}
}
