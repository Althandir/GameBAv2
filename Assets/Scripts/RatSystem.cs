using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RectTransform), typeof(BoxCollider2D))]
public class RatSystem : MonoBehaviour
{
    [SerializeField] RectTransform _gameUI;
    [SerializeField]
    [Range(0.05f, .15f)] 
    float _movementDelay;
    [SerializeField]
    [Range(0.01f, 1f)]
    float _movementSpeed;

    bool _isActive;
    bool _isVisible;
    RectTransform _pos;
    BoxCollider2D _boxCol2D;

    IEnumerator _visiblityChecker;
    IEnumerator _movingRoutine;
    IEnumerator _AudioRoutine;

    private void Awake()
    {
        _pos = GetComponent<RectTransform>();
        _boxCol2D = GetComponent<BoxCollider2D>();
        _movingRoutine = Move();
        _visiblityChecker = CheckVisibility();
        _AudioRoutine = PlayAudio();
    }

    /// <summary>
    /// Spawns the Rat if not already active
    /// </summary>
    [ContextMenu("Spawn Rat")]
    public void SpawnRat()
    {
        if (!_isActive)
        {
            _isActive = true;
            _movementSpeed = 0.01f;
            SetStartPos();
            StartCoroutine(_visiblityChecker);
            StartCoroutine(_movingRoutine);
            StartCoroutine(_AudioRoutine);
        }
    }

    /// <summary>
    /// Checks if the rat is visible every 0.1 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckVisibility()
    {
        float pufferPixels = _boxCol2D.size.x;
        
        float xMin = (_gameUI.rect.width / 2) * (-1) - pufferPixels;
        float xMax = (_gameUI.rect.width / 2) + pufferPixels;
        float yMin = (_gameUI.rect.height / 2) * (-1) - pufferPixels;
        float yMax = (_gameUI.rect.height / 2) + pufferPixels;

        while (true)
        {
            if (_pos.anchoredPosition.x < xMin || 
                _pos.anchoredPosition.x > xMax ||
                _pos.anchoredPosition.y < yMin || 
                _pos.anchoredPosition.y > yMax)
            {
                _isVisible = false;
                _isActive = false;
                break;
            }
            else
            {
                _isVisible = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// Moves the rat on the screen while it is visible
    /// </summary>
    /// <returns></returns>
    IEnumerator Move()
    {
        Vector3 MovementVector = new Vector3(1, UnityEngine.Random.Range(0, 0.75f), 0);

        if (_pos.localScale.x < 0)
        {
            MovementVector = new Vector3(MovementVector.x * (-1), MovementVector.y);
        }

        while (_isVisible) 
        {
            transform.Translate(MovementVector*_movementSpeed);
            yield return new WaitForSeconds(_movementDelay);
        }
    }

    /// <summary>
    /// Plays an audio every 5 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayAudio()
    {
        while (_isVisible)
        {
            yield return new WaitForSeconds(5);
        }
    }

    /// <summary>
    /// Setups the startposition of the rat
    /// </summary>
    void SetStartPos()
    {
        _pos.anchoredPosition = new Vector2(RandomX(), RandomY());
    }

    /// <summary>
    /// Returns a random Y-Height depending of the GameUI.Height
    /// </summary>
    /// <returns></returns>
    float RandomY()
    {
        return UnityEngine.Random.Range(0, _gameUI.rect.height / 2 * -1);
    }

    /// <summary>
    /// Returns a random X-Border depending of the GameUI.Width and rotates the Rat
    /// </summary>
    /// <returns></returns>
    float RandomX()
    {
        float rnd = UnityEngine.Random.value;
        if (rnd < .5)
        {
            _pos.localScale = new Vector3(-1, 1, 1);
            return Mathf.Abs(_gameUI.rect.width / 2);
        }
        else 
        {
            _pos.localScale = new Vector3(1, 1, 1);
            return Mathf.Abs(_gameUI.rect.width / 2) * (-1);
        }
    }

    /// <summary>
    /// When Player clicks on Rat it will speed up and move 2x faster per click
    /// </summary>
    private void OnMouseDown()
    {
        _movementSpeed *= 2;
        if (_movementDelay > 0.01f)
        {
            _movementDelay = 0.01f;
        }
    }
}
