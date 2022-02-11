using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements.Experimental;


public class ReplayController : MonoBehaviour
{
    public List<ReplayDataPoint> _replayData;
    public GameObject _targetObject;

    private float sampleDelay = 0.2f;
    private int _dataIndex = 0;
    private Transform _targetTransform;
    private Rigidbody _targetRigidbody;
    private RaceTimer _timer;
    private ReplayDataPoint _temp;
    private Vector3 _tempVector3;
    private Quaternion _tempQuaternion;

    public void SetReplayData(List<ReplayDataPoint> data)
    {
        _replayData = data;
    }
    
    public void SetTargetObject(GameObject target)
    {
        _targetObject = target;
        _targetTransform = _targetObject.transform;
        _targetRigidbody = _targetObject.GetComponent<Rigidbody>();
    }
        
    
    public void Record()
    {
        _replayData = new List<ReplayDataPoint>();
        StartCoroutine(Recorder());
    }
    
    public void StopRecording()
    {
        StopCoroutine(Recorder());
    }
    
    private IEnumerator Recorder()
    {
        while (true)
        {
            yield return new WaitForSeconds(sampleDelay);
            SaveDataPoint();
        }
    }
    
    
    public void Play()
    {
        StartCoroutine(Player());
    }
    
    public void StopPlaying()
    {
        StopCoroutine(Player());
    }
    
    private IEnumerator Player()
    {
        while (true)
        {
            yield return new WaitForSeconds(sampleDelay);
            _targetTransform.position = GetPosition(_dataIndex);
            _targetTransform.rotation = GetRotation(_dataIndex);
            _targetRigidbody.velocity = GetVelocity(_dataIndex);
            _dataIndex++;
        }
    }
    
    public Vector3 GetPosition(int i)
    {
        _temp = _replayData[i];
        _tempVector3.x = _temp.px;
        _tempVector3.y = _temp.py;
        _tempVector3.z = _temp.pz;
        return _tempVector3;
    }

    public Quaternion GetRotation(int i)
    {
        _temp = _replayData[i];
        _tempQuaternion.x = _temp.rx;
        _tempQuaternion.y = _temp.ry;
        _tempQuaternion.z = _temp.rz;
        _tempQuaternion.w = _temp.rw;
        return _tempQuaternion;
    }
    
    public Vector3 GetVelocity(int i)
    {
        _temp = _replayData[i];
        _tempVector3.x = _temp.vx;
        _tempVector3.y = _temp.vy;
        _tempVector3.z = _temp.vz;
        return _tempVector3;
    }

    private void SaveDataPoint()
    {
        _replayData.Add(new ReplayDataPoint(
            _targetTransform.position.x,
            _targetTransform.position.y,
            _targetTransform.position.z,
            _targetTransform.rotation.x,
            _targetTransform.rotation.y,
            _targetTransform.rotation.z,
            _targetTransform.rotation.w,
            _targetRigidbody.velocity.x,
            _targetRigidbody.velocity.y,
            _targetRigidbody.velocity.z));
    }

    public struct ReplayDataPoint {
        public float px;
        public float py;
        public float pz;
        public float rx;
        public float ry;
        public float rz;
        public float rw;
        public float vx;
        public float vy;
        public float vz;
        
        public ReplayDataPoint(float px, float py, float pz, float rx, float ry, float rz, float rw, float vx, float vy, float vz)
        {
            this.px = px;
            this.py = py;
            this.pz = pz;
            this.rx = rx;
            this.ry = ry;
            this.rz = rz;
            this.rw = rw;
            this.vx = vx;
            this.vy = vy;
            this.vz = vz;
        }
    }
    
    

}


