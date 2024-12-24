using System;
using System.Collections.Generic;
using MVC.Controller;

namespace MVC
{
    public class ControllerContainer : IDisposable
    {
        private readonly List<IController> _childrenControllers = new List<IController>();
        
        public void Dispose()
        {
            foreach (IController child in _childrenControllers)
            {
                child.Dispose();
            }
            
            _childrenControllers.Clear();
        }

        public void Add(IController controller)
        {
            _childrenControllers.Add(controller);
        }
    }
}