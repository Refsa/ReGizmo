using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PerformanceRunner : MonoBehaviour
{
    FrameTimeDebug _frameTimeDebug;

    [SerializeField] List<PerformanceTest> tests;
    int currentTest = 0;

    List<string> results;

    bool running = false;
    float startTime = 0f;
    float baseLine = 0f;

    void Awake()
    {
        Application.targetFrameRate = 0;
        QualitySettings.vSyncCount = 0;

        results = new List<string>();
        _frameTimeDebug = FindObjectOfType<FrameTimeDebug>();
    }

    void Start()
    {
        StartCoroutine(BaselineWarmup());
    }

    IEnumerator BaselineWarmup()
    {
        if (tests.Count == 0)
        {
            Debug.Log("No performance tests registered");
            yield break;
        }

        while (Time.frameCount < 100) yield return null;
        startTime = Time.time;
        _frameTimeDebug.Clear();

        while (Time.time - startTime < 10f)
        {
            yield return null;
        }

        baseLine = _frameTimeDebug.LastAvgFrameTime;

        Debug.Log($"Baseline FPS: {baseLine} fps");
        results.Add($"Baseline FPS: {baseLine} fps");

        StartCoroutine(RunTest(tests[currentTest]));
    }

    IEnumerator RunTest(PerformanceTest test)
    {
        test.Prepare();

        while (test.Warmup())
        {
            yield return null;
        }

        while (test.Run())
        {
            yield return null;
        }

        string result = $"{test.GetType().Name}: {test.AverageFrameTime} fps - {test.AverageFrameTime / baseLine}%";
        results.Add(result);
        Debug.Log(result);

        if (++currentTest < tests.Count)
        {
            StartCoroutine(RunTest(tests[currentTest]));
        }
    }

    Vector2 scrollPos = Vector2.zero;

    void OnGUI()
    {
        Rect rect = new Rect(Screen.width - 500f, 0f, 500f, 500f);

        using (new GUILayout.AreaScope(rect, "", GUI.skin.box))
        {
            using (var scrollView = new GUILayout.ScrollViewScope(scrollPos, false, true))
            {
                using (new GUILayout.VerticalScope())
                {
                    foreach (var result in results)
                    {
                        using (new GUILayout.HorizontalScope(GUI.skin.box))
                        {
                            GUILayout.Label(result);
                        }
                    }
                }

                scrollPos = scrollView.scrollPosition;
            }
        }
    }
}