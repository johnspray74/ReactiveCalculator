using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Analytics.Operators;
using ProgrammingParadigms;


namespace DomainAbstractions
{
    class TransformOperator : IDataFlow<string>
    {
        // This class can transforms a mathametical expression using a (highest precedence) binary or unary operator to one using a function
        // e.g. "3^2" => "Pow(3,2)"
        // It tries to do it on the cheap, i.e. not fully parsing the expression. So we make as few assumptions about the syntax of the expression as possible.
        // We assume the operator being replaced has equal highest precedence. 
        // if the operator is binary, we assume, for the moment, that it is right associative
        // if the operator is unary, we assume, for the moment, that it takes its operand from the left
        // We assume alphas, numbers, decimal point, curly braces and their content, '+' and '-' that are unary operators, and '+' & '-' that are part of a scientific number make up an operand.



        // Properties
        public string InstanceName { get; set; } = "Default";


        // Ports
        // The IDatFlow<string> implemented interface is the input
        private IDataFlow<string> output;



        public TransformOperator(string mathOperator, string mathFunction, bool unary = false, bool rightAssociative = false )
        {
            this.mathOperator = mathOperator;
            this.mathFunction = mathFunction;
            this.unary = unary;
            this.rightAssociative = rightAssociative;
            PostWiringInitialize();
        }




        private string mathOperator = null;
        private string mathFunction = null;
        private bool unary = false;
        private bool rightAssociative = false;



        string IDataFlow<string>.Data { get => ""; set { output.Data = Transform(value); } }








        private string Transform(string expression)
        {
            int i = expression.IndexOf(mathOperator);
            if (i == -1) return expression; // don't mess with the string if the operator isn't in it - don't introduce bugs
            expression = Regex.Replace(expression, @"\s", "");
            List<string> substrings = UnpackBracketedSubstrings(expression);
            List<string> transformedSubstrings = new List<string>();
            foreach (string s in substrings) transformedSubstrings.Add(ReplaceOperator(s, mathOperator, mathFunction, unary, rightAssociative));
            return PackBracketedSubstrings(transformedSubstrings);
        }


        private void TestTransform()
        {
            assertStringEq(Transform("3+2"), "3+2");
            assertStringEq(Transform("2^3"), "Pow(2,3)");
            assertStringEq(Transform("a^b"), "Pow(a,b)");
            assertStringEq(Transform("2+3^4+5"), "2+Pow(3,4)+5");
            assertStringEq(Transform("(2+3)^(4+5)"), "Pow((2+3),(4+5))");
            assertStringEq(Transform("Sin(2)^Sqrt(3)"), "Pow(Sin(2),Sqrt(3))");
            assertStringEq(Transform("2^3^4"), "Pow(2,Pow(3,4))");
            assertStringEq(Transform("(2^3)^4"), "Pow((Pow(2,3)),4)");
            assertStringEq(Transform("((2+3)^4)^5"), "Pow((Pow((2+3),4)),5)");
            assertStringEq(Transform("2^(3^4)^(5^6)^7"), "Pow(2,Pow((Pow(3,4)),Pow((Pow(5,6)),7)))");
            assertStringEq(Transform("(2^3)"), "(Pow(2,3))");
            assertStringEq(Transform("(2+3)^(4*5)"), "Pow((2+3),(4*5))");
            assertStringEq(Transform("2^3^4"), "Pow(2,Pow(3,4))");
            assertStringEq(Transform("2^(3*(4+5)+6)^(7+8)"), "Pow(2,Pow((3*(4+5)+6),(7+8)))");
            assertStringEq(Transform("a^bb^c1"), "Pow(a,Pow(bb,c1))");
            assertStringEq(Transform("Pow(2,3)^Pow(4,5)^Pow(6,7)"), "Pow(Pow(2,3),Pow(Pow(4,5),Pow(6,7)))");
            assertStringEq(Transform("((2))^((3))^((4))"), "Pow(((2)),Pow(((3)),((4))))");
            assertStringEq(Transform("2^3 + 4^5 - 6^7 * 8^9 / 10^11"), "Pow(2,3)+Pow(4,5)-Pow(6,7)*Pow(8,9)/Pow(10,11)");
            assertStringEq(Transform("2 ^ ( 3 + 4 ) ^ ( 5 - 6 ) ^ ( 7 * 8 ) ^ ( 9 / 10 ) ^ 11"), "Pow(2,Pow((3+4),Pow((5-6),Pow((7*8),Pow((9/10),11)))))");

        }

/*
        private void TestTransform()
        {
        }
*/



        /// <summary>
        /// Move every bracketed substring into a list. Replace every bracketed substring with an "{i}" which indexes into the list. Do this recursively until there are no brackets left in the expression
        /// The purpose of this function is reduce the expression to just operators +,-,*,/ etc so that we can use regular expressions without worrying about brackets
        /// example: given "2^(3*(4+5)+6)^(7+8)" create the following list of strings
        ///0: "2^{1}^{2}"
        ///1: "3*{3}"
        ///2: "7+8"
        ///3: "4+5"
        /// After this is done we can deal with the substrings without worrying about brackets
        /// The reverse function PackBracketedSubstrings will reverse this
        /// TBD this would be easier using Regex
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private List<string> UnpackBracketedSubstrings(string expression)
        {

            List<string> bracketedSubstrings = new List<string>();
            bracketedSubstrings.Add(expression);
            int listIndex = 0;
            while (listIndex < bracketedSubstrings.Count)
            {
                int openBracketIndex = 0;
                int safety = 100;
                while (safety > 0)
                {
                    openBracketIndex = bracketedSubstrings[listIndex].IndexOf('(', openBracketIndex);
                    if (openBracketIndex == -1) break;
                    // Theres bracket in this here string
                    int closeBracketIndex = openBracketIndex+1;
                    int nesting = 0;
                    while (closeBracketIndex < bracketedSubstrings[listIndex].Length && (bracketedSubstrings[listIndex][closeBracketIndex] != ')' || nesting > 0))
                    {
                        if (bracketedSubstrings[listIndex][closeBracketIndex] == '(') nesting++;
                        if (bracketedSubstrings[listIndex][closeBracketIndex] == ')') nesting--;
                        closeBracketIndex++;
                    }
                    if (closeBracketIndex < bracketedSubstrings[listIndex].Length)
                    {
                        bracketedSubstrings.Add(bracketedSubstrings[listIndex].Substring(openBracketIndex + 1, closeBracketIndex - (openBracketIndex + 1)));
                        bracketedSubstrings[listIndex] = bracketedSubstrings[listIndex].Remove(openBracketIndex, closeBracketIndex - openBracketIndex + 1).Insert(openBracketIndex, "{" + (bracketedSubstrings.Count-1).ToString() + "}");
                    }
                    safety--;
                }
                listIndex++;
            }
            return bracketedSubstrings;
        }





        /// <summary>
        /// given the list of strings:
        /// 0: "Pow(2,Pow({1},{2}))"
        /// 1: "3*{3}"
        /// 2: "7+8"
        /// 3: "4+5"
        /// make: "Pow(2,Pow(3*(4+5)+6),(7+8)))"
        /// </summary>
        /// <param name="substrings"></param>
        /// <returns></returns>
        private string PackBracketedSubstrings(List<string> substrings)
        {
            Regex regex = new Regex(@"\{(?<index>\d*)\}");
            string rv = substrings[0];
            int safety = 100;
            while (safety > 0)
            {
                Match match = regex.Match(rv);
                if (!match.Success) break;
                int index = Int32.Parse(match.Groups["index"].Value);
                rv = regex.Replace(rv, "("+substrings[index]+")", 1);
                safety--;
            }
            return rv;
        }







        /// <summary>
        /// We will find the operands either side of the given operator and transform to use the given function.
        /// For example Transform("2*3^4+5","^","Pow") returns "2*Pow(3,4)+5"
        /// We find the operands by taking all alpha-numeric, decimal point, and whitespace characters and also '{' and '}' and their content regardless - these are metadata.
        /// a special case is that for the left operator, we also take a minus or plus sign if when we continue to scan further to the left we find no more alphanumeric or '{' '}' characters
        /// another special case on both sides is to take a '+' or '-" that is part of a scientific number "1e-3" or "1E-3"
        /// The subexpression should have no brackets, strings or anything else that might contain nested characters that would stuff up the simple regular expression - So these need to be temporarily replaced with '{i}'
        /// We basically find operands of a high-precedence operator by scanning until we see characters like these: "+", "-", "*", "/", "<", ">", "=", "!", "&", "^" 
        /// </summary>
        /// <param name="subexpr"></param>
        /// <param name="op"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        private string ReplaceOperator(string subexpr, string op, string functionName, bool unary, bool rightAssociative)
        {

            string pattern;
            pattern = @"(?<firstOperand>((\d+(\.\d*)?([eE][-\+]?\d+)?)|(\w+\d*)|(\(.*\))|(\{\d*\})|\s)+)\" + op;
            if (!unary)
            {
                pattern += @"(?<secondOperand>((\d+(\.\d*)?([eE][-\+]?\d+)?)|(\w+\d*)|(\(.*\))|(\{\d*\})|\s)+)";
            }
            // This regular expression will find operand^operand where operand can be any of: number, label, {i}, function(...), function{i}, with whitespace 
            // ?<firstOperand> label a group
            // (\d+(\.\d*)([eE][-\+]?\d+)?)  numbers of the form 2 22 22. 22.22 22e22 22.e22 22.22e22 22e-22 22e+22 22E22 
            // (\w+) labels or function name
            // (\(.*\))   function parameters - although brackets are suppossed to have been removed, we may have already put in some functions e.g. Pow(2,3)
            // (\{\d*\})   take any {i} as part of the operand
            // \s match white space
            //
            string rv = subexpr;
            Regex regex;
            if (rightAssociative)
            {
                regex = new Regex(pattern, RegexOptions.RightToLeft);
            }
            else
            {
                regex = new Regex(pattern);
            }
            int safety = 100;
            while (safety>0)
            {
                Match match = regex.Match(rv);
                if (!match.Success) break;
                // TBD remove white space around firstOperand and secondOperand, better still dont match it as part of the operands
                // TBD remove () that may be around the operand - not needed inside the Pow(,)
                if (unary)
                {
                    rv = regex.Replace(rv, functionName + "(" + match.Groups["firstOperand"].Value + ")", 1);
                }
                else
                {
                    rv = regex.Replace(rv, functionName + "(" + match.Groups["firstOperand"].Value + "," + match.Groups["secondOperand"].Value + ")", 1);
                }
                safety--;
            }
            return rv;

            /*
            int operatorIndex = subexpr.LastIndexOf(op);
            if (operatorIndex > 0 & operatorIndex < subexpr.Length - 1)
            {
                int scanLeftIndex = operatorIndex - 1;
                int scanRightIndex = operatorIndex + 1;
                while (char.IsLetterOrDigit(subexpr[scanLeftIndex] || )

            }
            */
        }




        private void TestReplaceOperator()
        {
            assertStringEq(ReplaceOperator("2^3","^","Pow", false, true), "Pow(2,3)");
            assertStringEq(ReplaceOperator("2+3^4+5", "^", "Pow", false, true), "2+Pow(3,4)+5");
            assertStringEq(ReplaceOperator("2^3^4", "^", "Pow", false, true), "Pow(2,Pow(3,4))");
            assertStringEq(ReplaceOperator("11^1.1^1.e-1^22.22E+22^3.3e333", "^", "Pow", false, true), "Pow(11,Pow(1.1,Pow(1.e-1,Pow(22.22E+22,3.3e333))))");
            assertStringEq(ReplaceOperator("(2+2)^(3+3)", "^", "Pow", false, true), "Pow((2+2),(3+3))");
            assertStringEq(ReplaceOperator("{1}^{22}", "^", "Pow", false, true), "Pow({1},{22})");
            assertStringEq(ReplaceOperator("a^bb12", "^", "Pow", false, true), "Pow(a,bb12)");
            assertStringEq(ReplaceOperator("Pow(2,3)^Pow(4,5)", "^", "Pow", false, true), "Pow(Pow(2,3),Pow(4,5))");
            assertStringEq(ReplaceOperator("Pow{2}^Pow{3}", "^", "Pow", false, true), "Pow(Pow{2},Pow{3})");
            assertStringEq(ReplaceOperator(" 2 ^ 3 ", "^", "Pow", false, true), "Pow( 2 , 3 )");

        }




        private void assertStringEq(string a, string b)
        {
            if (a != b) throw new Exception($"Failed: {a} should equal {b}");
        }



        
        private void PostWiringInitialize()
        {
            Debug.WriteLine("Running TestTransformOperator");
            TestReplaceOperator();
            // Debug.WriteLine("Running TestTransform"); // TBD These tests need to instantiate their own TransformOperator and be run from the application with a set of DomainAbstraction tests
            // TestTransform();



        }
    }
}
