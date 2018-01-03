using UnityEngine;

namespace CriticalShot.Player
{
    [RequireComponent(typeof(ConfigurableJoint))]
    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _panRotationSpeed = 3f;
        [SerializeField] private float _tiltRotationSpeed = 3f;
        [SerializeField] private float _thrusterForce = 1000f;
        [SerializeField] private bool _yAxisInverted;

        [Header("Spring settings:")]
        [SerializeField] private float _jointPositionSpring = 15f;
        [SerializeField] private float _jointMaxForce = 40f;

        [Header("Weapon settings")] 
        [SerializeField] private PlayerWeapon _playerWeapon;

        private PlayerMotor _motor;
        private ConfigurableJoint _joint;

        private void Start()
        {
            _motor = GetComponent<PlayerMotor>();
            _playerWeapon = GetComponent<PlayerWeapon>();
            _joint = GetComponent<ConfigurableJoint>();
            SetJointSettings(_jointPositionSpring);
        }

        private void Update()
        {
            Movement();
            PanRotation();
            TiltRotation();
            Thruster();
            Firing();
        }

        private void Firing()
        {
            if (Input.GetButton("Fire1"))
            {
                _playerWeapon.Fire();
            }
            else
            {
                _playerWeapon.HoldFire();
            }
        }

        private void Thruster()
        {
            Vector3 actualThusterForce = Vector3.zero;
            if (Input.GetButton("Jump"))
            {
                actualThusterForce = Vector3.up * _thrusterForce;
                SetJointSettings(0f);
            }
            else
            {
                SetJointSettings(_jointPositionSpring);
            }

            _motor.ThrusterForce(actualThusterForce);
        }

        private void PanRotation()
        {
            float yRot = Input.GetAxisRaw("Mouse X");
            Vector3 rotation = new Vector3(0f, yRot, 0f) * _panRotationSpeed;

            _motor.PanRotate(rotation);
        }

        private void TiltRotation()
        {
            float xRot = Input.GetAxisRaw("Mouse Y");
            if (!_yAxisInverted)
            {
                xRot *= -1;
            }

            _motor.TiltRotate(xRot * _tiltRotationSpeed);
        }

        private void Movement()
        {
            float xMov = Input.GetAxisRaw("Horizontal");
            float zMov = Input.GetAxisRaw("Vertical");

            Vector3 movHorizontal = transform.right * xMov;
            Vector3 movVertical = transform.forward * zMov;

            Vector3 velocity = (movHorizontal + movVertical).normalized * _speed;

            _motor.Move(velocity);
        }

        private void SetJointSettings(float jointPositionSpring)
        {
            _joint.yDrive = new JointDrive
            {
                maximumForce = _jointMaxForce,
                positionSpring = jointPositionSpring
            };
        }
    }
}