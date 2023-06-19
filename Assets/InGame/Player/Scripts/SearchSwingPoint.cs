using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SearchSwingPoint : IPlayerAction
{
    [Header("����Ray��`�悷�邩�ǂ���")]
    [SerializeField]
    private bool _isDrowCube = true;

    [Header("���̃T�C�Y")]
    [SerializeField] private Vector3 _boxSize;

    [SerializeField] private List<SwingDirPos> _boxPos = new List<SwingDirPos>();



    [Header("���g�̍����Ƀ��C���[���΂��ʒu")]
    [SerializeField] private List<SwingDirPos> _swingPosLeft = new List<SwingDirPos>();

    [Header("���g�̉E���Ƀ��C���[���΂��ʒu")]
    [SerializeField] private List<SwingDirPos> _swingPosRight = new List<SwingDirPos>();

    [Header("���C���[�̍Œ��̒���")]
    [SerializeField] private float _maxWireLong = 20;

    [Header("���C���[�̍ŒZ�̒���")]
    [SerializeField] private float _minWireLong = 15;

    [SerializeField] private Transform _Cpos;

    [SerializeField] private LayerMask _layer;

    /// <summary>Swing���o���邩�ǂ���</summary>
    private bool _isCanHit;
    /// <summary>Swing�̃��C���[���h���ʒu</summary>
    private Vector3 _swingPosition;
    private Vector3 _realSwingPoint;
    public Vector3 RealSwingPoint { get => _realSwingPoint; set => _realSwingPoint = value; }


    public Vector3 SwingPos => _swingPosition;
    public bool IsCanHit => _isCanHit;

    /// <summary>Swing�̏o����ꏊ��T��</summary>
    public bool Search()
    {
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);

        //�E���͂��������ꍇ�A�E����D�悵�Č���
        if (_playerControl.InputManager.HorizontalInput > 0)
        {
            //�E�����Ɋm�F
            bool firstCheck = WallSearch(1, _playerControl.PlayerT, horizontalRotation);

            //�E���ɓ�����ꏊ���Ȃ������ꍇ
            if (firstCheck == false)
            {
                //2��ځA�������m�F����
                bool secondCheck = WallSearch(-1, _playerControl.PlayerT, horizontalRotation);

                if (secondCheck)
                {
                    return true;
                }�@//�������������Ă�����Ԃ�
                else
                {
                    //�ŏI�m�F�BBox�ł̊m�F�B�E������
                    bool lastCheckRight = BoxCheck(1, horizontalRotation);
                    if (lastCheckRight)
                    {
                        return true;
                    }   
                    else
                    {
                        return BoxCheck(-1, horizontalRotation);
                    }
                }
            }
            else
            {
                return firstCheck;
            }   //�P��ڂŉE���ɓ������Ă�����E����Ԃ�
        }   
        //�����̓��͂��������ꍇ�B�m�F���@�͓���
        else if (_playerControl.InputManager.HorizontalInput < 0)
        {
            bool firstCheck = WallSearch(-1, _playerControl.PlayerT, horizontalRotation);

            if (firstCheck == false)
            {
                bool secondCheck = WallSearch(1, _playerControl.PlayerT, horizontalRotation);

                if (secondCheck)
                {
                    return true;
                }
                else
                {
                    bool lastCheckLeft = BoxCheck(-1, horizontalRotation);
                    if (lastCheckLeft)
                    {
                        return true;
                    }
                    else
                    {
                        return BoxCheck(1, horizontalRotation);
                    }
                }
            }
            else
            {
                return firstCheck;
            }
        }
        else
        {
            bool firstCheck = WallSearch(1, _playerControl.PlayerT, horizontalRotation);

            if (firstCheck == false)
            {
                bool secondCheck = WallSearch(-1, _playerControl.PlayerT, horizontalRotation);

                if (secondCheck)
                {
                    return true;
                }
                else
                {
                    bool lastCheckRight = BoxCheck(1, horizontalRotation);
                    if (lastCheckRight)
                    {
                        return true;
                    }
                    else
                    {
                        return BoxCheck(-1, horizontalRotation);
                    }
                }
            }
            else
            {
                return firstCheck;
            }
        }
    }


    public bool WallSearch(float inputH, Transform player, Quaternion hRotation)
    {
        //�T���ꏊ
        List<SwingDirPos> searchPoints = new List<SwingDirPos>();

        if (inputH == 1)
        {
            searchPoints = _swingPosRight;
        }   //�E���́A�E����T���Ƃ�
        else if (inputH == -1)
        {
            searchPoints = _swingPosLeft;
        }   //�����́A������T���Ƃ�

        //�������ɕ��ѕς���
        searchPoints.Sort();

        //Swing�|�C���g�̒T��
        foreach (var searchPos in searchPoints)
        {
            RaycastHit hit;

            //�J�����̉�]�����l�����āA��̕�����ϊ�
            Vector3 pos = hRotation * new Vector3(searchPos.Dir.x, searchPos.Dir.y, searchPos.Dir.z);

            Vector3 dir = default;

            if (inputH > 0)
            {
                dir = hRotation * Vector3.right;
            }
            else
            {
                dir = hRotation * Vector3.left;
            }


            //Ray���΂�
            bool isHit = Physics.Raycast(_playerControl.PlayerT.position + new Vector3(pos.x, pos.y, pos.z), dir, out hit, _maxWireLong, _layer);

            Debug.DrawRay(_playerControl.PlayerT.position, pos * 30, Color.green);

            //Ray��Hit���Ă�����
            if (isHit)
            {
                float distance = Vector3.Distance(hit.point, _playerControl.PlayerT.position);

                //Hit�n�_�܂ł̋������A���C���[�̍ŒZ������蒷��������L��
                if (distance >= _minWireLong)
                {
                    _isCanHit = true;
                    _swingPosition = hit.point;



                    Vector3 d = new Vector3(_playerControl.PlayerT.position.x, _swingPosition.y, _playerControl.PlayerT.position.z);

                    Vector3 f = Camera.main.transform.forward;
                    f.y = 0;

                    Vector3 dirPlayer = d + f * 20;

                    _realSwingPoint = dirPlayer;

                    if (_swingPosition == Vector3.zero)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        _isCanHit = false;
        return false;
    }

    public bool BoxCheck(float h, Quaternion hRotation)
    {

        foreach (var boxPos in _boxPos)
        {
            ////Box�ł̎Q��
            Vector3 addCenter = hRotation * new Vector3(boxPos.Dir.x, boxPos.Dir.y, boxPos.Dir.z);
            Vector3 boxCenter = _playerControl.PlayerT.position + addCenter;

            RaycastHit boxHit;

            Vector3 dir = default;

            if (h == 1)
            {
                dir = hRotation * Vector3.right;
            }
            else
            {
                dir = hRotation * Vector3.left;
            }


            //Cast���΂�
            bool isHitBox = Physics.BoxCast(boxCenter, _boxSize, dir, out boxHit, Quaternion.identity, _maxWireLong, _layer);

            if (isHitBox)
            {
                float distance = Vector3.Distance(boxHit.point, _playerControl.PlayerT.position);

                //Hit�n�_�܂ł̋������A���C���[�̍ŒZ������蒷��������L��
                if (distance >= _minWireLong)
                {
                    _isCanHit = true;
                    _swingPosition = boxHit.point;

                    Vector3 d = new Vector3(_playerControl.PlayerT.position.x, _swingPosition.y, _playerControl.PlayerT.position.z);

                    Vector3 cameraFoward = Camera.main.transform.forward;
                    cameraFoward.y = 0;

                    Vector3 dirPlayer = d + cameraFoward * 20;

                    _realSwingPoint = dirPlayer;

                    if (_swingPosition == Vector3.zero)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }



    public void OnDrawGizmos(Transform player)
    {
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);

        foreach (var a in _boxPos)
        {
            Gizmos.color = Color.blue;

            Quaternion cameraR = default;

            var q = Camera.main.transform.rotation.eulerAngles;
            q.x = 0f;
            cameraR = Quaternion.Euler(q);


            Gizmos.matrix = Matrix4x4.TRS(player.position, cameraR, player.localScale);

            Gizmos.DrawCube(a.Dir, _boxSize);

            Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
        }

        foreach (var a in _swingPosLeft)
        {
            Gizmos.color = Color.red;

            Vector3 newA = horizontalRotation * new Vector3(a.Dir.x, a.Dir.y, a.Dir.z);
            Vector3 dir = horizontalRotation * Vector3.right;


            Gizmos.DrawRay(player.position + new Vector3(newA.x, newA.y, newA.z), dir);
        }

        foreach (var a in _swingPosRight)
        {

            Vector3 newA = horizontalRotation * new Vector3(a.Dir.x, a.Dir.y, a.Dir.z);
            Vector3 dir = horizontalRotation * Vector3.left;

            Gizmos.DrawRay(player.position + new Vector3(newA.x, newA.y, newA.z), dir);
        }
    }




}

[System.Serializable]
public class SwingDirPos : IComparable<SwingDirPos>
{
    public Vector3 Dir;


    public int CompareTo(SwingDirPos other)
    {
        if (this.Dir.y < other.Dir.y)
        {
            return -1;  // ������ ID �����������́u�����̕����O�v�Ƃ���
        }
        else if (this.Dir.y > other.Dir.y)
        {
            return 1;  // ������ ID ���傫�����́u�����̕������v�Ƃ���
        }
        else if (this.Dir.y == other.Dir.y) // ID �������Ȃ�u�����v�Ƃ���
        {
            if (this.Dir.z < other.Dir.z)
            {
                return -1;  // ������ ID �����������́u�����̕����O�v�Ƃ���
            }
            else if (this.Dir.z > other.Dir.z)
            {
                return 1;  // ������ ID ���傫�����́u�����̕������v�Ƃ���
            }
            else if (this.Dir.z == other.Dir.z)
            {
                if (this.Dir.x < other.Dir.x)
                {
                    return -1;  // ������ ID �����������́u�����̕����O�v�Ƃ���
                }
                else if (this.Dir.x > other.Dir.x)
                {
                    return 1;  // ������ ID ���傫�����́u�����̕������v�Ƃ���
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
        else
        {
            return 0;
        }
    }
}
