using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortController : MonoBehaviour
{
    [Header("Sort")]
    [SerializeField] protected Text _text;
    [SerializeField] protected Image _textImage;

    [Header("Find by name")]
    [SerializeField] private InputField _inputText;

    private string _content;
    private string[] _saveContentSort;
    private string[] _contentSort;
    private string[] _contentArray;
    private bool _isSort;

    public static Action Notify;

    private void Awake()
    {
        Notify += SaveContent;
    }

    private void OnDestroy()
    {
        if (Notify != null) Notify -= SaveContent;
    }

    private void SaveContent()
    {
        var currentContent = _text.text;
        _content = currentContent;
        _contentArray = currentContent.Split(' ');
    }

    private void PripareToSort()
    {
        var currentContent = _text.text;
        _contentSort = currentContent.Split(' ');
        _saveContentSort = currentContent.Split(' ');
    }

    public void ButtonSortClick()
    {
        if (_isSort)
        {
            SetUnsort();
        }
        else
        {
           SetSort();
        }
    }

    private void SetUnsort()
    {
        _text.text = string.Join(" ", _saveContentSort);
        _textImage.color = Color.white;

        _isSort = false;
    }

    private void SetSort()
    {
        PripareToSort();

        Array.Sort(_contentSort);
        _text.text = string.Join(" ", _contentSort);
        _textImage.color = new Color(0.78f, 0.78f, 0.78f);

        _isSort = true;
    }

    public void ButtonFindByWord()
    {
        if (_content == null) SaveContent();

        if (_isSort)
        {
            SetUnsort();
        }

        var findWord = _inputText.text;

        if (findWord == "")
        {
            _text.text = _content;

            return;
        }

        var outputArray = new List<string>();

        for (int i = 0; i < _contentArray.Length; i++)
        {
            if (_contentArray[i].Contains(findWord))
            {
                outputArray.Add(_contentArray[i]);
            }
        }

        _text.text = string.Join(" ", outputArray);

        PripareToSort();
    }

    public void ButtonReset()
    {
        if (_isSort)
        {
            SetUnsort();
        }

        _inputText.text = null;

        if (_content == null) SaveContent();

        _text.text = _content;
    }
}
