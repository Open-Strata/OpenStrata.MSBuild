﻿// Copyright (c) 74Bravo LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project or repository root for license information.

using System;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace OpenStrata.MSBuild.Tasks
{

    public abstract class BaseTask : Microsoft.Build.Utilities.Task
    {

        private string taskName;

        public sealed override bool Execute()
        {
            try
            {

                taskName = this.GetType().Name;

                Log.LogMessage($"OpenStrata Task: {taskName} has started.");
                //TODO:  Do some logging activites and centralized error handling.
                if (!ExecuteTask())
                {
                    Log.LogMessage($"OpenStrata Task: {taskName} has do not finish.");
                    return false;

                }
                Log.LogMessage($"OpenStrata Task: {taskName} has finished.");
                return true;
            }
            catch (Exception ex)
            {

                return TaskFailed(ex);
            }
        }

        public abstract bool ExecuteTask();

        public bool TaskFailed(string message)
        {
            Log.LogError($"OpenStrata Task: {taskName} : Failed : {message}");
            return false;
        }

        public bool TaskFailed(Exception exception)
        {

            Log.LogError(new StringBuilder()
                       .AppendLine($"OpenStrata Task: {taskName} : Failed : {exception.Source}")
                       .BuildExceptionMessageBlock(exception)
                       .ToString());

            return false;
        }

        public bool TaskSuccess(string message = "Finished successfully.")
        {
            Log.LogMessage($"OpenStrata Task: {taskName} : Success : {message}");
            return true;
        }

        public bool TaskFinishedWithWarning(string message)
        {
            Log.LogWarning($"OpenStrata Task: {taskName} : Warning : {message}");
            return true;
        }

        public void LogMessage(string msg)
        {
            this.Log.LogMessage(msg);
        }

    }

    public static class BaseTaskExtensions
    {

        public static StringBuilder BuildExceptionMessageBlock(this StringBuilder sb, Exception ex)
        {
            if (ex == null) return sb;
            return sb.BuildExceptionMessageBlock(ex.InnerException)
                .AppendLine(ex.Message)
                .AppendLine(ex.StackTrace);
        }

        public static bool AsBoolean (this string value, bool defaultValue = false)
        {
            if (string.IsNullOrEmpty(value)) return defaultValue;

            if (Boolean.TryParse(value, out bool result)) return result;

            return defaultValue;

        }        

        public static void Log(this TaskLoggingHelper logger, string msg)
        {
            logger.LogMessage(msg);
        }


    }


}
