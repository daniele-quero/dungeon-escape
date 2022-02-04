using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFire : MonoBehaviour
{
    [SerializeField] private Spider _spider;
    [SerializeField] private float _shotLife;
    private WaitForSeconds _shotLifeWait;

    private void Start()
    {
        _shotLifeWait = new WaitForSeconds(_shotLife);
    }

    public void ShootAcid()
    {
        Transform t = _spider.transform.childCount > 4 ? _spider.transform.GetChild(4) : null;
        if (t != null)
        {
            _spider.shotDone = false;
            StartCoroutine(ShotDoneBackup());
            var s = t.GetComponent<AcidShot>();
            s.InitShot();
            s.Fired = true;
            _spider.Audio.PlayAttackAudio();
        }
    }

    public void ShotDone()
    {
        _spider.shotDone = true;
    }

    private IEnumerator ShotDoneBackup()
    {
        yield return _shotLifeWait;
        _spider.shotDone = true;
    }

}
