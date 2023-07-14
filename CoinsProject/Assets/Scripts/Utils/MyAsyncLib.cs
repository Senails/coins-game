using System;
using System.Threading.Tasks;

using static MyDateLib;

static class AsyncLib {
    //отложеный запуск с возможностью отмены
    static public Action setTimeout(AsyncAction action,int ms){
        bool flag = false;

        Func<Task> task = async ()=>{
            await Task.Delay(ms);
            if (flag) return;
            await action();   
        };
        task();

        return ()=>{
            flag = true;
        };
    }
    static public Action setTimeout(Action action,int ms){
        bool flag = false;

        Func<Task> task = async ()=>{
            await Task.Delay(ms);
            if (flag) return;
            action();   
        };
        task();

        return ()=>{
            flag = true;
        };
    }
    
    
    //интервальный запуск до отмены
    static public Action setInterval(Action action,int ms){
        bool flag = false;

        Func<Task> task = async ()=>{
            while (true){
                if (flag) return;
                await Task.Delay(ms);
                action(); 
            }  
        };
        task();

        return ()=>{
            flag = true;
        };
    }
    static public Action setInterval(AsyncAction action,int ms){
        bool flag = false;

        Func<Task> task = async ()=>{
            while (true){
                if (flag) return;
                await Task.Delay(ms);
                await action(); 
            }  
        };
        task();

        return ()=>{
            flag = true;
        };
    }


    //создает функцию которая принимает колбек , но срабатывает с ограничением по времени
    static public Action<Action> CreateTrotlingFunc(int ms){
        long lastUse = getDateMilisec(); 

        return (Action callback)=>{
            long now = getDateMilisec();
            if ( now-lastUse < ms) return;

            lastUse = now;
            callback.Invoke();
        };
    }


    public delegate Task AsyncAction();
    public record dontCloseRecord {
        public Task closeTask; 
        public Action actionForClose;

        public dontCloseRecord (Task closeTask , Action actionForClose){
            this.closeTask = closeTask;
            this.actionForClose = actionForClose;
        }
    };
}
