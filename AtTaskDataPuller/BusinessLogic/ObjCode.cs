using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ObjCode
    {
        public static readonly ObjCode PROJECT = new ObjCode("project");
        public static readonly ObjCode TASK = new ObjCode("task");
        public static readonly ObjCode ISSUE = new ObjCode("optask");
        public static readonly ObjCode OPTASK = new ObjCode("optask");
        public static readonly ObjCode TEAM = new ObjCode("team");
        public static readonly ObjCode HOUR = new ObjCode("hour");
        public static readonly ObjCode TIMESHEET = new ObjCode("tshet");
        public static readonly ObjCode USER = new ObjCode("user");
        public static readonly ObjCode ASSIGNMENT = new ObjCode("assgn");
        public static readonly ObjCode USER_PREF = new ObjCode("userpf");
        public static readonly ObjCode CATEGORY = new ObjCode("ctgy");
        public static readonly ObjCode CATEGORY_PARAMETER = new ObjCode("ctgypa");
        public static readonly ObjCode PARAMETER = new ObjCode("param");
        public static readonly ObjCode PARAMETER_GROUP = new ObjCode("pgrp");
        public static readonly ObjCode PARAMETER_OPTION = new ObjCode("popt");
        public static readonly ObjCode PARAMETER_VALUE = new ObjCode("pval");
        public static readonly ObjCode ROLE = new ObjCode("role");
        public static readonly ObjCode GROUP = new ObjCode("group");
        public static readonly ObjCode NOTE = new ObjCode("note");
        public static readonly ObjCode DOCUMENT = new ObjCode("docu");
        public static readonly ObjCode DOCUMENT_VERSION = new ObjCode("docv");
        public static readonly ObjCode EXPENSE = new ObjCode("expns");
        public static readonly ObjCode CUSTOM_ENUM = new ObjCode("custem");

        /// <summary>
        /// String representation of the ObjCode
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Creates a new ObjCode with the given value
        /// </summary>
        /// <param name="val">
        /// A <see cref="System.String"/>
        /// </param>
        private ObjCode(string val)
        {
            this.Value = val;
        }

        /// <summary>
        /// Returns <see cref="AtTaskRestExample.ObjCode"/>.Value
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/>
        /// </returns>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// Compares this.Value to the given value.
        /// This means that:
        /// myObjCode.equals(ObjCode.NOTE)
        /// is the same as:
        /// myObjCode.equals("note");
        /// </summary>
        /// <param name="obj">
        /// A <see cref="System.Object"/>
        /// </param>
        /// <returns>
        /// A <see cref="System.Boolean"/>. True if the objects represent the same ObjCode.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is string)
            {
                return Value.Equals((obj as string).ToLower());
            }
            return Value.Equals(obj);
        }

        /// <summary>
        /// Returns the hash code of Value
        /// </summary>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
