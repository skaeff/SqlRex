using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRex
{
    
    public static class RegexValues
    {
        public static string DdlObjectsPreparedProcViewTrigger = @"\b(create)\s+(procedure|proc|trigger|view|function)\b\s([^=<>\s\']+).([^=<>\s\'\(]+)[*\s\b\t]*";

        public static string DdlObjectsPreparedWithIndex = @"\b(create|alter)\s+(unique)*\s*(nonclustered)*\s*(procedure|proc|table|trigger|view|function|index)\b\s([^=<>\s\']+).([^=<>\s\'\(]+)[*\s\b\t]*";

        public static string SqlCmdObjects = @"\[\$\(([^=<>\[\]\s\']+)\)\].\[[^=<>\s\']+\]";
        
        public static string SqlCmdObjectsShort = @"\$\(([^=<>\[\]\s\']+)\)";

        
        public static string DdlObjects = @"\b(create)\s+(procedure|proc|table|trigger|view|function)\b\s\[\$\(([^=<>\[\]\s\']+)\)\].\[[^=<>\s\']+\]";
        public static string DdlObjectsPrepared = @"\b(create)\s+(procedure|proc|table|trigger|view|function)\b\s\[([^=<>\[\]\s\']+)\].(\[[^=<>\s\']+\])";

        public static string DdlObjects_ = @"\b(create)\s+(procedure|proc|table|trigger|view|function)\b\s\$\(([^=<>\[\]\s\']+)\).[^=<>\s\']+";
        
        public static string DdlObjectsPrepared_ = @"\b(create)\s+(procedure|proc|table|trigger|view|function)\b\s([^=<>\s\']+).([^=<>\s\'\(]+)[*\s\b\t]*";

        

        public static string DdlIndexAll = @"\b(create|alter)\s+(procedure|proc|table|trigger|view|function)\b\s\[([^=<>\[\]\s\']+)\].\[[^=<>\s\']+\]";

        public static string Variables = @"\@([^=<>\s\'\)\(\,]+)";
        public static string SqlCmdVariables = @"\:SETVAR\s+([a-zA-Z_]+)\s+([a-zA-Z_]+)";
    }
}
