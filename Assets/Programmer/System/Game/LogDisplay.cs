using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class LogDisplay : MonoBehaviour
{
    // Maximun number of logs saved
    [SerializeField] int m_MaxLogCount = 20;
    // Area to display logs
    [SerializeField] Rect m_Area = new Rect(50, 0, 1000, 400);
    // Variable to put the log string
    Queue<string> m_LogMessages = new Queue<string>();
    // This is used to combine the strings in the log
    StringBuilder m_StringBuilder = new StringBuilder();

    // Start is called before the first frame update
    void Start()
    {
        // Register the function to be called when outputting the log in Application.logMessageReceived
        Application.logMessageReceived += LogReceived;
    }

    // LogReceived is called when the log is output
    void LogReceived(string text, string stackTrace, LogType type)
    {
        // Add logs to Queue
        m_LogMessages.Enqueue(text);
        // If the number of logs exceeds the upper limit, delete the oldest one
        while(m_LogMessages.Count > m_MaxLogCount)
        {
            m_LogMessages.Dequeue();
        }
    }

    void OnGUI()
    {
        // Reset the contents of StringBuilder
        m_StringBuilder.Length = 0;

        // Combine log strings
        foreach (string s in m_LogMessages)
        {
            m_StringBuilder.Append(s).Append(System.Environment.NewLine);
        }

        // Display on screen
        GUI.Label(m_Area, m_StringBuilder.ToString());
    }
}
