using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [Header("Time Settings")]
    public float realSecondsPerTick = 10f;
    public int minutesPerTick = 10;

    [Header("Starting Time")]
    public int startDay = 1;
    public int startHour = 8;
    public int startMinute = 0;

    [Header("Current Time")]
    public int currentDay;
    public int currentHour;
    public int currentMinute;

    private float timer;

    public event Action OnTimeChanged;
    public event Action OnDayChanged;

    private void Awake(){
        if (Instance != null && Instance != this){

            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start(){

        currentDay = startDay;
        currentHour = startHour;
        currentMinute = startMinute;

        NotifyTimeChanged();
    }

    private void Update(){

        timer += Time.deltaTime;

        while (timer >= realSecondsPerTick){

            timer -= realSecondsPerTick;
            AdvanceTime(minutesPerTick);
        }
    }

    private void AdvanceTime(int minutesToAdd){

        currentMinute += minutesToAdd;

        while (currentMinute >= 60){

            currentMinute -= 60;
            currentHour++;
        }

        while (currentHour >= 24){

            currentHour -= 24;
            currentDay++;
            OnDayChanged?.Invoke();
        }

        NotifyTimeChanged();
    }

    private void NotifyTimeChanged(){

        OnTimeChanged?.Invoke();
    }

    public string GetFormattedTime(){

        int displayHour = currentHour % 12;
        if (displayHour == 0) displayHour = 12;

        string amPm = currentHour >= 12 ? "PM" : "AM";
        return $"{displayHour:00}:{currentMinute:00} {amPm}";
    }

    public string GetFormattedDay(){

        return $"Day {currentDay}";
    }


    public void SkipToNextDay(int hour = 8, int minute = 0){

        currentDay++;
        currentHour = hour;
        currentMinute = minute;

        OnDayChanged?.Invoke();
        NotifyTimeChanged();
    }



    public void SetTime(int hour, int minute){
        
        currentHour = Mathf.Clamp(hour, 0, 23);
        currentMinute = Mathf.Clamp(minute, 0, 59);

        NotifyTimeChanged();
    }
}