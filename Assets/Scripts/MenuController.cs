using System.Collections;
using UnityEngine;
using SimpleJSON;
using System.Net;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _panelBlock;
    [SerializeField] private GameObject _panelBlockText;
    [SerializeField] protected Text _text;

    [SerializeField] private Text _textAll;
    [SerializeField] private Text _textMessage;
    [SerializeField] private Text _textError;

    [SerializeField] private SortController _s;

    private JSONNode _json;
    private GameObject _activePanel;

    private const float _period = 0.5f;
    private const string _url = "http://tc-monitor.tiram.icu:5000/?key=";
    private const string _key = "Voo0oHaj";

    void Awake()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            _panelBlock.SetActive(true);
            _panelBlockText.SetActive(true);

            return;
        }

        var webClient = new WebClient();
        var response = webClient.DownloadString(_url + _key);
        _json = JSONNode.Parse(response);

        _textAll.text = _json.ToString();
        _textMessage.text = _json["message"];
        _textError.text = _json["error"];

        SortController.Notify?.Invoke();
    }

    public void ButtonOpen(GameObject window)
    {
        if (_panelBlock.activeSelf)
        {
            return;
        }

        _panelBlock.SetActive(true);
        _activePanel = window;

        StartCoroutine(AnimPanel(_activePanel.transform, Vector3.one));
    }

    public void ButtonClose()
    {
        _panelBlock.SetActive(false);
        StartCoroutine(AnimPanel(_activePanel.transform, Vector3.zero));

        _activePanel = null;
    }

    IEnumerator AnimPanel(Transform window, Vector3 doneVector)
    {
        var time = 0f;
        var scale = window.localScale;

        while (time < _period)
        {
            time += Time.deltaTime;
            var nTime = time / _period;
            var lValue = Vector3.Lerp(scale, doneVector, nTime);
            window.localScale = lValue;

            yield return null;
        }
    }
}
