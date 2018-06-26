﻿using UnityEngine;

namespace StandardAssets.Characters.Physics
{
	[RequireComponent(typeof(OpenCharacterController))]
	public class OpenCharacterControllerPhysics : BaseCharacterPhysics
	{
		private OpenCharacterController characterController;
		
		protected override void Awake()
		{
			base.Awake();
			characterController = GetComponent<OpenCharacterController>();
		}

		protected override float GetPredicitedFallDistance()
		{
			return characterController.GetPredicitedFallDistance();
		}

		protected override bool CheckGrounded()
		{
			return characterController.isGrounded;
		}

		protected override void MoveCharacter(Vector3 movement)
		{
			characterController.Move(movement);
		}
	}
}