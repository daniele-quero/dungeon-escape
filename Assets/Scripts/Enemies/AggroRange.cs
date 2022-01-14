using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AggroRange : MonoBehaviour
{
    public Transform Player { get => _player; set => _player = value; }
    private BoxCollider2D _range;
    [SerializeField] private Transform _player;

    public BoxCollider2D Range { get => _range; }

    private void Start()
    {
        _range = GetComponent<BoxCollider2D>();
        Player = null;
    }

    #region Trigger Management
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ("Player" == collision.tag)
            Player = collision.GetComponent<Transform>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ("Player" == collision.tag)
            Player = null;

        GetComponentInParent<Enemy>().Animator.SetTrigger("onResume");
    }
    #endregion
}
