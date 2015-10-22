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

        [SerializeField] private int eventSecondsLater = 10;

        private void Awake()
        {
            startedDateText.text = string.Empty;
            eventDateText.text = string.Empty;
        }

        // Uma coisa para quem não sabe, mas você pode utilizar o methodo Start do monobehaviour como IEnumerator direto :)
        private IEnumerator Start()
        {
            //Pegando a data atual
            startedDateText.text = DateTime.Now.ToShortTimeString();

            //Esperando quantos segundos eu setar no editor
            yield return new WaitForSeconds(eventSecondsLater);
            //Mostrando a mensagem do event
            eventDateText.text = string.Format("EVENT! {0} seconds later the start time ({1})", eventSecondsLater,
                startedDateText.text);
        }
    }
}
