using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AcidShot : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] GameObject _parent;
    [SerializeField] AudioSource _audio;
    private SpriteRenderer _model;
    private Vector3 _direction;
    private WaitForSeconds _lifetime;
    private WaitForSeconds _step;
    private Collider2D _collider;
    private Animator _animator;
    private Light2D _light;

    public bool Fired { get; set; }

    private void Awake()
    {
        _step = new WaitForSeconds(0.01f);
        _lifetime = new WaitForSeconds(4f);
        _model = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _light = GetComponentInChildren<Light2D>();

        _light.intensity = 0f;
        _model.enabled = false;
        _collider.enabled = false;
        _animator.enabled = false;
    }

    public void InitShot()
    {
        SetDirection();
        transform.SetParent(null);
        StartCoroutine(AcidFade(_lifetime));

        _light.intensity = 0.75f;
        _collider.enabled = true;
        _model.enabled = true;
        _animator.enabled = false;
    }

    void Update()
    {
        if (Fired)
            Move();
    }

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _audio.Play();
        StartCoroutine(AcidFade(null));
    }

    private IEnumerator AcidFade(WaitForSeconds wait)
    {
        yield return wait;
        if (transform.parent == null)
        {
            _collider.enabled = false;
            Fired = false;
            Color color = _model.color;
            float intensity = _light.intensity;
            for (float a = 1; a >= 0; a -= 0.02f)
            {
                Color newColor = new Color(color.r, color.g, color.b, a);
                _model.color = newColor;
                _light.intensity -= intensity * 0.02f;
                yield return _step;
            }

            MoveToPool();
        }
    }

    private void MoveToPool()
    {
        transform.SetParent(_parent.transform);
        transform.localPosition = Vector3.zero;
        _collider.enabled = false;
        Fired = false;
        _light.intensity = 0f;
        _model.color = Color.white;
        _model.enabled = false;
    }

    private void SetDirection()
    {

        bool isFlipped = false;
        foreach (var w in _parent.GetComponents<Walker>())
            isFlipped |= w.IsFlipped;

        _direction = isFlipped ? Vector3.left : Vector3.right;
    }

}
