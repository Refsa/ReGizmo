
using UnityEditor;

namespace ReGizmo.Generator
{
    internal abstract class DrawGenerator
    {
        protected string fileShell = 
@"
using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {
        $CONTENT
    }
}";
        protected string methodShell;
        protected string methodName;
        protected string fileName;

        protected Variable[] variables;

        public virtual void Generate(string targetFolder)
        {
            string content = fileShell.Replace("$CONTENT", GenerateInternal());

            System.IO.File.WriteAllText(targetFolder + fileName, content);

            AssetDatabase.Refresh();
        }

        protected abstract string GenerateInternal();
    }
}