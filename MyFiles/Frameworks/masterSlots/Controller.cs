using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller 
{
    public string Name;

/*    public void log(string str)
    {
        Debug.Log($"{Name} {str}");
    }*/

    protected Model GetModel<T>() where T:Model
    {
        return MVC.GetModel<T>();
    }

    protected View GetView<T>() where T:View
    {
        return MVC.GetView<T>();
    }

    /*    protected void RegisterModel(Model model)
        {
            MVC.RegisterModel(model);
        }

        protected void RegisterView(View view)
        {
            MVC.RegisterView(view);
        }

        protected void RegisterController(System.Type controllerType)
        {
            MVC.RegisterController( controllerType);
        }*/

/*    internal static void RegisterModel<T>() where T : Model
    {
        MVC.RegisterModel<T>();
    }

    internal static void RegisterView<T>() where T : View
    {
        MVC.RegisterView<T>();
    }

    internal static void RegisterController(string typeName)
    {
        MVC.RegisterController(typeName);
    }
*/

    public abstract void HandleEvent(object data);
}
