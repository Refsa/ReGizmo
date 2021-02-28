

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ReGizmo.Generator
{
    public struct Variable
    {
        public System.Type ValueType;
        public string ArgName;
        public string MethodArgName;
        public string DefaultName;

        public Variable(System.Type type, string paramName, string defaultName, string methodArgName)
        {
            ValueType = type;
            ArgName = paramName;
            DefaultName = defaultName;
            MethodArgName = methodArgName;
        }
    }

    public struct Parameters
    {
        public Variable[] Variables;

        public Parameters(Variable[] variables)
        {
            Variables = variables;
        }
    }

    public static class VariableGenerator
    {

    }

    public static class Permutation
    {
        static VariableComparer variableComparer = new VariableComparer();
        static VariableArrayComparer variableArrayComparer = new VariableArrayComparer();
        static HashSet<int> seenOverrides = new HashSet<int>();

        public static IEnumerable<(string, string)> GenerateOverrides(Variable[] variables)
        {
            seenOverrides.Clear();

            var paramsWithNulls = new Variable[variables.Length * 2];

            for (int i = 0; i < variables.Length; i++)
            {
                paramsWithNulls[i] = variables[i];
                var noType = variables[i];
                noType.ValueType = null;
                paramsWithNulls[i + variables.Length] = noType;
            }

            string output = "";

            var permutations =
                GetPermutations(paramsWithNulls, variables.Length)
                .Select(v =>
                {
                    var arr = v.ToArray();
                    SortPermutationArray(ref arr, variables);
                    return arr;
                })
                .Where(v => VerifyPermutation(v, variables))
                .OrderByDescending(ActiveParametersCount);
                // .Distinct(variableArrayComparer);

            /* permutations = permutations
                .Where(v =>
                {
                    int paramCount = ActiveParametersCount(v);
                    foreach (var other in permutations
                        .Where(e => ActiveParametersCount(e) == paramCount && e != v))
                    {
                        for (int i = 0; i < variables.Length; i++)
                        {

                        }
                    }
                }); */

            foreach (var perm in permutations)
            {
                yield return GenerateParameters(perm);
            }
        }

        static int ActiveParametersCount(Variable[] v)
        {
            return v.Sum(e => e.ValueType != null ? 1 : 0);
        }

        static (string, string) GenerateParameters(Variable[] parameters)
        {
            string pushParams = "";
            string inParams = "";

            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];

                if (param.ValueType == null)
                {
                    pushParams += param.DefaultName;
                    if (i < parameters.Length - 1) pushParams += ", ";
                }
                else
                {
                    pushParams += param.MethodArgName;
                    inParams += param.ValueType + " " + param.ArgName;
                    if (i < parameters.Length - 1)
                    {
                        pushParams += ", ";
                        bool isLastNotNull = false;
                        for (int j = i + 1; j < parameters.Length; j++)
                        {
                            isLastNotNull = parameters[j].ValueType != null;
                            if (isLastNotNull) break;
                        }
                        if (isLastNotNull)
                            inParams += ", ";
                    }
                }
            }

            return (pushParams, inParams);
        }

        // TODO: in-place
        static void SortPermutationArray(ref Variable[] parameters, Variable[] originalSet)
        {
            Variable[] stored = new Variable[originalSet.Length];
            for (int i = 0; i < originalSet.Length; i++)
            {
                string argName = parameters[i].ArgName;
                int actualIndex = System.Array.FindIndex(originalSet, e => e.ArgName == argName);
                stored[actualIndex] = parameters[i];
            }

            for (int i = 0; i < originalSet.Length; i++)
            {
                parameters[i] = stored[i];
            }
        }

        // TODO: Currently only handles 4 parameters in a specific order
        static bool VerifyPermutation(Variable[] parameters, Variable[] originalSet)
        {
            for (int i = 0; i < originalSet.Length; i++)
            {
                if (parameters[i].ArgName == null) return false;
                if (parameters[i].DefaultName == null) return false;
            }

            if (parameters.Distinct(variableComparer).Count() != parameters.Length) return false;

            /* bool hasScale = parameters[2].ValueType != null;
            bool isAlone = parameters[0].ValueType == null && parameters[1].ValueType == null && parameters[3].ValueType == null;

            bool hasRotation = parameters[3].ValueType != null;
            bool isAloneWithScale = parameters[0].ValueType == null && parameters[1].ValueType == null;

            if ((hasScale && isAlone) || (hasRotation && hasScale && isAloneWithScale))
            {
                return false;
            } */

            return true;
        }

        static int GetHashCode(Variable[] parameters)
        {
            int hCode = 1 << 32;
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ValueType != null)
                {
                    hCode ^= parameters[i].ValueType.Name.GetHashCode();
                }

                hCode ^= parameters[i].ArgName.GetHashCode() ^ parameters[i].DefaultName.GetHashCode();
            }

            return hCode.GetHashCode();
        }

        static IEnumerable<IEnumerable<Variable>> GetPermutations(IEnumerable<Variable> items, int count)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                {
                    yield return new Variable[] { item };
                }
                else
                {
                    foreach (var result in GetPermutations(items.Skip(i + 1), count - 1))
                    {
                        yield return new Variable[] { item }.Concat(result);
                    }
                }

                i++;
            }
        }

        static int VariationCount(int length) => length == 0 ? 1 : length * VariationCount(length - 1);

        class VariableComparer : IEqualityComparer<Variable>
        {
            public bool Equals(Variable x, Variable y)
            {
                return
                    x.ArgName == y.ArgName && x.DefaultName == y.DefaultName;
            }

            public int GetHashCode(Variable obj)
            {
                return
                    (obj.ArgName.GetHashCode() ^ obj.DefaultName.GetHashCode()).GetHashCode();
            }
        }

        class VariableArrayComparer : IEqualityComparer<Variable[]>
        {
            public bool Equals(Variable[] x, Variable[] y)
            {
                if (x.Length != y.Length || ActiveParametersCount(x) != ActiveParametersCount(y)) return false;

                return true;
            }

            public int GetHashCode(Variable[] obj)
            {
                int hashCode = 0;

                foreach (var o in obj)
                {
                    hashCode ^=
                        (o.ArgName.GetHashCode() ^ o.DefaultName.GetHashCode()).GetHashCode();
                }

                return hashCode.GetHashCode();
            }
        }
    }
}