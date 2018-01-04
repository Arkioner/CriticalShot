using UnityEngine;

namespace CriticalShot.Weapon.LaserFireGun
{
	[System.Serializable]
	public class LaserFireWeapon : MonoBehaviour
	{
		[SerializeField] private string _type = "Laser";
		[SerializeField] private int _damage = 10;
		[SerializeField] private float _range = 100f;

		public string Type
		{
			get { return _type; }
		}

		public int Damage
		{
			get { return _damage; }
		}

		public float Range
		{
			get { return _range; }
		}
	}
}