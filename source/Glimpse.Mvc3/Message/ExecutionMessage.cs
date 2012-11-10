﻿using System;
using System.Reflection;
using System.Web.Mvc;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Message;

namespace Glimpse.Mvc.Message
{
    public class ExecutionMessage : TimerResultMessage, IExecutionMessage
    {
        public ExecutionMessage(FilterCategory? filterCategory, Type filterType, MethodInfo method, TimerResult timerResult, ControllerBase controllerBase)
            : base(timerResult, Simplify(method.Name), "Filter")
        {
            // IsChildAction is false if ControllerContext is null
            IsChildAction = controllerBase.ControllerContext != null && controllerBase.ControllerContext.IsChildAction;
            Category = filterCategory;
            ExecutedType = filterType;
            ExecutedMethod = method; 
        }

        public bool IsChildAction { get; private set; }

        public FilterCategory? Category { get; private set; }

        public Type ExecutedType { get; private set; }

        public MethodInfo ExecutedMethod { get; private set; } 

        private static string Simplify(string methodName)
        {
            var nameParts = methodName.Split('.');
            return nameParts[nameParts.Length - 1];
        }
    }
}