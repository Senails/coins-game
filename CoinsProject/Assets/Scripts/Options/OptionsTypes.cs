using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace OptionsTypes{

    public record OptionsConfig{
        public MoveModeEnum MoveMode {get;set;} = MoveModeEnum.hybrid;
        public Dictionary<string,KeyCode> KyeDictionary {get;set;} = new Dictionary<string,KeyCode>{
            { "вверх" , KeyCode.W},
            { "вниз" , KeyCode.S},
            { "влево" , KeyCode.A},
            { "вправо" , KeyCode.D},

            { "Карта" , KeyCode.M},
            { "Инвентарь" , KeyCode.I},
            { "Взаимодействие" , KeyCode.E},
            { "Атака" , KeyCode.A},

            { "ячейка 1" , KeyCode.Alpha1},
            { "ячейка 2" , KeyCode.Alpha2},
            { "ячейка 3" , KeyCode.Alpha3},
            { "ячейка 4" , KeyCode.Alpha4},
            { "ячейка 5" , KeyCode.Alpha5},
        };
    }


    public enum MoveModeEnum{
        keyboard,
        mouse,
        hybrid,
    }
}