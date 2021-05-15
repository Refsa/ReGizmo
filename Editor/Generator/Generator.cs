

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ReGizmo.Generator
{
    internal struct Variable
    {
        public System.Type ValueType;
        public string ArgName;
        public string MethodArgName;
        public string DefaulValue;
        public int Priority;

        /// <summary>
        /// Create a parameter that goes into a variable
        /// </summary>
        /// <param name="type">Type of the varialbe</param>
        /// <param name="paramName">Function parameter name of the variable</param>
        /// <param name="defaultValue">Default value for the variable</param>
        /// <param name="variableExpr">Expression of variable when used</param>
        /// <param name="priority">Higher means it will have priority over other variables</param>
        public Variable(System.Type type, string paramName, string defaultValue, string variableExpr, int priority = 0)
        {
            ValueType = type;
            ArgName = paramName;
            DefaulValue = defaultValue;
            MethodArgName = variableExpr;
            Priority = priority;
        }
    }

    internal static class Permutation
    {
        static VariableComparer variableComparer = new VariableComparer();
        static VariableArrayComparer variableArrayComparer = new VariableArrayComparer();
        static HashSet<int> seenOverrides = new HashSet<int>();

        // TODO: This be a mess
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

            var permutations =
                GetPermutations(paramsWithNulls, variables.Length)
                .Select(v =>
                {
                    var arr = v.ToArray();
                    SortPermutationArray(ref arr, variables);
                    return arr;
                })
                .Where(v => VerifyPermutation(v, variables))
                .OrderByDescending(ActiveParametersCount)
                .Distinct(variableArrayComparer)
                .ToList();

            /*
            - Find all configs of same active parameter counts
            - Find the ones that are equal in types
            - Discard the ones with lower priority
            */

            List<(int, Variable[])>[] buckets = new List<(int, Variable[])>[variables.Length - 1];
            for (int i = 0; i < buckets.Length; i++) buckets[i] = new List<(int, Variable[])>();
            for (int i = 0; i < permutations.Count; i++)
            {
                var perm = permutations[i];

                int activeParams = ActiveParametersCount(perm);
                if (activeParams == 0 || activeParams == variables.Length) continue;

                buckets[activeParams - 1].Add(
                    (i, perm.Where(e => e.ValueType != null).ToArray())
                );
            }

            List<int> toRemove = new List<int>();
            for (int i = 0; i < buckets.Length; i++)
            {
                (int, int, int)[] hashCodes = new (int, int, int)[buckets[i].Count];
                for (int j = 0; j < buckets[i].Count; j++)
                {
                    hashCodes[j].Item1 = buckets[i][j].Item1;
                    hashCodes[j].Item2 = ActiveParametersTypeHash(buckets[i][j].Item2);
                    hashCodes[j].Item3 = ParametersPriority(buckets[i][j].Item2);
                }

                for (int j = 0; j < hashCodes.Length; j++)
                {
                    for (int k = j; k < hashCodes.Length; k++)
                    {
                        if (k == j || toRemove.Contains(hashCodes[k].Item1) || toRemove.Contains(hashCodes[j].Item1)) continue;

                        if (hashCodes[j].Item2 == hashCodes[k].Item2)
                        {
                            if (hashCodes[j].Item3 > hashCodes[k].Item3)
                            {
                                toRemove.Add(hashCodes[k].Item1);
                            }
                            else
                            {
                                toRemove.Add(hashCodes[j].Item1);
                            }
                        }
                    }
                }
            }

            foreach (int rem in toRemove.OrderByDescending(e => e))
            {
                permutations.RemoveAt(rem);
            }

            foreach (var perm in permutations)
            {
                yield return GenerateParameters(perm);
            }
        }

        static int ActiveParametersCount(Variable[] v)
        {
            return v.Sum(e => e.ValueType != null ? 1 : 0);
        }

        static int ActiveParametersTypeHash(Variable[] v)
        {
            return v.Aggregate(0, (acc, val) =>
            {
                return acc ^ val.ValueType.Name.GetHashCode();
            });
        }

        static int ParametersPriority(Variable[] v)
        {
            return v.Sum(e => e.Priority);
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
                    pushParams += param.DefaulValue;
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

        static bool VerifyPermutation(Variable[] parameters, Variable[] originalSet)
        {
            if (parameters == null || parameters.Length == 0 || parameters.Length != originalSet.Length)
            {
                return false;
            }

            for (int i = 0; i < originalSet.Length; i++)
            {
                if (parameters[i].ArgName == null) return false;
                if (parameters[i].DefaulValue == null) return false;
            }

            if (parameters.Distinct(variableComparer).Count() != parameters.Length) return false;

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

                hCode ^= parameters[i].ArgName.GetHashCode() ^ parameters[i].DefaulValue.GetHashCode();
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
                    x.ArgName == y.ArgName && x.DefaulValue == y.DefaulValue;
            }

            public int GetHashCode(Variable obj)
            {
                return
                    (obj.ArgName.GetHashCode() ^ obj.DefaulValue.GetHashCode()).GetHashCode();
            }
        }

        class VariableArrayComparer : IEqualityComparer<Variable[]>
        {
            public bool Equals(Variable[] x, Variable[] y)
            {
                if (x.Length != y.Length || ActiveParametersCount(x) != ActiveParametersCount(y)) return false;

                int equals = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i].ValueType != null && x[i].ValueType == y[i].ValueType)
                    {
                        equals++;
                    }
                }

                if (equals == ActiveParametersCount(x))
                {
                    return true;
                }

                return false;
            }

            public int GetHashCode(Variable[] obj)
            {
                int hashCode = 0;

                foreach (var o in obj)
                {
                    hashCode ^=
                        (o.ArgName.GetHashCode() ^ o.DefaulValue.GetHashCode()).GetHashCode();
                }

                return hashCode.GetHashCode();
            }
        }
    }
}