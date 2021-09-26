using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreListPopulator : MonoBehaviour
{

    private const int TOTAL_ENTRY_LENGTH = 26;

    [SerializeField] private Transform _contentParent;
    [SerializeField] private GameObject _highScoreEntryPrefab;

    private GameState _gameState;

    void Awake()
    {
        _gameState = GoUtil.FindGameState();
    }

    public void RepopulateEntries()
    {
        ClearExistingEntries();
        PopulateEntries();
    }

    private void ClearExistingEntries()
    {
        for (int childIndex = 0; childIndex < _contentParent.transform.childCount; childIndex++)
        {
            GameObject.Destroy(_contentParent.GetChild(childIndex).gameObject);
        }
    }

    private void PopulateEntries()
    {
        List<HighScoreInfo> highScores = _gameState.GetHighScores();
        highScores.Sort((h1, h2) => -h1.Score.CompareTo(h2.Score));
        int place = 1;
        highScores.ForEach(highScore => AddEntry(highScore, place++));
    }

    private void AddEntry(HighScoreInfo highScore, int place)
    {
        GameObject populatedEntry = CreatePopulatedEntry(highScore, place);
        populatedEntry.transform.SetParent(_contentParent);
        populatedEntry.transform.localScale = Vector3.one;
    }

    private GameObject CreatePopulatedEntry(HighScoreInfo highScore, int place)
    {
        GameObject entryObject = GameObject.Instantiate<GameObject>(_highScoreEntryPrefab);
        string placeAndName = string.Format("{0}. {1} ", place, highScore.PlayerName);
        string score = string.Format(" {0:N0}", highScore.Score);
        int numberOfDotsToPadWith = TOTAL_ENTRY_LENGTH - placeAndName.Length - score.Length;
        entryObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0}{1}{2}", placeAndName, "".PadLeft(numberOfDotsToPadWith, '.'), score);
        return entryObject;
    }
}
