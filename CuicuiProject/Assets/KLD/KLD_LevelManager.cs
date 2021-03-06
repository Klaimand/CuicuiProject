using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KLD_LevelManager : MonoBehaviour
{
    [SerializeField] KLD_CandyManager candyManager;
    [SerializeField] KLD_ScoreManager scoreManager;

    [SerializeField] GameObject spike = null;

    int spikeNumber = 4;

    [SerializeField] Transform[] leftSpots;
    [SerializeField] Transform[] rightSpots;

    [SerializeField] Transform spikesParent;
    Transform leftSpikeParent;
    Transform rightSpikeParent;

    [SerializeField] float candyOffset = 1f;

    [SerializeField, Header("Spikes slide")] AnimationCurve slideCurve;
    [SerializeField] float slideTime = 0.2f;
    [SerializeField] float slideOffset = 0.5f;

    void Start()
    {
        leftSpikeParent = new GameObject("leftSpikeParent").transform;
        rightSpikeParent = new GameObject("rightSpikeParent").transform;

        leftSpikeParent.parent = spikesParent;
        rightSpikeParent.parent = spikesParent;

        OffsetSpots();

    }

    void Update()
    {

    }

    void PlaceSpots(bool _right, int _nb)
    {
        _nb = _nb > 12 ? 12 : _nb;

        List<int> spotIndexesLeft = new List<int>();

        for (int i = 0; i < 12; i++) //populate indexes
        {
            spotIndexesLeft.Add(i);
        }

        for (int i = 0; i < _nb + 1; i++)
        {
            if (!(i == _nb))
            {
                int indexToRemove = spotIndexesLeft[Random.Range(0, spotIndexesLeft.Count)];

                Transform[] spots = _right ? rightSpots : leftSpots;

                GameObject go = Instantiate(spike, spots[indexToRemove].position, Quaternion.identity, _right ? rightSpikeParent : leftSpikeParent);
                go.transform.GetChild(0).GetComponent<SpriteRenderer>().color = scoreManager.GetLevelColor();

                spotIndexesLeft.Remove(indexToRemove);
            }

            else //candy placement
            {
                int indexToRemovee = spotIndexesLeft[Random.Range(0, spotIndexesLeft.Count)];

                Transform[] spotss = _right ? rightSpots : leftSpots;

                candyManager.PlaceCandy(
                    spotss[indexToRemovee].position + (_right ? Vector3.left * candyOffset : Vector3.right * candyOffset)
                    );
            }
        }

    }

    IEnumerator RemoveSpots(bool _right, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        Transform parent = _right ? rightSpikeParent : leftSpikeParent;

        int k = parent.childCount;
        for (int i = 0; i < k; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    public void BirdTouch(bool _right)
    {
        PlaceSpots(!_right, spikeNumber);
        StartCoroutine(OffsetSpikes(!_right));
        StartCoroutine(RemoveSpots(_right, slideTime + 0.1f));
    }

    public void RemoveSpikesAtStart()
    {
        StartCoroutine(OffsetSpikes(false));
        StartCoroutine(RemoveSpots(false, slideTime + 0.1f));

        StartCoroutine(OffsetSpikes(true));
        StartCoroutine(RemoveSpots(true, slideTime + 0.1f));
    }

    IEnumerator OffsetSpikes(bool _touchedRight)
    {
        float t = 0f;

        float xPos = 0f;

        while (t < slideTime)
        {
            xPos = slideCurve.Evaluate(t / slideTime) * slideOffset;

            xPos = _touchedRight ? slideOffset - xPos : xPos;

            spikesParent.localPosition = Vector3.right * xPos;

            t += Time.deltaTime;
            yield return null;
        }

        //spikesParent.localPosition = Vector3.right * (_touchedRight ? slideOffset : 0f);
    }

    void OffsetSpots()
    {
        foreach (Transform child in leftSpots)
        {
            child.localPosition -= Vector3.right * slideOffset;
        }

        foreach (Transform child in rightSpots)
        {
            child.localPosition += Vector3.right * slideOffset;
        }
    }

    public void SetSpikeNumber(int _spikes)
    {
        spikeNumber = _spikes;
    }


}
