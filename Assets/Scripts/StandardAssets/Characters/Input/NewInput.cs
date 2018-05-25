﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using Cinemachine;
using ProBuilder2.Common;
using StandardAssets.Characters.Effects;
using StandardAssets.Characters.Input;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Interactions;
using UnityEngine.Experimental.Input.Controls;
using Random = UnityEngine.Random;


namespace StandardAssets.Characters.Input
{
	public class NewInput : MonoBehaviour, IInput
	{
		Vector2 m_MoveInput;

		private Vector2 moveVector2;

		public Camera mainCamera;

		public GameObject projectile;


		public DemoInputActions controls;
		
		private static string compositeInputFile = "compositeInput.json";

		private string compositeInputJsonString;

		private Vector2 m_look;


		public void OnEnable()
		{

			

			controls.Enable();
			

		}

		public void OnDisable()
		{
			
			controls.Disable();
		
		}
		
		void Awake()
		{

			CinemachineCore.GetInputAxis = LookInputOverride;

			controls.gameplay.look.performed += ctx => m_look = ctx.ReadValue<Vector2>();
		}

		void Update()
		{
			Debug.Log("MousE: "+m_look.ToString());
			//controls.gameplay.look.performed += ctx => m_look = ctx.ReadValue<Vector2>();
			//Debug.Log(CinemachineCore.GetInputAxis("Mouse x"));
		}

		float LookInputOverride(string axis)
		{
			//TODO: DAVE overload this
			if (axis == "Mouse X")
			{
				return m_look.x;
			}

			if (axis == "Mouse Y")
			{
				return m_look.y;
			}

			return 0;
		}

		void Move(KeyControl control)
		{
			string keyName = control.keyCode.ToString();
			int number = Convert.ToInt32(keyName.Substring(keyName.Length - 1));
			Vector2 moveAxis = new Vector2(0, 0);
			switch (number)
			{
				case 8:
					moveAxis.x = 1;
					break;
				case 2:
					moveAxis.x = -1;
					break;
				case 4:
					moveAxis.y = -1;
					break;
				case 6:
					moveAxis.y = 1;
					break;
			}

			m_MoveInput.Set(moveAxis.x, moveAxis.y);
			Debug.Log(moveAxis.ToString());
		}


		void Fire(InputAction.CallbackContext context)
		{
			Debug.Log("NEW INPUT FIRE!");
			var transform = this.transform;
			var newProjectile = Instantiate(projectile);
			newProjectile.transform.position = transform.position + transform.forward * 0.6f;
			newProjectile.transform.rotation = transform.rotation;
			var size = 1;
			newProjectile.transform.localScale *= size;
			newProjectile.GetComponent<Rigidbody>().mass = Mathf.Pow(size, 3);
			newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
			newProjectile.GetComponent<MeshRenderer>().material.color =
				new Color(Random.value, Random.value, Random.value, 1.0f);
		}

		public Vector2 moveInput
		{
			get { return m_MoveInput; }
		}

		public bool isMoveInput
		{
			get { return moveInput.sqrMagnitude > 0; }
		}

		public Action jump { get; set; }
	}
	
}