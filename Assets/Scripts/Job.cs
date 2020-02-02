﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Job : MonoBehaviour
{
    /// <summary>
    /// Time the Job started
    /// </summary>
    public float StartTime { get; }

    /// <summary>
    /// Location of the Job
    /// </summary>
    public Vector2 Jobsite { get; }

    [Header("Config")]
    [Tooltip("This thing you need to interact with to finisht the job")]
    public GameObject Objective;

    [Tooltip("The person who ejactulates the job description")]
    public GameObject Client;

    [Tooltip("Job blurb text")]
    public TextMeshProUGUI Text;

    [Tooltip("Job text")]
    public string blurb = "My name is A'dumb!";

    [Tooltip("Default response")]
    public string DefaultResponse = "That's not very helpful";

    [Tooltip("Scoring tools for the job and their score\nUnspecified tool will default to a score of 0")]
    public ToolScorePair[] Tools;

    private bool AtJobsite = false;

    #region Events

    public event EventHandler JobCompleted;
    private void OnJobCompleted(JobCompletedEventArgs e)
    {
        EventHandler Handler = JobCompleted;
        Debug.Log("Job's finished!");
        Handler?.Invoke(this, e);
    }

    public event EventHandler JobsiteReached;
    private void OnJobsiteReached(EventArgs e)
    {
        EventHandler Handler = JobsiteReached;
        Handler?.Invoke(this, e);
    }

    #endregion

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        Text.text = "";
    }

    private void Update()
    {
        if (AtJobsite == true && Input.GetKeyDown(KeyCode.Q))
        {
            StopMiniGame();
        }
    }

    /// <summary>
    /// On Trigger Enter
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (AtJobsite == false && collision.gameObject.tag == "Player")
        {
            StartMinigame();
        }
    }

    /// <summary>
    /// This is mostly to make sure this isn't doing anything 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision: " + collision.gameObject.name);
    }

    /// <summary>
    /// Start Minigame
    /// </summary>
    private void StartMinigame()
    {
        AtJobsite = true;
        OnJobsiteReached(new EventArgs());

        Text.text = blurb;
    }

    private void StopMiniGame()
    {
        AtJobsite = false;
        Text.text = DefaultResponse;
        OnJobCompleted(new JobCompletedEventArgs());
    }
}