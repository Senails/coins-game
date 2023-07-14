using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace OptionsTypes{

    public record OptionsConfig{
        public MoveModeEnum MoveMode {get;set;} = MoveModeEnum.hybrid;
        public Dictionary<string,KeyCode> KyeDictionary {get;set;} = new Dictionary<string,KeyCode>{
            { "Вверх" , KeyCode.W},
            { "Вниз" , KeyCode.S},
            { "Влево" , KeyCode.A},
            { "Вправо" , KeyCode.D},

            { "Карта" , KeyCode.M},
            { "Инвентарь" , KeyCode.I},
            { "Взаимодействие" , KeyCode.E},
            { "Атака" , KeyCode.Space},

            { "Ячейка 1" , KeyCode.Alpha1},
            { "Ячейка 2" , KeyCode.Alpha2},
            { "Ячейка 3" , KeyCode.Alpha3},
            { "Ячейка 4" , KeyCode.Alpha4},
            { "Ячейка 5" , KeyCode.Alpha5},
        };
    }


    public enum MoveModeEnum{
        keyboard,
        mouse,
        hybrid,
    }
}