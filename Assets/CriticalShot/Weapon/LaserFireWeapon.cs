using UnityEngine;

namespace CriticalShot.Weapon
{
	[System.Serializable]
	public class LaserFireWeapon : MonoBehaviour
	{
		[SerializeField] private string _type = "Laser";
		[SerializeField] private float _damage = 10f;
		[SerializeField] private float _range = 100f;

		public string Type
		{
			get { return _type; }
		}

		public float Damage
		{
			get { return _damage; }
		}

		public float Range
		{
			get { return _range; }
		}
	}
}