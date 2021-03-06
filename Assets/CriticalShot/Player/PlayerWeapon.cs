﻿using AutoGeneratedCode.Enums;
using CriticalShot.Managers;
using CriticalShot.Weapon;
using CriticalShot.Weapon.LaserFireGun;
using UnityEngine;
using UnityEngine.Networking;

namespace CriticalShot.Player
{
	public class PlayerWeapon : NetworkBehaviour
	{
		[SerializeField] private LaserFireWeapon _currentFireWeapon;
		[SerializeField] private Camera _weaponPosition;
		[SerializeField] private LayerMask _mask;

		private bool _tryingToFire;

		void Start()
		{
			if (_currentFireWeapon == null)
			{
				throw new MissingComponentException("PlayerWeapon: A LaserFireWeapon is required");
			}

			if (_weaponPosition == null)
			{
				throw new MissingComponentException("PlayerWeapon: A Camera is required");
			}

			LaserFireWeapon weapon = Instantiate(_currentFireWeapon);
			weapon.transform.parent = _weaponPosition.transform;
			weapon.transform.localPosition = new Vector3(0.4f, 0.25f, 0.6f);
		}

		void FixedUpdate()
		{
			PerformFire();
		}

		public void Fire()
		{
			_tryingToFire = true;
		}

		
		public void HoldFire()
		{
			_tryingToFire = false;
		}
	
		[Client]
		private void PerformFire()
		{
			if (_tryingToFire)
			{
				RaycastHit hitInfo;
				if (Physics.Raycast(_weaponPosition.transform.position, _weaponPosition.transform.forward, out hitInfo, _currentFireWeapon.Range, _mask))
				{
					if (hitInfo.collider.CompareTag(Tags.Player))
					{
						CmdPerformFire(hitInfo.collider.name);
					}
				}
			}
		}

		[Command]
		private void CmdPerformFire(string playerObjectiveId)
		{
			Debug.Log(playerObjectiveId + " has been shot.");
			PlayerManager playerManager = GameManager.GetPlayerManager(playerObjectiveId);

			playerManager.TakeDamage(_currentFireWeapon.Damage);
			//GameObject.Find(playerObjectiveId).SetActive(false);
		}
	}
}
