
using UnityEditor;

namespace ReGizmo.Generator
{
    internal abstract class DrawGenerator
    {
        protected string fileShell =
@"
using UnityEngine;
using ReGizmo.Utils;

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

        protected string GenerateHelper(string defaultParams)
        {
            string content = "";

            foreach (var perm in Permutation.GenerateOverrides(variables))
            {
                string method = methodShell;

                string arguments = defaultParams;
                if (!string.IsNullOrEmpty(perm.Item2))
                {
                    if (!string.IsNullOrEmpty(arguments)) arguments += ", ";
                    arguments += perm.Item2 + ", DepthMode depthMode = DepthMode.Sorted";
                }
                else
                {
                    if (!string.IsNullOrEmpty(arguments)) arguments += ", ";
                    arguments += "DepthMode depthMode = DepthMode.Sorted";
                }

                method = method.Replace("$PARAMS", arguments);

                string[] chars = perm.Item1.Split(',');
                for (int i = 0; i < chars.Length; i++)
                {
                    method = method.Replace($"$PARAM_{i + 1}", chars[i].Trim());
                }
                content += method;
            }

            return content;
        }
    }
}