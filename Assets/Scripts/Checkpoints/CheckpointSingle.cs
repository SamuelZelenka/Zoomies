using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    [SerializeField] private GameObject _undoneChore;
    [SerializeField] private GameObject _doneChore;
    [SerializeField] private GameObject _ringOfDiamonds;
    [SerializeField] private AudioClip _audioClip;
    
    private LevelCheckpoints _levelCheckpoints;
    private AudioSource _audioSource;

    private void Awake()
    {
        _levelCheckpoints = GetComponentInParent<LevelCheckpoints>();
        _audioSource = GetComponentInParent<AudioSource>();
        _undoneChore.SetActive(true);
        _ringOfDiamonds.SetActive(true);
        _doneChore.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            _undoneChore.SetActive(false);
            _ringOfDiamonds.SetActive(false);
            _doneChore.SetActive(true);
            _audioSource.PlayOneShot(_audioClip);
            _levelCheckpoints.PlayerThroughCheckpoint(this);
        }
    }

    public void SetTrackCheckpoints(LevelCheckpoints levelCheckpoints) =>  _levelCheckpoints = levelCheckpoints;
}
