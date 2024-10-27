using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending_Controller : MonoBehaviour
{
    [SerializeField] string _victoryMessage = "You escaped!"; 

    private GameObject _player;
    private Level_Controller _levelController;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _levelController = GameObject.FindGameObjectWithTag("Level").GetComponent<Level_Controller>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _player)
        {
            _levelController.endGame(_victoryMessage, true);
            Destroy(other.gameObject);
        }
    }
}
