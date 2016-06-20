using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DateExample
{
    public class Example : MonoBehaviour
    {
        [SerializeField] private Text startedDateText;
        [SerializeField] private Text eventDateText;

        [SerializeField] private int eventStartHour = 23;
        [SerializeField] private int eventEndHour = 24;

        private TimeSpan startTimeSpan;
        private TimeSpan endTimeSpan;

        private void Awake()
        {
            startedDateText.text = string.Empty;
            eventDateText.text = string.Empty;

            startTimeSpan = new TimeSpan(eventStartHour, 0,0);
            endTimeSpan = new TimeSpan(eventEndHour, 0,0);
        }

        private void Start()
        {
            //Pegando a data atuals
            startedDateText.text = DateTime.Now.ToShortTimeString();

            StartCoroutine(CheckForEventTimeCoroutine());
        }

        private bool IsTimeBetween(DateTime targetDatetime, TimeSpan targetStart, TimeSpan targetEnd)
        {
            // convert datetime to a TimeSpan
            TimeSpan now = targetDatetime.TimeOfDay;
            // see if start comes before end
            if (targetStart < targetEnd)
                return targetStart <= now && now <= targetEnd;
            // start is after end, so do the inverse comparison
            return !(targetEnd < now && now < targetStart);
        }

        private IEnumerator CheckForEventTimeCoroutine()
        {
            while (true)
            {
                if (IsTimeBetween(DateTime.Now, startTimeSpan, endTimeSpan))
                    eventDateText.text = string.Format("HORA DO EVENTO :)");
                else
                    eventDateText.text = string.Format("evento fechado :(");

                //Esperando 1 minuto antes de checar de novo
                yield return new WaitForSeconds(60);
            }

        }
    }
}
